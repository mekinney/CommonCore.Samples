using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace charts.commoncore.demo
{
    public static class AppExtension
    {
        public static void Dispose(this IList list)
        {
            list.Clear();
            list = null;
        }
        public static ObservableCollection<RandomUser> ToRandomUserObservableCollection(this List<Result> list)
        {
            var collection = new ObservableCollection<RandomUser>();
            foreach (var item in list)
            {

                collection.Add(new RandomUser()
                {
                    FirstName = item.name.first,
                    LastName = item.name.last,
                    ImageUrl = item.picture.medium,
                    Address = item.location.street,
                    City = item.location.city,
                    State = item.location.state,
                    Zip = item.location.postcode,
                    Phone = item.cell,
                    Gender = item.gender,
                    DOB = item.dob,
                    NAT = item.nat
                });
            }
            return collection;
        }

        public static List<RandomUser> ToRandomUserList(this List<Result> list)
        {
            var collection = new List<RandomUser>();
            foreach (var item in list)
            {

                collection.Add(new RandomUser()
                {
                    FirstName = item.name.first,
                    LastName = item.name.last,
                    ImageUrl = item.picture.medium,
                    Address = item.location.street,
                    City = item.location.city,
                    State = item.location.state,
                    Zip = item.location.postcode,
                    Phone = item.cell,
                    Gender = item.gender,
                    DOB = item.dob,
                    NAT = item.nat
                });
            }
            return collection;
        }
    }
}
