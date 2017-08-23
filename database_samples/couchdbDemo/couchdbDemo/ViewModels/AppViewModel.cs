using System;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace couchdbDemo
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

            this.CouchDb.GetAll<Person>().ContinueWith((t) =>
            {
                if (t.Result != null)
                {
                    People = t.Result.ToObservable();
                }
                else
                {
                    People = new ObservableCollection<Person>();
                }
            });


            AddPerson = new RelayCommand(async (obj) => {
                var result = await this.CouchDb.AddOrUpdate<Person>(NewPerson);
                if (result!=null)
                {
                    NewPerson = new Person();
                    People = await CouchDb.GetAll<Person>().ToObservable();
                    await Navigation.PushAsync(new PageTwo());
                }
            });

            ViewPeople = new RelayCommand(async(obj) => { 
                await Navigation.PushAsync(new PageTwo());
            });

        }

        public override void OnViewMessageReceived(string key, object obj)
        {
            
            if(key==AppSettings.DeletePersonTag && obj!=null)
            {
                var pk = (string)obj;
                Task.Run(async () =>
                {
                    var result = await this.CouchDb.Delete<Person>(pk);
                    if (result)
                    {
                        People = await CouchDb.GetAll<Person>().ToObservable();
                    }
                });
            }
        }

    }
}
