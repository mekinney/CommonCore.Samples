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

        public async Task<List<T>> GetAll<T>() where T : LiteDbModel, new()
        {
            await semaphore.WaitAsync();
            try
            {
                return await Task.Run(() =>
                {
                    return db.GetCollection<T>(typeof(T).Name).FindAll().ToList();
                });
            }
			catch (Exception ex)
			{
                ex.ConsoleWrite();
                return null;
			}
            finally{
                semaphore.Release();
            }
        }

		public async Task<List<T>> Get<T>(Expression<Func<T, bool>> exp) where T : LiteDbModel, new()
		{
			await semaphore.WaitAsync();
			try
			{
				return await Task.Run(() =>
				{
                    var collection = db.GetCollection<T>(typeof(T).Name);
                    return collection.Find(exp).ToList();
				});
			}
			catch (Exception ex)
			{
				ex.ConsoleWrite();
				return null;
			}
			finally
			{
				semaphore.Release();
			}
		}

		public async Task<bool> Insert<T>(T obj) where T : LiteDbModel, new()
		{
			await semaphore.WaitAsync();
			try
			{
                return await Task.Run(() =>
                {
                    var collection = db.GetCollection<T>(typeof(T).Name);
                    var result = collection.Insert(obj);
                    return true;
                });
			}
            catch(Exception ex)
            {
				ex.ConsoleWrite();
                return false;
            }
			finally
			{
				semaphore.Release();
			}
		}

		public async Task<bool> Update<T>(T obj) where T : LiteDbModel, new()
		{
			await semaphore.WaitAsync();
			try
			{
				return await Task.Run(() =>
				{
					var collection = db.GetCollection<T>(typeof(T).Name);
					return collection.Update(obj);
				});
			}
			catch (Exception ex)
			{
				ex.ConsoleWrite();
				return false;
			}
			finally
			{
				semaphore.Release();
			}
		}

		public async Task<bool> Delete<T>(T obj) where T : LiteDbModel, new()
		{
			await semaphore.WaitAsync();
			try
			{
                return await Task.Run(() =>
                {
                    var collection = db.GetCollection<T>(typeof(T).Name);
                    var result = collection.Delete(x => x.Id == obj.Id);
					return result > 0 ? true : false;
                });
			}
			catch (Exception ex)
			{
                ex.ConsoleWrite();
				return false;
			}
			finally
			{
				semaphore.Release();
			}
		}

		public async Task<bool> Delete<T>(string id) where T : LiteDbModel, new()
		{
			await semaphore.WaitAsync();
			try
			{
				return await Task.Run(() =>
				{
					var collection = db.GetCollection<T>(typeof(T).Name);
					var result = collection.Delete(x => x.Id == id);
                    return result > 0 ? true : false;
				});
			}
			catch (Exception ex)
			{
				ex.ConsoleWrite();
				return false;
			}
			finally
			{
				semaphore.Release();
			}
		}
    }
}
