using System;
using todo.mobile;
using Xamarin.Forms;


namespace Xamarin.Forms.CommonCore
{
    public partial class CoreSettings
    {
        public const string DataUpdated = "dataUpdated";
        public const string TodoAPIBase = "todoAPI";
        public const string UploadAPIBase = "uploadAPI";
        public const string UserAPIBase = "userAPI";
        public const string HubAPIBase = "hubApi";
        public static User AppUser { get; set; }
    }
}
