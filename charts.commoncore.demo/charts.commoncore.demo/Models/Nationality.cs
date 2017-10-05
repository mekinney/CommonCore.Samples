using System;
using Xamarin.Forms;

namespace charts.commoncore.demo
{
    public class Nationality: BindableObject
    {
        public string Description { get; set; }
        public bool IsSelected { get; set; }
    }
}
