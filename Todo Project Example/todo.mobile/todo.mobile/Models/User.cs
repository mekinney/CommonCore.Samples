using System;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class Profile : User
    {
        public string Password { get; set; }

        public Profile()
        {

        }
        public Profile(User usr)
        {
            foreach(var prop in typeof(User).GetProperties())
            {
                if(prop.Name!="TokenIsValid")
                    prop.SetValue(this, prop.GetValue(usr));
            }
        }
    }
    public class User: CoreModel
    {
        public int Id { get; set; }
        public Guid CorrelationID { get; set; } = Guid.NewGuid();
        public long UTCTickStamp { get; set; } = DateTime.UtcNow.Ticks;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool IsLocked { get; set; }
        public string MetaData { get; set; }

        public CoreAuthentication Token { get; set; }

        public bool TokenIsValid
        {
            get
            {
                if (Token == null)
                    return false;
                else
                    return Token.TokenIsValid;
            }
        }
    }
}
