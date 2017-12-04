using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class TodoViewModel : CoreViewModel
    {
        public Todo CurrentItem { get; set; } = new Todo();
        public ObservableCollection<Todo> CurrentTodoList { get; set; }

        public ICommand SaveCurrentItem { get; set; }
        public ICommand FABClicked { get; set; }

        public TodoViewModel()
        {
            SaveCurrentItem = new CoreCommand(async(obj) => {
                CurrentItem.Id = AppSettings.AppUser.Id;
                var saveResult = await this.TodoLogic.AddOrUpdateTodo(CurrentItem);
                if(saveResult.success){
                    CurrentTodoList.Add(saveResult.todo);
                    NavigateBack();
                }
                else{
                    this.DialogPrompt.ShowMessage(new Prompt(){
                        Title="Error",
                        Message = saveResult.error.Message
                    });
                }
            });

            FABClicked = new CoreCommand(async (obj) =>
            {
                await Navigation.PushAsync(new AddTodoPage(), true);
            });

        }

        public override void LoadResources(string parameter = null)
        {
            Task.Run(async () => {



                this.LoadingMessageHUD = "Syncing data with server...";
                this.IsLoadingHUD = true; 

                var uploadResult = await this.TodoLogic.TestUploadFile();

                CurrentTodoList = await this.TodoLogic.SyncOfflineData().ToObservable();
                this.IsLoadingHUD = false; 
            });
        }

        public void NavigateBack(){
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync();
            });
        }

    }
}
