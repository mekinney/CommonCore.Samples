﻿using System;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;
using System.Linq;
using Realms;
using System.Collections.ObjectModel;

namespace realmDemo
{
    public class AppViewModel: ObservableViewModel
    {
        private IDisposable queryToken;
 
        public Person NewPerson { get; set; } = new Person();

        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();

        public ICommand AddPerson { get; set; }
        public ICommand ViewPeople { get; set; }

        public AppViewModel()
        {
 
            People = RealmDb.All<Person>().ToObservable();

			queryToken= RealmDb.All<Person>().SubscribeForNotifications((sender, changes, error) =>
            {
                People = RealmDb.All<Person>().ToObservable();
            });

            AddPerson = new RelayCommand(async (obj) => {

				RealmDb.Write(() =>
                {
                    RealmDb.Add(NewPerson);
                    NewPerson = new Person();
                });

                await Navigation.PushAsync(new PageTwo());
            });

            ViewPeople = new RelayCommand(async(obj) => { 
                await Navigation.PushAsync(new PageTwo());
            });

        }

        ~AppViewModel(){
            queryToken.Dispose();
        }

        public override void OnViewMessageReceived(string key, object obj)
        {
            if(key==AppSettings.DeletePersonTag && obj!=null)
            {
                var pk = (string)obj;
                var item = this.RealmDb.All<Person>().FirstOrDefault(x => x.Id == pk);
                if (item != null){
					using (var trans = RealmDb.BeginWrite())
					{
						RealmDb.Remove(item);
						trans.Commit();
					}
                }

            }
        }

    }
}
