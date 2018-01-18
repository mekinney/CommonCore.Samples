using System;
using System.IO;
using pdfmaker.Controls;
using pdfmaker.Dependencies;
using Xamarin.Forms;

namespace pdfmaker
{
    public class AppSettings
    {
        public static double Height { get; set; }
    }
    public class TestPage:ContentPage
    {
        private CoreWebView wv;

        public TestPage()
        {
            var urlSource = new UrlWebViewSource
            {
                Url = "https://blog.xamarin.com/",
            };
            
            wv = new CoreWebView()
            {
                Source = urlSource,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            this.ToolbarItems.Add(new ToolbarItem(){
                Text="Save", 
                Command=new Command(async(obj) => {
                    //Method 1
                    //wv.CreatePDF.Invoke((success) => {
                    //    var result = success;
                    //});

                    //Method 2
                    var success = await wv.CreatePDFAsync();
                    if (success)
                    {
#if __IOS__
                        DependencyService.Get<IFileReader>().Read(wv.FullPath);
#else
                        await this.DisplayAlert("Success", "PDF was successfully printed", "Cancel");
#endif
                    }
                })
            });

            this.Content = new StackLayout()
            {
                Children = { wv }
            };
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            AppSettings.Height = height;
            base.OnSizeAllocated(width, height);
        }


    }
    public class App : Application
    {
        public App()
        {

            MainPage = new NavigationPage(new TestPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
