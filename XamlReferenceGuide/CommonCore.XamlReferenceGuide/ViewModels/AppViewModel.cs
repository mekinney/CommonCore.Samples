using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommonCore.XamlReferenceGuide.Models;
using CommonCore.XamlReferenceGuide.Views;
using Xamarin.Forms.CommonCore;
using System.Linq;

namespace CommonCore.XamlReferenceGuide
{
    public class AppViewModel : ObservableViewModel
    {
        public ObservableCollection<DisplayInfo> Data { get; set; } = new ObservableCollection<DisplayInfo>();
        public DisplayInfo SelectedDisplayInfo { get; set; }
        public ICommand ViewListControl { get; set; }
        public ICommand ViewBehaviors { get; set; }
        public ICommand ViewSelectedDisplay { get; set; }

        public AppViewModel()
        {
            PopulateData();

            ViewListControl = new RelayCommand((obj) => {
                Navigation.PushNonAwaited<ListControlPage>();
            });
            ViewBehaviors = new RelayCommand((obj) => { 
                Navigation.PushNonAwaited<BehaviorsPage>();
            });

            ViewSelectedDisplay = new RelayCommand((obj) => {
                if (SelectedDisplayInfo != null)
                {
                    this.DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Selected Item:",
                        Message = SelectedDisplayInfo.Title
                    });
                }
            });
        }

        private void PopulateData()
        {
            var list = new List<DisplayInfo>();
            list.Add(new DisplayInfo() { Image = "index24.png", Title = "Custom Data", SubTitle="Data that is custom" });
            list.Add(new DisplayInfo() { Image = "index24.png", Title = "Home Alone", SubTitle = "Not cool" });
            list.Add(new DisplayInfo() { Image = "index24.png", Title = "Nothing", SubTitle = "I need help" });
            list.Add(new DisplayInfo() { Image = "index24.png", Title = "Jack", SubTitle = "Get out of here" });
            list.Add(new DisplayInfo() { Image = "index24.png", Title = "Homey", SubTitle = "Nothing is working" });
            list.Add(new DisplayInfo() { Image = "index24.png", Title = "Oh Yea", SubTitle = "Eat more pizza" });
            Data = list.ToObservable<DisplayInfo>();
        }

    }
}
