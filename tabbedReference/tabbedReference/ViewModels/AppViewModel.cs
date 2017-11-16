using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;

namespace tabbedReference
{
    public class AppViewModel : CoreViewModel, ISearchProvider
    {
        public RandomUser SelectedRandomUser { get; set; } = new RandomUser();
        public ObservableCollection<RandomUser> RandomUsers { get; set; } = new ObservableCollection<RandomUser>();
        public ObservableCollection<RandomUser> Favorites { get; set; } = new ObservableCollection<RandomUser>();

        public ICommand SearchCommand { get; set; }

        public bool SearchIsDefaultAction { get; set; } = false;
        public string QueryHint { get; set; } = "Last Name";

        public AppViewModel()
        {
            RandomUsers = new ObservableCollection<RandomUser>();

            GetRandomUsers().ContinueWith((t) => { });

            GetFavorites().ContinueWith((t) => { });

            SearchCommand = new CoreCommand((obj) => {

                var search = obj;
            });
        }


        private async Task GetRandomUsers()
        {
            this.LoadingMessageHUD = "Performing download...";
            this.IsLoadingHUD = true;

            var url = this.WebApis["randomuser"];

            var result = await this.HttpService.Get<RootObject>(url);
            result.Error?.LogException();

            this.IsLoadingHUD = false;
            if (result.Error==null)
            {
                RandomUsers = result.Response.results.ToRandomUserObservableCollection();
            }

        }

        private async Task GetFavorites()
        {
            var result = await this.SqliteDb.GetAll<RandomUser>();
            result.Error?.LogException();
            if (result.Error == null)
            {
                Favorites = result.Response.ToObservable<RandomUser>();
            }
        }

        public async Task AddToFavorites(RandomUser user)
        {
            var result = await this.SqliteDb.AddOrUpdate<RandomUser>(user);
			result.Error?.LogException();
            if (!result.Success)
            {
                DialogPrompt.ShowMessage(new Prompt()
                {
                    Title = "Error",
                    Message = result.Error.Message
                });
            }
            else
            {
                Favorites.Add(user);
            }

        }

        public async Task RemoveFavorites(RandomUser user)
        {
            var result = await this.SqliteDb.DeleteByCorrelationID<RandomUser>(user.CorrelationID);
			result.Error?.LogException();
            if (!result.Success)
            {
                DialogPrompt.ShowMessage(new Prompt()
                {
                    Title = "Error",
                    Message = result.Error.Message
                });
            }
            else
            {
                Favorites.Remove(user);
            }

        }

        public override void ReleaseResources(string parameter = null)
        {
            Task.Run(async () =>
            {
                await this.FileStore.SaveAsync<List<RandomUser>>("RandomUser", RandomUsers.ToList());
                RandomUsers.Clear();
            });
        }
        public override void LoadResources(string parameter = null)
        {
            Task.Run(async () =>
            {
                var temp = await this.FileStore.GetAsync<List<RandomUser>>("RandomUser");
                if (temp.Error==null)
                {
                    RandomUsers = temp.Response.ToObservable<RandomUser>();
                }
                else
                {
                    await GetRandomUsers();
                }
            });
        }
    }
}
