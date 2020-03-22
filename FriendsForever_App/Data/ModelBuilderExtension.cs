using FriendsForever_App.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region State Data Seeding
            using (StreamReader reader = new StreamReader("Data/Country.json"))
            {
                List<Country> CountryList = new List<Country>();
                string json = reader.ReadToEnd();
                var jObject = JObject.Parse(json);
                JArray sheetList = (JArray)jObject["data"];
                for (int i = 0; i < sheetList.Count; i++)
                {
                    int countryId = (int)sheetList[i]["CountryId"];
                    string countryName = (string)sheetList[i]["CountryName"];
                    string countryCode = (string)sheetList[i]["CountryCode"];
                    CountryList.Add(new Country
                    {
                        CountryId = countryId,
                        CountryName = countryName,
                        CountryCode = countryCode
                    });
                }
                modelBuilder.Entity<Country>().HasData(CountryList);
            }
            #endregion
        }
    }
}
