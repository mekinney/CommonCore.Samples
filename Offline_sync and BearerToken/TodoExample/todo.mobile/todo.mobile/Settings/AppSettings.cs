using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class AppSettings : CoreSettings
    {
        public const string TodoAPIBase = "todoAPI";
        public const string UserAPIBase = "userAPI";
        public static User AppUser { get; set; }
    }
}
