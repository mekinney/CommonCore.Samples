using System;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;
using System.Linq;
using System.Collections.ObjectModel;

namespace litedbDemo
{
    public class AppViewModel: ObservableViewModel
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

            this.LiteNoSqlService.GetAll<Person>().ContinueWith((t) =>
            {
                People = t.Result.ToObservable();
            });


            AddPerson = new RelayCommand(async (obj) => {
                var result = await this.LiteNoSqlService.Insert(NewPerson);
                if (result)
                {
                    NewPerson = new Person();
                    People = await this.LiteNoSqlService.GetAll<Person>().ToObservable();
                    await Navigation.PushAsync(new PageTwo());
                }
            });

            ViewPeople = new RelayCommand(async(obj) => {
                await Navigation.PushAsync(new PageTwo());
            });

        }

        public override void OnViewMessageReceived(string key, object obj)
        {
            if(key=="deletePerson" && obj!=null)
            {
                var pk = (string)obj;
                this.LiteNoSqlService.Delete<Person>(pk).ContinueWith(async (t) =>
                {
                    if (t.Result)
                    {
                        People = await this.LiteNoSqlService.GetAll<Person>().ToObservable();
                    }
                });

            }
        }

    }
}
