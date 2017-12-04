using System;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace sqliteDemo
{
	public class AppStyles: CoreStyles
	{
        private static Style defaultButton;
		private static Style defaultLabel;
        private static Style defaultListView;
        private static Style defaultEntry;

        public static Style DefaultButton
        {
            get
            {
                return defaultButton ?? (defaultButton = new Style(typeof(Button)){
                    Setters ={
                        new Setter(){Property=Button.TextProperty,Value=Color.White},
                        new Setter(){Property=Button.BackgroundColorProperty,Value=Color.Black},
                        new Setter(){Property=Button.MarginProperty ,Value=new Thickness(5,5,5,5)}
                    }
                });  
            }
        }

		public static Style DefaultLabel
		{
			get
			{
				return defaultLabel ?? (defaultLabel = new Style(typeof(Label))
				{
                    Setters ={
                        new Setter(){Property=Label.TextProperty,Value=Color.White},
                        new Setter(){Property=Label.BackgroundColorProperty,Value=Color.Black},
                        new Setter(){Property=Label.MarginProperty ,Value=new Thickness(5,5,5,5)}
                    }
				});
			}
		}

		public static Style DefaultListView
		{
			get
			{
				return defaultListView ?? (defaultListView = new Style(typeof(ListView))
				{
                    Setters ={
                        new Setter(){Property=ListView.MarginProperty ,Value=new Thickness(5,5,5,5)}
                    }
				});
			}
		}

		public static Style DefaultEntry
		{
			get
			{
				return defaultEntry ?? (defaultEntry = new Style(typeof(Entry))
				{
                    Setters ={
                        new Setter(){Property=Entry.MarginProperty ,Value=new Thickness(5,5,5,5)}
                    }
				});
			}
		}
        
	}
}
