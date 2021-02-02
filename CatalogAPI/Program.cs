using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CatalogAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //to call "Seed" method from CatalogSeed.cs. We don't call it from Startup.cs because 
            //when the microservice and database are deployed on different Docker containers
            //the "services.AddDbContext.." command starts building the container for db.If seed 
            //method is called after that line then it might fail if the container for db is not up 
            //and running yet. So we call it from Program.cs to make sure containers are ready to go.
            
            var host = CreateHostBuilder(args).Build(); 

            //before running the appl, we seed the data by calling the "Seed" method:
            using (var scope = host.Services.CreateScope())
            {
                var serviceProviders = scope.ServiceProvider;
                var context = serviceProviders.GetRequiredService<CatalogContext>(); //this line ensures db VM is built and ready to go.
                CatalogSeed.Seed(context);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
