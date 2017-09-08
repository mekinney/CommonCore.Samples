using System;
namespace Xamarin.Forms.CommonCore
{
    public class AdmobSettings
    {
        public string IOSApp { get; set; }
        public string IOSBanner { get; set; }
        public string IOSInterstitial { get; set; }
        public string DroidApp { get; set; }
        public string DroidBanner { get; set; }
        public string DroidInterstitial { get; set; }
    }
    public partial class ConfigurationModel
    {
        public AdmobSettings Admob { get; set; }
        public string InAppPurchaseProductId { get; set; }
    }
}
