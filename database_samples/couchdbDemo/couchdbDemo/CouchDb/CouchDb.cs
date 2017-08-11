using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Couchbase.Lite;
using Newtonsoft.Json;
using Xamarin.Forms.CommonCore;
using System.Linq;
using System.Collections;
using System.Reflection;

namespace couchdbDemo
{
    public static class CouchDbExtensions
    {
        public static ObservableCollection<T> ToObservable<T>(this QueryEnumerator query)
        {
            var collection = new ObservableCollection<T>();
            foreach (QueryRow obj in query)
            {
                var str = JsonConvert.SerializeObject(obj.Document.Properties);
                var n = JsonConvert.DeserializeObject<T>(str);
                collection.Add(n);
            }
            return collection;
        }
        public static List<T> ToList<T>(this QueryEnumerator query)
        {
            var collection = new List<T>();
            foreach (QueryRow obj in query)
            {
                var str = JsonConvert.SerializeObject(obj.Document.Properties);
                var n = JsonConvert.DeserializeObject<T>(str);
                collection.Add(n);
            }
            return collection;
        }
    }
    public interface ICouchDb
    {
        Task<List<T>> GetAll<T>() where T : IDbModel;
        Task<bool> AddOrUpdate<T>(T obj) where T : IDbModel;
        Task<bool> Delete<T>(T obj) where T : IDbModel;
        Task<bool> Delete<T>(string id) where T : IDbModel;
    }
    public class CouchDb : ICouchDb
    {
        private Database _db;
        private Couchbase.Lite.View pkIndexedView;

        public Couchbase.Lite.View PKView
        {
            get
            {
                if (pkIndexedView == null)
                {
                    pkIndexedView = _db.GetView("pkIndexedView");
                    pkIndexedView.SetMap((doc, emit) =>
                    {
                        if (doc.ContainsKey("Id") && doc["Id"] is string)
                        {
                            emit(doc["Id"], null);
                        }
                    }, "1");
                }
                return pkIndexedView;
            }
        }

        public CouchDb()
        {
            Couchbase.Lite.Storage.SystemSQLite.Plugin.Register();
            _db = Manager.SharedInstance.GetDatabase("couchdb-demo");
        }

        public async Task<List<T>> GetAll<T>() where T : IDbModel
        {
            return await Task.Run(async () =>
            {
                var result = await PKView.CreateQuery().RunAsync();
                return result.ToList<T>();
            });

        }
        public async Task<bool> AddOrUpdate<T>(T obj) where T : IDbModel
        {
            return await Task.Run(() =>
           {
               if (obj.Id == null)
               {
                   obj.Id = Guid.NewGuid().ToString();
                   var doc = _db.CreateDocument();
                   doc.PutProperties(obj.ToDictionary());
                   return true;
               }
               else
               {
                   var doc = _db.GetDocument(obj.Id);
                   doc.Update(newRevision =>
                   {
                       newRevision.SetUserProperties(obj.ToDictionary());
                       return true;
                   });
               }
               return true;
           });
        }

        public async Task<bool> Delete<T>(T obj) where T : IDbModel
        {
            return await Delete<T>(obj.Id);
        }

        public async Task<bool> Delete<T>(string id) where T : IDbModel
        {
            return await Task.Run(() =>
            {
                try
                {
                    var doc = GetDocumentByGuidId(id);
                    if (doc != null)
                    {
                        doc.Delete();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                    return false;
                }

            });

        }

        private Document GetDocumentByGuidId(string id)
        {
			var result = _db.AsQueryable().Where(x => (string)x.DocumentProperties["Id"] == id)
			   .Select(x => x.DocumentProperties["_id"]);
			foreach (var obj in result)
			{
				return _db.GetExistingDocument((string)obj);
			}

            return null;
        }
	}
}
