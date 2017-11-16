using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.CommonCore;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace todo.mobile
{
    public class TodoBusinessLogic : BusinessBase
    {
        private string TodoBase
        {
            get { return AppSettings.Config.WebApi[AppSettings.TodoAPIBase]; }
        }

        public async Task<(Todo todo, bool success, Exception error)> AddOrUpdateTodo(Todo todo)
        {
            Exception ex = null;
            var httpResult = await HttpService.Post<Dictionary<Guid,int>>($"{TodoBase}/AddOrUpdate", todo);

            if (httpResult.Success)
            {
                todo.Id = httpResult.Response.First().Value;
            }

            var dbResult = await this.SqliteDb.AddOrUpdate<Todo>(todo);

            if(!httpResult.Success || !dbResult.Success)
            {
                httpResult.Error?.LogException("TodoBusinessLogic - AddOrUpdateTodo"); 
                dbResult.Error?.LogException("TodoBusinessLogic - AddOrUpdateTodo");
                ex = dbResult.Error; //Only concerned if locally not saved at this point
            }

            return (todo, ex != null ? false : true, ex);
        }


        public async Task DeleteTodo(Todo todo)
        {
            var httpResult = await HttpService.Get<Dictionary<Guid,int>>($"{TodoBase}/Delete?id={todo.Id}");
            if(httpResult.Success)
            {
                await this.SqliteDb.DeleteByCorrelationID<Todo>(todo.CorrelationID);
            }
            else{
                todo.MarkedForDelete = true;
                await this.SqliteDb.AddOrUpdate<Todo>(todo);
            }
        }

        public async Task<List<Todo>> SyncOfflineData()
        {

            var result = await this.HttpService.Get<List<Todo>>($"{TodoBase}/GetAllUpdatedByUser?userId={AppSettings.AppUser.Id}&utcTickStamp={CoreSettings.SyncTimeStamp}");
            if(result.Success){
                var updates = await CreateUpdateLists(result.Response);
                await this.SqliteDb.AddOrUpdate<Todo>(updates.localUpdates);
                var srvUpdateResult = await this.HttpService.Post<List<Todo>>($"{TodoBase}/AddOrUpdateCollection",updates.serverUpdates);
                if(srvUpdateResult.Success)
                {
                    var newItemsSavedOnServer = srvUpdateResult.Response.Where(x=> updates.locallyCreatedItems.Contains(x.CorrelationID));
                    var newItemsBulkInsertResult = await this.SqliteDb.AddOrUpdate<Todo>(newItemsSavedOnServer);
                    if(!newItemsBulkInsertResult.Success){
                        //Something bad happended
                    }
                }
                else
                {
                    srvUpdateResult.Error?.LogException("TodoBusinessLogic - SyncOfflineData");
                }

                CoreSettings.SyncTimeStamp = DateTime.UtcNow.Ticks;
            }
            else
            {
                result.Error?.LogException("TodoBusinessLogic - SyncOfflineData");
            }


            return result.Response;
        }

        private async Task<(List<Todo> localUpdates, List<Todo> serverUpdates, List<Guid> locallyCreatedItems)> CreateUpdateLists(List<Todo> serverData)
        {
            var ids = serverData.Select(x => x.Id).ToArray();
            var alreadyExists = await this.SqliteDb.GetByQuery<Todo>(x => ids.Contains(x.Id)); // perform only one query
            var localList = new List<Todo>(); //server to local updates
            var serverList = new List<Todo>(); //local to server updates
            foreach (var obj in serverData)
            {
                var item = alreadyExists.Response.FirstOrDefault(x => x.Id == obj.Id);
                if (item != null)
                {
                    if (obj.MarkedForDelete)
                    {
                        await this.SqliteDb.DeleteByCorrelationID<Todo>(item.CorrelationID);
                    }
                    else
                    {
                        obj.CorrelationID = item.CorrelationID;
                        if (item.UTCTickStamp < obj.UTCTickStamp) // is the server version more recent?
                            localList.Add(obj);
                        else
                            serverList.Add(obj); //local version is more recent
                    }
                }
                else
                {
                    localList.Add(obj); //server version doesn't exist locally
                }
            }

            //Now get items in local database that have zero for primary key (newely created locally only)
            //Send to server and then update the local version with the updated primary key

            var locallyCreatedItems = new List<Guid>();
            var newItemsLocal = await SqliteDb.GetByQuery<Todo>(x => x.Id == 0);
            if (newItemsLocal.Success)
                newItemsLocal.Response.ForEach((item) => { 
                    serverList.Add(item);
                    locallyCreatedItems.Add(item.CorrelationID);
            });


            return (localList, serverList, locallyCreatedItems);
        }
    }
}