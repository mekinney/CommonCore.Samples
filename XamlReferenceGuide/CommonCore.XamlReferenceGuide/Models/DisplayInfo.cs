using System;
using Xamarin.Forms;

namespace CommonCore.XamlReferenceGuide.Models
{
    public class DisplayInfo: BindableObject
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
    }
}
