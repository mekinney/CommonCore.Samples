using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace todo.webapi.Data
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public long CompleteByDate { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public long UTCTickStamp { get; set; }
        public string CorrelationID { get; set; }
    }

    public class TodoContext: DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public TodoContext(DbContextOptions<TodoContext> options) :base(options){ }
        protected override void OnModelCreating(ModelBuilder builder)
        { 
            builder.Entity<Todo>().HasKey(m => m.Id); 
            base.OnModelCreating(builder); 
        } 
    }

    public interface ITodoRepository
    {
        Task<List<Todo>> GetAll();
        Task<List<Todo>> GetAllUpdatedByUser(int userId, long utcTickStamp);
        Task<(Dictionary<string, int> dict, Exception error)> Delete(int id);
        Task<(Dictionary<string, int> dict, Exception error)> AddOrUpdate(Todo todo);
        Task<(Dictionary<string, int> dict, Exception error)>  AddOrUpdateCollection(List<Todo> collection);
    }

    public class TodoRepository: ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<Todo>> GetAll()
        {
            return await _context.Todos.ToListAsync();
        }
        public async Task<List<Todo>> GetAllUpdatedByUser(int userId, long utcTickStamp)
        {
            return await _context.Todos.Where(x => x.UserId == userId && x.UTCTickStamp > utcTickStamp).ToListAsync();
        }

        public async Task<(Dictionary<string,int> dict, Exception error)> AddOrUpdateCollection(List<Todo> collection)
        {
            var adds = new List<Todo>();
            var updates = new List<Todo>();
            var dict = new Dictionary<string, int>();
            Exception excep = null;

            foreach(var item in collection)
            {
                if (item.Id == default(int))
                    adds.Add(item);
                else
                    updates.Add(item);
            }
            await _context.AddRangeAsync(adds);
            _context.UpdateRange(updates);

            var results = await _context.SaveChangesAsync();

            collection.ForEach((item) => dict.Add(item.CorrelationID, item.Id));

            if (results == 0)
                excep = new ApplicationException("There was an issue saving the collection");
            
            return (dict, excep);
        }
        public async Task<(Dictionary<string, int> dict, Exception error)> AddOrUpdate(Todo todo)
        {
            Exception excep = null;
            try
            {
                if (todo.Id == default(int))
                {
                    _context.Add(todo);
                }
                else
                {
                    _context.Update(todo);
                }
                var result = await _context.SaveChangesAsync();
                if (result != 1)
                    excep = new ApplicationException("The item did not successfully save to the database.");

            }
            catch (Exception ex)
            {
                excep = ex;
            }

            return (new Dictionary<string, int>() { { todo.CorrelationID, todo.Id } }, excep);

        }
        public async Task<(Dictionary<string, int> dict, Exception error)> Delete(int id)
        {
            Exception excep = null;
            string correlationId = null;
            if (id == default(int))
            {
                excep = new ApplicationException("Object primary key has not been set");
            }
            else
            {

                try
                {
                    var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);
                    if (todo != null)
                    {
                        correlationId = todo.CorrelationID;
                        _context.Todos.Remove(todo);
                        var result = await _context.SaveChangesAsync();
                        if (result == 0)
                            excep = new ApplicationException("Delete was not successful");
                    }
                }
                catch (Exception ex)
                {
                    excep = ex;
                }
            }
            return (new Dictionary<string, int>() { { correlationId, id } }, excep);

        }
    }
}
