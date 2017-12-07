using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class AppSettings : CoreSettings
    {
        public const string DataUpdated = "dataUpdated";
        public const string TodoAPIBase = "todoAPI";
        public const string UploadAPIBase = "uploadAPI";
        public const string UserAPIBase = "userAPI";
        public const string HubAPIBase = "hubApi";
        public static User AppUser { get; set; }
    }
}
