using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microcharts;
using Xamarin.Forms.CommonCore;

namespace charts.commoncore.demo
{
    public class AppViewModel : ObservableViewModel
    {

        public ObservableCollection<Nationality> Nationalities { get; set; }
        public ObservableCollection<RandomUser> Users { get; set; }

        public DonutChart GenderChart { get; set; }
        public BarChart AgeChart { get; set; }
        public LineChart NationalitiesChart { get; set; }
        public PointChart NationalitiesPointChart { get; set; }

        public ICommand GetNationalityData { get; set; }

        public AppViewModel()
        {
            GetNationalityData = new RelayCommand((obj) => { LoadNationalityData(); });
        }

        public override void LoadResources(string parameter = null)
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

        public override void ReleaseResources(string parameter = null)
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

        private void LoadNationalityData()
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
    }
}
