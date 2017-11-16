using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms.CommonCore;
using System.Linq;
using Xamarin.Forms;
using SkiaSharp;

namespace charts.commoncore.demo
{
    public class RandomUserLogic : CoreBusiness
    {
        private List<RandomUser> userList;

        private string[] colorWheel = new string[]{
            "#2c3e50",
            "#77d065",
            "#b455b6",
            "#3498db",
            "#98b6b6",
            "#df744a",
            "#4f666a",
            "#228b22",
            "#ffff00"
        };

        public ObservableCollection<RandomUser> ObservableRandomUsers
        {
            get { return userList?.ToObservableCollection(); }
        }

        public async Task<(bool Response, Exception Error)> LoadRandomUsers()
        {
            var url = this.WebApis["randomuser"];
            var result = await this.HttpService.Get<RootObject>(url);
            result.Error?.LogException("RandomUserLogic - LoadRandomUsers");
            if (result.Error == null)
            {
                userList = result.Response.results.ToRandomUserList();
                return (true, null);
            }
            else
            {
                return (false, result.Error);
            }
        }

        public List<Nationality> GetUniqueNationalities()
        {
            var list = new List<Nationality>();
            var nats = userList?.Select(x => x.NAT).Distinct().OrderBy(x => x).ToList();
            nats?.ForEach(item => list.Add(new Nationality() { Description = item }));
            return list;
        }
        public Microcharts.Entry[] GetUsersByAgeGroup()
        {
            var twentyCount = userList.Count(x => x.Age <= 29);
            var thirtyCount = userList.Count(x => x.Age >= 30 && x.Age <= 39);
            var fortyCount = userList.Count(x => x.Age >= 40 && x.Age <= 49);
            var fiftyPlusCount = userList.Count(x => x.Age >= 50);

            var data = new[]
            {
                new Microcharts.Entry(twentyCount)
                 {
                     Label = "Twenties",
                     ValueLabel = twentyCount.ToString(),
                     Color = SKColor.Parse(colorWheel[0])
                 },
                new Microcharts.Entry(thirtyCount)
                 {
                     Label = "Thirties",
                     ValueLabel = thirtyCount.ToString(),
                     Color = SKColor.Parse(colorWheel[1])
                 },
                new Microcharts.Entry(fortyCount)
                 {
                     Label = "Forties",
                     ValueLabel = fortyCount.ToString(),
                     Color = SKColor.Parse(colorWheel[2])
                 },
                new Microcharts.Entry(fiftyPlusCount)
                 {
                     Label = "Over Fifty",
                     ValueLabel = fiftyPlusCount.ToString(),
                     Color = SKColor.Parse(colorWheel[3])
                }
            };
            return data;
        }
        public Microcharts.Entry[] GetUserByNationality(Nationality[] nats)
        {

            var list = new List<Microcharts.Entry>();
            var index = 0;
            foreach (var nat in nats)
            {
                var cnt = userList.Count(x => x.NAT == nat.Description);
                list.Add(
                    new Microcharts.Entry(cnt)
                    {
                        Label = nat.Description,
                        ValueLabel = cnt.ToString(),
                        Color = SKColor.Parse(colorWheel[index])
                    }
                );
                index++;

                if (index == 8)
                    break;
            }
            return list.ToArray();
        }
        public Microcharts.Entry[] GetUsersByGender()
        {
            var maleCount = userList.Count(x => x.Gender == "male");
            var femaleCount = userList.Count(x => x.Gender == "female");
            var data = new[]
            {
                new Microcharts.Entry(maleCount)
                 {
                     Label = "Males",
                     ValueLabel = maleCount.ToString(),
                     Color = SKColor.Parse(colorWheel[0])
                 },
                new Microcharts.Entry(femaleCount)
                 {
                     Label = "Females",
                     ValueLabel = femaleCount.ToString(),
                     Color = SKColor.Parse(colorWheel[1])
                 }
            };
            return data;
        }


        public override void Dispose()
        {
            userList?.Clear();
            if (userList != null)
                userList = null;
        }
    }
}
