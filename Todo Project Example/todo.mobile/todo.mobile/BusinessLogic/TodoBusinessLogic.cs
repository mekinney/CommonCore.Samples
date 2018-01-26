using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.CommonCore;
using System.Linq;

namespace todo.mobile
{
    public class TodoBusinessLogic : CoreBusiness
    {
        private string todoBase = CoreSettings.Config.WebApi[CoreSettings.TodoAPIBase];
        private string uploadBase = CoreSettings.Config.WebApi[CoreSettings.UploadAPIBase];

        public async Task<(Todo todo, bool success, Exception error)> AddOrUpdateTodo(Todo todo)
        {
            Exception ex = null;
            var httpResult = await HttpService.Post<Dictionary<Guid,int>>($"{todoBase}/AddOrUpdate", todo);
            httpResult.Error?.LogException("TodoBusinessLogic - AddOrUpdateTodo"); 
            ex = httpResult.Error;

            if (httpResult.Success)
            {
                todo.Id = httpResult.Response.First().Value;
            }

            var dbResult = await this.SqliteDb.AddOrUpdate<Todo>(todo);
            dbResult.Error?.LogException("TodoBusinessLogic - AddOrUpdateTodo");
            ex = dbResult.Error;

            return (todo, ex != null ? false : true, ex);
        }


        public async Task<(List<Todo> Response, bool Success, Exception Error)> DeleteTodo(Todo todo)
        {
            todo.MarkedForDelete = true;
            var httpResult = await HttpService.Post<Dictionary<Guid, int>>($"{todoBase}/AddOrUpdate", todo);
            if(httpResult.Success)
            {
                await this.SqliteDb.DeleteByCorrelationID<Todo>(todo.CorrelationID);
            }
            else{
                todo.MarkedForDelete = true;
                await this.SqliteDb.AddOrUpdate<Todo>(todo);
            }

            return await this.SqliteDb.GetByQuery<Todo>(x=>x.MarkedForDelete==false);
  
        }

        public async Task<List<Todo>> GetAllByCurrentUser()
        {
            var exempt = new List<Guid>();
            var httpResult = await this.HttpService.Get<List<Todo>>($"{todoBase}/GetAllUpdatedByUser?userId={CoreSettings.AppUser.Id}&utcTickStamp={CoreSettings.SyncTimeStamp}");
            httpResult.Error?.LogException("TodoBusinessLogic - GetAllByCurrentUser - GetAllUpdatedByUser");
            var temp = httpResult.Response;
            if (httpResult.Success && httpResult.Response.Count > 0)
            {
                var markedForDelete = httpResult.Response.Where(x => x.MarkedForDelete == true);
                foreach (var deletedItem in markedForDelete)
                    await this.SqliteDb.DeleteByCorrelationID<Todo>(deletedItem.CorrelationID);

                var updatedItems = httpResult.Response.Where(x => x.MarkedForDelete == false);
                var dbResult = await this.SqliteDb.AddOrUpdate<Todo>(updatedItems);
                exempt = httpResult.Response.Select(x => x.CorrelationID).ToList();
            }

            var sqlResult = await this.SqliteDb.GetAll<Todo>();
            sqlResult.Error?.LogException("TodoBusinessLogic - GetAllByCurrentUser - GetAll");
            var syncBackUp = sqlResult.Response?.Where(x => x.UTCTickStamp > CoreSettings.SyncTimeStamp && !exempt.Contains(x.CorrelationID));
            if (syncBackUp.Count() > 0)
            {
                var syncBackUpResult = await this.HttpService.Post<Dictionary<Guid, int>>($"{todoBase}/AddOrUpdateCollection", syncBackUp);
                syncBackUpResult.Error?.LogException("TodoBusinessLogic - GetAllByCurrentUser - AddOrUpdateCollection");
                if(syncBackUpResult.Success)
                {
                    foreach(var item in syncBackUp)
                    {
                        item.Id = syncBackUpResult.Response[item.CorrelationID];
                    }
                    var localUpdateResult = await this.SqliteDb.AddOrUpdate<Todo>(syncBackUp);
                    localUpdateResult.Error?.LogException("TodoBusinessLogic - GetAllByCurrentUser - AddOrUpdate");
                }
            }
            CoreSettings.SyncTimeStamp = DateTime.UtcNow.Ticks;
            return sqlResult.Response.Where(x => x.MarkedForDelete == false).ToList();
       
        }

    }
}