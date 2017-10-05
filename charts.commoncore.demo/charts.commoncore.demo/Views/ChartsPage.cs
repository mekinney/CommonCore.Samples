using System;
using Microcharts;
using Microcharts.Forms;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace charts.commoncore.demo
{
    public class ChartsPage: BoundPage<AppViewModel>
    {
        public ChartsPage()
        {
            this.Title = "Charts";
            this.ToolbarItems.Add(new ToolbarItem("More", null,
                () =>
                {
                    Navigation.PushNonAwaited<NationalitySelectionPage>();
                })
            {
                AutomationId = "More"
            });

            var ageLabel = new Label()
            {
                Text= "Age Ranges:",
                TextColor = Color.LightGray
            };
            var ageChart = new ChartView()
            {
                 
                VerticalOptions = LayoutOptions.FillAndExpand, 
                Margin=10
            };
            ageChart.SetBinding(ChartView.ChartProperty, "AgeChart");

            var genderLabel = new Label()
            {
                Text = "Gender Ratio:",
                TextColor = Color.LightGray
            };
            var genderChart = new ChartView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin=10
            };
            genderChart.SetBinding(ChartView.ChartProperty, "GenderChart");

            var nationalitiesChart = new ChartView();
            nationalitiesChart.SetBinding(ChartView.ChartProperty, "NationalitiesChart");

            Content = new StackLayout()
            {
                Padding = 20,
                Children = { ageLabel, ageChart, genderLabel, genderChart }
            };
        }

    }
}
