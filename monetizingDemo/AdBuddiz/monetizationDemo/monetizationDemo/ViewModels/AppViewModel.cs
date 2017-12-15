using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace monetizationDemo.ViewModels
{
    public class AppViewModel : CoreViewModel
    {
        public ICommand RemoveAds { get; set; }

        public AppViewModel()
        {

            RemoveAds = new CoreCommand(async(obj) => {
                await MakePurchase();
            });
        }


        public async Task<bool> MakePurchase()
        {
            IInAppBilling billing = null;

            if (!CrossInAppBilling.IsSupported)
                return false;

            try
            {
                billing = CrossInAppBilling.Current;
                var connected = await billing.ConnectAsync();
                if (!connected)
                    return false;

                try
                {
                    var purchase = await CrossInAppBilling.Current.PurchaseAsync
                    (
                        CoreSettings.Config.InAppPurchaseProductId, 
                        ItemType.InAppPurchase, 
                        "apppayload"
                    );

                    if (purchase == null)
                    {
                        DialogPrompt.ShowMessage(new Prompt(){
                            Title="Error",
                            Message="The purchase failed and you will receive ads forever."
                        });

                        return false;
                    }
                    else
                    {
                        //Purchased, save this information
                        var id = purchase.Id;
                        var token = purchase.PurchaseToken;
                        var state = purchase.State;

                        await FileStore.SaveAsync<InAppBillingPurchase>("adsRemoved", purchase);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Error",
                        Message = ex.Message
                    });
                    return false;
                }

            }
            finally
            {
                await billing?.DisconnectAsync();
            }
        }

        public override void OnViewMessageReceived(string key, object obj)
        {
           
        }
    }
}
