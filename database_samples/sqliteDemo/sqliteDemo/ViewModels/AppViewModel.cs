using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace sqliteDemo
{
    public class AppViewModel : ObservableViewModel
    {
        
        public Person NewPerson { get; set; } = new Person();
        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();

        public ICommand AddPerson { get; set; }
        public ICommand ViewPeople { get; set; }

        public AppViewModel()
        {
            
            LoadAllPeople("AppViewModel", null).ContinueWith((t) => { });

            AddPerson = new RelayCommand(async (obj) =>
            {
                var result = await this.SqliteDb.AddOrUpdate<Person>(NewPerson);
                if (result.Success)
                {
                    NewPerson = new Person();
					await LoadAllPeople("AddPerson", () =>
					{
                        Navigation.PushNonAwaited<PageTwo>();
					});
                }
				else
				{
					DialogPrompt.ShowMessage(new Prompt()
					{
						Title = "Error",
						Message = result.Error.Message
					});
				}

            });




            ViewPeople = new Command(async (obj) =>
            {
                await LoadAllPeople("ViewPeople", ()=>{
                    Navigation.PushNonAwaited<PageTwo>();
                });

            });

        }


        public override void OnViewMessageReceived(string key, object obj)
        {
            if (key == AppSettings.DeletePersonTag && obj != null)
            {
                var pk = (string)obj;

                SqliteDb.DeleteByQuery<Person>(x => x.Id == pk).ContinueWith(async(t) => {
                    var deleteResult = t.Result;
                    if(deleteResult.Success){
                        await LoadAllPeople("OnViewMessageReceived", null);
                    }
					else
					{
						DialogPrompt.ShowMessage(new Prompt()
						{
							Title = "Error",
							Message = deleteResult.Error.Message
						});
					}
                });
            }
        }

		private async Task LoadAllPeople(string caller, Action callBack)
		{
			var allResult = await SqliteDb.GetAll<Person>();
			if (allResult.Error==null)
			{
				People = allResult.Response.ToObservable<Person>();
                callBack?.Invoke();
			}
			else
			{
				DialogPrompt.ShowMessage(new Prompt()
				{
					Title = "Error",
					Message = allResult.Error.Message
				});
			}
		}

    }
}
