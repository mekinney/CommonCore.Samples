using System;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore.Styles;

namespace tabbedReference
{
    public class AppStyles : CoreStyles
    {
        private static Style addressCell;


        public static Style AddressCell
        {
            get
            {
                return addressCell ?? (
                    addressCell = new Style(typeof(Label))
                    {
                        Setters ={
                            new Setter(){Property=Label.TextProperty ,Value=Color.Gray},
                            new Setter(){Property=Label.FontSizeProperty ,Value=12},
                            new Setter(){Property=Label.MarginProperty ,Value=new Thickness(5,0,2,0)}
                        }
                    });
            }

        }
    }
}
