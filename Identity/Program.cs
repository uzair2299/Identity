using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //The Build() function builds the application and if no errors are found then will run your application on the browser.
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //The UseStartup() function calls the Startup.cs class which is also kept on the application root folder.The Startup.cs class provides application specific configuration.The Program Class calls the Startup Class using UseStartup() function
                    webBuilder.UseStartup<Startup>();
                });
    }
}
