using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms.CommonCore;

namespace tabbedReference
{
    public class AppViewModel : ObservableViewModel
    {
        private ObservableCollection<RandomUser> randomUsers;
        private ObservableCollection<RandomUser> favorites;
        private RandomUser selectedRandomUser;

        public RandomUser SelectedRandomUser
        {
            get { return selectedRandomUser ?? (selectedRandomUser = new RandomUser()); }
            set { SetProperty(ref selectedRandomUser, value); }
        }

        public ObservableCollection<RandomUser> RandomUsers
        {
            get { return randomUsers ?? (randomUsers = new ObservableCollection<RandomUser>()); }
            set { SetProperty(ref randomUsers, value); }
        }

        public ObservableCollection<RandomUser> Favorites
        {
            get { return favorites ?? (favorites = new ObservableCollection<RandomUser>()); }
            set { SetProperty(ref favorites, value); }
        }

        public AppViewModel()
        {
            RandomUsers = new ObservableCollection<RandomUser>();

            GetRandomUsers().ContinueWith((t) => { });

            GetFavorites().ContinueWith((t) => { });
        }


        private async Task GetRandomUsers()
        {
            this.LoadingMessageHUD = "Performing download...";
            this.IsLoadingHUD = true;

            var url = this.WebApis["randomuser"];

            var result = await this.HttpService.Get<RootObject>(url);
            Log.LogResponse(result);

            this.IsLoadingHUD = false;
            if (result.Success)
            {
                RandomUsers = result.Response.results.ToRandomUserObservableCollection();
            }

        }

        private async Task GetFavorites()
        {
            var result = await this.SqliteDb.GetAll<RandomUser>();
            Log.LogResponse(result);
            if (result.Success)
            {
                Favorites = result.Response.ToObservable<RandomUser>();
            }
        }

        public async Task AddToFavorites(RandomUser user)
        {
            var result = await this.SqliteDb.AddOrUpdate<RandomUser>(user);
            Log.LogResponse(result);
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
            var result = await this.SqliteDb.DeleteByInternalID<RandomUser>(user.CorrelationID);
            Log.LogResponse(result);
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
                if (temp.Success)
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
