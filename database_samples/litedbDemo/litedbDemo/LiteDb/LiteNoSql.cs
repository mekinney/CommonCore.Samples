using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using System.Linq;
using Xamarin.Forms.CommonCore;
using System.Linq.Expressions;

namespace litedbDemo
{
    public class LiteNoSql: ILiteNoSql
    {
        private SemaphoreSlim semaphore;
        private string filePath;
        public LiteDatabase db;

     
        public LiteNoSql()
        {
            semaphore = new SemaphoreSlim(1, 1);
            filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/lite.db";
            db = new LiteDatabase(filePath);
        }

        public async Task<(List<T> Response, Exception Error)> GetAll<T>() where T : LiteDbModel, new()
        {
            (List<T> Response, Exception Error) response = (null, null);
            await semaphore.WaitAsync();
            try
            {
                return await Task.Run(() =>
                {
                    response.Response = db.GetCollection<T>(typeof(T).Name).FindAll().ToList();
                    return response;
                });
            }
			catch (Exception ex)
			{
				ex.LogException("LiteNoSql - GetAll");
                response.Error = ex;
                return response;
			}
            finally{
                semaphore.Release();
            }
        }

		public async Task<(List<T> Response, Exception Error)> Get<T>(Expression<Func<T, bool>> exp) where T : LiteDbModel, new()
		{
            (List<T> Response, Exception Error) response = (null, null);

			await semaphore.WaitAsync();
			try
			{
				return await Task.Run(() =>
				{
                    var collection = db.GetCollection<T>(typeof(T).Name);
                    response.Response = collection.Find(exp).ToList();
					return response;
				});
			}
			catch (Exception ex)
			{
				response.Error = ex;
				return response;
			}
			finally
			{
				semaphore.Release();
			}
		}

		public async Task<(bool Success, Exception Error)> Insert<T>(T obj) where T : LiteDbModel, new()
		{
            (bool Success, Exception Error) response = (false, null);

			await semaphore.WaitAsync();
			try
			{
                return await Task.Run(() =>
                {
                    var collection = db.GetCollection<T>(typeof(T).Name);
                    var result = collection.Insert(obj);
					response.Success = true;
					return response;
                });
			}
            catch(Exception ex)
            {
				response.Error = ex;
				return response;
            }
			finally
			{
				semaphore.Release();
			}
		}

		public async Task<(bool Success, Exception Error)> Update<T>(T obj) where T : LiteDbModel, new()
		{
			(bool Success, Exception Error) response = (false, null);

			await semaphore.WaitAsync();
			try
			{
				return await Task.Run(() =>
				{
					var collection = db.GetCollection<T>(typeof(T).Name);
					response.Success = collection.Update(obj);
					return response;
				});
			}
			catch (Exception ex)
			{
				response.Error = ex;
				return response;
			}
			finally
			{
				semaphore.Release();
			}
		}

		public async Task<(bool Success, Exception Error)> Delete<T>(T obj) where T : LiteDbModel, new()
		{
			(bool Success, Exception Error) response = (false, null);

			await semaphore.WaitAsync();
			try
			{
                return await Task.Run(() =>
                {
                    var collection = db.GetCollection<T>(typeof(T).Name);
                    var result = collection.Delete(x => x.Id == obj.Id);
					response.Success = result > 0 ? true : false;
                    return response;
                });
			}
			catch (Exception ex)
			{
				response.Error = ex;
				return response;
			}
			finally
			{
				semaphore.Release();
			}
		}

		public async Task<(bool Success, Exception Error)> Delete<T>(string id) where T : LiteDbModel, new()
		{
			(bool Success, Exception Error) response = (false, null);

			await semaphore.WaitAsync();
			try
			{
				return await Task.Run(() =>
				{
					var collection = db.GetCollection<T>(typeof(T).Name);
					var result = collection.Delete(x => x.Id == id);
					response.Success = result > 0 ? true : false;
					return response;
				});
			}
			catch (Exception ex)
			{
				response.Error = ex;
				return response;
			}
			finally
			{
				semaphore.Release();
			}
		}
    }
}
