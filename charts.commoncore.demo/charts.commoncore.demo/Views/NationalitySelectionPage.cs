using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace charts.commoncore.demo
{
    public class BoolToImageConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isSelected = (bool)value;
            if (isSelected)
                return "checked.png";
            else
                return "unchecked.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class NationalitySelectionPage: CorePage<AppViewModel>
    {
        private BoolToImageConvert imageConverter = new BoolToImageConvert();
        public NationalitySelectionPage()
        {
            this.Title = "Options";

            var optionLabel = new Label()
            {
                Text="Nationality Options",
                TextColor = Color.LightGray
            };

            var lst = new CoreListView(ListViewCachingStrategy.RecycleElement)
            {
                RowHeight=45,
                MaintainSelection=false,
                ItemTemplate = new DataTemplate(typeof(NationalityCell))
            };
            lst.ItemTapped += (e, a) =>
            {
                ((Nationality)a.Item).IsSelected = !((Nationality)a.Item).IsSelected;
            };
            lst.SetBinding(CoreListView.ItemsSourceProperty, "Nationalities");

            var btnProceed = new Button()
            {
                Text = "View Chart"
            };
            btnProceed.SetBinding(Button.CommandProperty,"GetNationalityData");

            Content = new StackLayout()
            {
                Children = { optionLabel, lst, btnProceed }
            };
        }
    }
}
