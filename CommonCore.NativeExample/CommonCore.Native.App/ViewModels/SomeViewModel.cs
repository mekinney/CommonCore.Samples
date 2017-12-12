using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;

namespace CommonCore.Native.App
{
    public class SomeViewModel : CoreViewModel
    {
        public string SomeText { get; set; }
        public int TotalItems { get; set; }
        public ICommand SomeAction { get; set; }

        public SomeViewModel()
        {
            SomeAction = new CoreCommand(async (obj) =>
            {
                LoadingMessageHUD = "Some action...";
                IsLoadingHUD = true;
                await Task.Delay(new TimeSpan(0, 0, 4));
                IsLoadingHUD = false;
            });
        }

        public override void LoadResources(string parameter = null)
        {
            var items = this.SomeLogic.GetSomeData();
            if (items.error == null)
            {
                TotalItems = items.data.Count;
            }
            else
            {
                this.DialogPrompt.ShowMessage(new Prompt(){
                    Title="Error",
                    Message = items.error.Message
                });
            }
        }
    }
}
