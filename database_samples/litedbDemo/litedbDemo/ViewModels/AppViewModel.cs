﻿using System;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace litedbDemo
{
    public class AppViewModel : CoreViewModel
    {
        public Person NewPerson { get; set; } = new Person();
        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();

        public ICommand AddPerson { get; set; }
        public ICommand ViewPeople { get; set; }

        public AppViewModel()
        {
            LoadAllPeople("AppViewModel", null).ContinueWith((t) => { });

            AddPerson = new CoreCommand(async (obj) =>
            {
                var result = await this.LiteDb.Insert(NewPerson);
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
                    DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Error",
                        Message = result.Error.Message
                    });
                }
            });

            ViewPeople = new CoreCommand(async (obj) =>
            {
                await Navigation.PushAsync(new PageTwo());
            });

        }

        public override void OnViewMessageReceived(string key, object obj)
        {
            if (key == CoreSettings.DeletePersonTag && obj != null)
            {
                var pk = (string)obj;
                this.LiteDb.Delete<Person>(pk).ContinueWith(async (t) =>
                {
                    if (t.Result.Success)
                    {
                        await LoadAllPeople("OnViewMessageReceived",null);
                    }
                    else{

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
            var allResult = await this.LiteDb.GetAll<Person>();
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
