using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace todo.webapi.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsLocked { get; set; }
        public string MetaData { get; set; }
        public long UTCTickStamp { get; set; }
        public string CorrelationID { get; set; }
    }

    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Todo>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }

    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByLogin(string userName, string hashedPassword);
        Task<(Dictionary<string, int> dict, Exception error)> Delete(int id);
        Task<(Dictionary<string, int> dict, Exception error)> AddOrUpdate(User todo);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByLogin(string userName, string hashedPassword)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == hashedPassword);
        }

        public async Task<(Dictionary<string, int> dict, Exception error)> AddOrUpdate(User user)
        {
            Exception excep = null;

            try
            {
                if (user.Id == default(int))
                {
                    _context.Add(user);
                }
                else
                {
                    _context.Update(user);
                }
                var result = await _context.SaveChangesAsync();
                if (result != 1)
                    excep = new ApplicationException("The item did not successfully save to the database.");

            }
            catch (Exception ex)
            {
                excep = ex;
            }

            return (new Dictionary<string, int>() { { user.CorrelationID, user.Id } }, excep);

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
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                    if (user != null)
                    {
                        correlationId = user.CorrelationID;
                        _context.Users.Remove(user);
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
