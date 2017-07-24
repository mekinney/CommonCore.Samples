using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LiteDB;

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
        Task<List<T>> GetAll<T>() where T : LiteDbModel, new();
        Task<bool> Insert<T>(T obj) where T : LiteDbModel, new();
        Task<bool> Delete<T>(T obj) where T : LiteDbModel, new();
        Task<bool> Delete<T>(string id) where T : LiteDbModel, new();
        Task<bool> Update<T>(T obj) where T : LiteDbModel, new();
        Task<List<T>> Get<T>(Expression<Func<T, bool>> exp) where T : LiteDbModel, new();
    }
}
