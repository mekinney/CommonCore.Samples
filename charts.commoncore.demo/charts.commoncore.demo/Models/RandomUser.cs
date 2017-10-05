using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace charts.commoncore.demo
{
    public class RandomUser: BindableObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string NAT { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}".ToTitleCase(); }
        }

        public int Age
        {
            get 
            {
                var dateOfBirth = DateTime.Parse(DOB);
                var today = DateTime.Today;
                var a = (today.Year * 100 + today.Month) * 100 + today.Day;
                var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
                return (a - b) / 10000;
            }
        }

        public FormattedString FullAddress
        {

            get
            {
                var fs = new FormattedString();
                fs.AddTextSpan($"{Address} \n");
                fs.AddTextSpan($"{City}, {State}   {Zip} \n");
                fs.AddTextSpan($"{Phone}");
                return fs;
            }
        }
    }

}
