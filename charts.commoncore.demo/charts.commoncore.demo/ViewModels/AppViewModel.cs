using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microcharts;
using Xamarin.Forms.CommonCore;

namespace charts.commoncore.demo
{
    public class AppViewModel : CoreViewModel
    {
        public ObservableCollection<Nationality> Nationalities { get; set; }
        public ObservableCollection<RandomUser> Users { get; set; }

        public DonutChart GenderChart { get; set; }
        public BarChart AgeChart { get; set; }
        public LineChart NationalitiesChart { get; set; }
        public PointChart NationalitiesPointChart { get; set; }

        public ICommand GetNationalityData { get; private set; } 

        public AppViewModel()
        {
            GetNationalityData = new CoreCommand(LoadNationalityData);
        }

        private void LoadResources()
        {
            this.LoadingMessageHUD = "Loading...";
            this.IsLoadingHUD = true;
            this.AppLogic.LoadRandomUsers().ContinueWith((result) => {
                this.IsLoadingHUD = false;
                var response = result.Result;
                if (response.Error == null)
                {
                    Users = AppLogic.ObservableRandomUsers;
                
                    GenderChart = new DonutChart()
                    {
                        Entries =AppLogic.GetUsersByGender()
                    };
                    AgeChart = new BarChart()
                    {
                        Entries = AppLogic.GetUsersByAgeGroup()
                    };

                    Nationalities = AppLogic.GetUniqueNationalities().ToObservableCollection();
                }
                else{
                    DialogPrompt.ShowMessage(new Prompt(){
                        Title = "Error",
                        Message = response.Error.Message
                    });
                }
            });

        }

        private void ReleaseResources()
        {
            Nationalities?.Dispose();
            Users?.Dispose();

            if (GenderChart != null)
                GenderChart = null;
            if (NationalitiesChart != null)
                NationalitiesChart = null;
            if (AgeChart != null)
                AgeChart = null;
        }

        private void LoadNationalityData(object obj)
        {
            var selected = Nationalities.Where((x => x.IsSelected == true)).ToArray();
            var entries = this.AppLogic.GetUserByNationality(selected);
            NationalitiesChart = new LineChart()
            {
                Entries = entries
            };
            NationalitiesPointChart = new PointChart()
            {
                Entries = entries
            };
            Navigation.PushNonAwaited<NationalityChartPage>();
        }

        public override void OnViewMessageReceived(string key, object obj)
        {
            switch(key){
                case CoreSettings.LoadResources:
                    LoadResources();
                    break;
                case CoreSettings.ReleaseResources:
                    ReleaseResources();
                    break;
            }
        }
    }
}
