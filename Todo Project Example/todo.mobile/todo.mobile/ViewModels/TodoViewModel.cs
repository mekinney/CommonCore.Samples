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
        public FontItem EmptyDataIcon { get; set; }
        public Todo CurrentItem { get; set; } = new Todo();
        public ObservableCollection<Todo> CurrentTodoList { get; set; }
        public bool DataExists { get; set; }

        public ICommand SaveCurrentItem { get; set; }
        public ICommand FABClicked { get; set; }

        public TodoViewModel()
        {
            SaveCurrentItem = new CoreCommand(async(obj) => {
                CurrentItem.UserId = AppSettings.AppUser.Id;
                var saveResult = await this.TodoLogic.AddOrUpdateTodo(CurrentItem);
                if(saveResult.success){
                    CurrentTodoList.Add(saveResult.todo);
                    DataExists = CurrentTodoList.Count > 0 ? true : false;
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
                CurrentTodoList = await this.TodoLogic.GetAllByCurrentUser().ToObservable();
                DataExists = CurrentTodoList.Count > 0 ? true : false;
                this.IsLoadingHUD = false;
                EmptyDataIcon = FontUtil.GetFont("fa-frown-o", FontType.FontAwesome);
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
