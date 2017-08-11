using System;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace litedbDemo
{
    public class AppViewModel : ObservableViewModel
    {
        private Person newPerson;
        private ObservableCollection<Person> people;

        public Person NewPerson
        {
            get { return newPerson ?? (newPerson = new Person()); }
            set { SetProperty(ref newPerson, value); }
        }

        public ObservableCollection<Person> People
        {
            get { return people ?? (people = new ObservableCollection<Person>()); }
            set { SetProperty(ref people, value); }
        }

        public ICommand AddPerson { get; set; }
        public ICommand ViewPeople { get; set; }

        public AppViewModel()
        {
            LoadAllPeople("AppViewModel", null).ContinueWith((t) => { });

            AddPerson = new RelayCommand(async (obj) =>
            {
                var result = await this.LiteNoSqlService.Insert(NewPerson);
                if (result.Success)
                {
                    NewPerson = new Person();
                    await LoadAllPeople("AddPerson", async () =>
                    {
                        await Navigation.PushAsync(new PageTwo());
                    });
                }
                else
                {
                    this.Log.LogResponse(result, "AddPerson - LiteNoSqlService.Insert ");
                    DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Error",
                        Message = result.Error.Message
                    });
                }
            });

            ViewPeople = new RelayCommand(async (obj) =>
            {
                await Navigation.PushAsync(new PageTwo());
            });

        }

        public override void OnViewMessageReceived(string key, object obj)
        {
            if (key == AppSettings.DeletePersonTag && obj != null)
            {
                var pk = (string)obj;
                this.LiteNoSqlService.Delete<Person>(pk).ContinueWith(async (t) =>
                {
                    if (t.Result.Success)
                    {
                        await LoadAllPeople("OnViewMessageReceived",null);
                    }
                    else{
						this.Log.LogResponse(t.Result, "OnViewMessageReceived - LiteNoSqlService.Delete ");
						DialogPrompt.ShowMessage(new Prompt()
						{
							Title = "Error",
							Message = t.Result.Error.Message
						});
                    }
                });

            }
        }

        private async Task LoadAllPeople(string caller, Action callBack)
        {
			var allResult = await this.LiteNoSqlService.GetAll<Person>();
			if (allResult.Success)
			{
				People = allResult.Response.ToObservable<Person>();
                callBack?.Invoke();
			}
			else
			{
                this.Log.LogResponse(allResult, $"{caller} - LiteNoSqlService.GetAll ");
				DialogPrompt.ShowMessage(new Prompt()
				{
					Title = "Error",
					Message = allResult.Error.Message
				});
			}
        }

    }
}
