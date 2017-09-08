using System;
namespace Xamarin.Forms.CommonCore
{
    public class AdBuzzieSettings
    {
        public string IOSPublisherKey { get; set; }
        public string DroidPublisherKey { get; set; }
    }
    public partial class ConfigurationModel
    {
        public AdBuzzieSettings AdBuzzie { get; set; }
        public string InAppPurchaseProductId { get; set; }
    }
}
