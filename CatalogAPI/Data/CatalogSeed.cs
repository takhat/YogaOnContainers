using CatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogAPI.Data
{
    public static class CatalogSeed
    {
        // After migration, create/update database automatically 
        public static void Seed(CatalogContext catalogContext)
        {
            catalogContext.Database.Migrate();

            //After generating the db:

            //if CatalogTypes table is empty, create a list of Catalog Types 
            //using GetCatalogTypes() method:
            if (!catalogContext.CatalogTypes.Any())
            {
                catalogContext.CatalogTypes.AddRange(GetCatalogTypes());
                catalogContext.SaveChanges();
            }

            //if CatalogItems table is empty
            if (!catalogContext.CatalogItems.Any())
            {
                catalogContext.CatalogItems.AddRange(GetCatalogItems());
                catalogContext.SaveChanges();
            }
        }

        //return a new List i.e. array of Catalog Types where each 
        //item in the list will have an Id and a Catalog Type
        private static IEnumerable<CatalogType> GetCatalogTypes()
        {
            return new List<CatalogType>
            {
                new CatalogType
                {
                    Type = "Adults"
                },

                new CatalogType
                {
                    Type = "Kids and Teens"
                }
            };
        }

        //return a new List i.e. array of Catalog Items where each list-item
        //will have an Id, Name, Description, Price, Pic Url and Catalog type
        private static IEnumerable<CatalogItem> GetCatalogItems()
        {
            return new List<CatalogItem>
            {
                new CatalogItem
                {
                    Name="Daily 25-min Morning Yoga on Weekdays",
                    CatalogTypeId = 1,
                    Description = "The Hatha Yoga is a moderate intensity mini-yoga class that combines breath, mindfulness and movement to help build physical and emotional strength, endurance, balance, flexibility, immunity and overall sense of wellbeing. It aims to combat stress and fatigue while improving positivity and vitality.",
                    Timings ="Weekdays 7:30-7:55 AM except major holidays",
                    Price = 85,
                    PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1",
                },

                new CatalogItem
                {
                    Name="Weekly 60-min After-School Yoga for Teens and Tweens",
                    CatalogTypeId = 2,
                    Description = "This class features age-appropriate postures, incorporating mindfulness and breathing exercises, aimed at improving focus, flexibility, strength and stamina, calming the mind, boosting immunity and supporting healthy physical and emotional growth of teens and tweens.",
                    Timings ="Fridays 4:15-5:15 PM except major holidays",
                    Price = 50,
                    PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2",
                }
            };
        }
    } 
}

