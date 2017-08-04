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


		public ObservableCollection<RandomUser> RandomUsers
		{
			get { return randomUsers ?? (randomUsers = new ObservableCollection<RandomUser>()); }
			set { SetProperty(ref randomUsers, value); }
		}

        public AppViewModel()
        {
            RandomUsers = new ObservableCollection<RandomUser>();

            GetRandomUsers().ContinueWith((t) => { });
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
