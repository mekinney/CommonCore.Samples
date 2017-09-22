using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LiteDB;
using Xamarin.Forms.CommonCore;

namespace litedbDemo
{
    public class LiteDbModel{
		private ObjectId obj;
		public string Id { get; set; } = ObjectId.NewObjectId().ToString();
		public DateTime CreateTime
		{
			get
			{
				if (obj == null)
					obj = new ObjectId(Id);

				return obj.CreationTime;
			}
		}
		public int TimeStamp
		{
			get
			{
				if (obj == null)
					obj = new ObjectId(Id);

				return obj.Timestamp;
			}
		}
    }

    public interface ILiteNoSql
    {
        Task<(List<T> Response, Exception Error)> GetAll<T>() where T : LiteDbModel, new();
        Task<(bool Success, Exception Error)> Insert<T>(T obj) where T : LiteDbModel, new();
        Task<(bool Success, Exception Error)> Delete<T>(T obj) where T : LiteDbModel, new();
        Task<(bool Success, Exception Error)> Delete<T>(string id) where T : LiteDbModel, new();
        Task<(bool Success, Exception Error)> Update<T>(T obj) where T : LiteDbModel, new();
        Task<(List<T> Response, Exception Error)> Get<T>(Expression<Func<T, bool>> exp) where T : LiteDbModel, new();
    }
}
