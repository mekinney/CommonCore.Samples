using System;
using Microcharts.Forms;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace charts.commoncore.demo
{
    public class NationalityChartPage: CorePage<AppViewModel>
    {
        public NationalityChartPage()
        {
            var natLabel = new Label()
            {
                Text = "Nationality Ranges:",
                TextColor = Color.LightGray
            };

            var nationalityChart = new ChartView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            nationalityChart.SetBinding(ChartView.ChartProperty, "NationalitiesChart");


            var natPointLabel = new Label()
            {
                Text = "Nationality Point Ranges:",
                TextColor = Color.LightGray
            };
            var nationalityPointChart = new ChartView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            nationalityPointChart.SetBinding(ChartView.ChartProperty, "NationalitiesPointChart");


            Content = new StackLayout()
            {
                Padding = 10,
                Children = { natLabel, nationalityChart, natPointLabel, nationalityPointChart }
            };
        }
    }
}
