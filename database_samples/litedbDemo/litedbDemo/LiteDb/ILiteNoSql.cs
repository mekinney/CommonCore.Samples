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
        Task<GenericResponse<List<T>>> GetAll<T>() where T : LiteDbModel, new();
        Task<BooleanResponse> Insert<T>(T obj) where T : LiteDbModel, new();
        Task<BooleanResponse> Delete<T>(T obj) where T : LiteDbModel, new();
        Task<BooleanResponse> Delete<T>(string id) where T : LiteDbModel, new();
        Task<BooleanResponse> Update<T>(T obj) where T : LiteDbModel, new();
        Task<GenericResponse<List<T>>> Get<T>(Expression<Func<T, bool>> exp) where T : LiteDbModel, new();
    }
}
