﻿using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using System.Threading.Tasks;

namespace sqliteDemo
{
    public class AppViewModel : ObservableViewModel
    {
        private IDisposable queryToken;
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
            People = new ObservableCollection<Person>();

            LoadAllPeople("AppViewModel", null).ContinueWith((t) => { });

            AddPerson = new RelayCommand(async (obj) =>
            {
                var result = await this.SqliteDb.AddOrUpdate<Person>(NewPerson);
                if (result.Success)
                {
					await LoadAllPeople("AddPerson", async () =>
					{
						await Navigation.PushAsync(new PageTwo());
					});
                }
				else
				{
					this.Log.LogResponse(result, "AddPerson - SqliteDb.AddOrUpdate ");
					DialogPrompt.ShowMessage(new Prompt()
					{
						Title = "Error",
						Message = result.Error.Message
					});
				}

            });




            ViewPeople = new Command(async (obj) =>
            {
                await LoadAllPeople("ViewPeople", async()=>{
                    await Navigation.PushAsync(new PageTwo());
                });

            });

        }

        ~AppViewModel()
        {
            queryToken.Dispose();
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
						this.Log.LogResponse(deleteResult, "OnViewMessageReceived - SqliteDb.DeleteByQuery ");
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
			if (allResult.Success)
			{
				People = allResult.Response.ToObservable<Person>();
                callBack?.Invoke();
			}
			else
			{
				this.Log.LogResponse(allResult, $"{caller} - SqliteDb.GetAll ");
				DialogPrompt.ShowMessage(new Prompt()
				{
					Title = "Error",
					Message = allResult.Error.Message
				});
			}
		}

    }
}
