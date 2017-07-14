using System;
using System.Collections.ObjectModel;
using providersearch.Models;
using System.Linq;

namespace providersearch.Extensions
{
    public static class AppExtensions
    {
        public static ObservableCollection<Specialities> ToObservableByCategory(this providersearch.Models.specialities.RootObject obj, string category)
        {
            var collection = new ObservableCollection<Specialities>();
            if(obj.data!=null&& obj.data.Count>0){
                var list = obj.data.Where(x => x.category == category.ToLower()).ToList();
                foreach(var item in list){
                    collection.Add(new Specialities(){
                         Category = item.category,
                         UID = item.uid,
                         Description = item.description,
                         Name = item.name
                    });
                }
            }
            return collection;
        }
    }
}
