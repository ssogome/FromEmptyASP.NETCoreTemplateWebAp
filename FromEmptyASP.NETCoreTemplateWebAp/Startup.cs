using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FromEmptyASP.NETCoreTemplateWebAp.Persistence;
using FromEmptyASP.NETCoreTemplateWebAp.Persistence.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace FromEmptyASP.NETCoreTemplateWebAp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICountryRepository>(new CountryRepository());
            services.AddDirectoryBrowser();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ICountryRepository country)
        {
            //Enable serving files from the configured web root folder (i. e.; wwwroot)
            app.UseStaticFiles();
            //Enable serving files from \Pics located under the root folder of t he site
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "pics")),
                RequestPath = new PathString("/public/pics")
            });

            app.UseDirectoryBrowser();
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "pics"))
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var query = context.Request.Query["q"];
                var listOfCountries = country.AllBy(query).ToList();
                var json = JsonConvert.SerializeObject(listOfCountries);
                await context.Response.WriteAsync("Hello World! \n" + "Courtesy of <b>Programming ASP.NET Core</b>!" + "<hr>" +
                    "ENVIRONMENT=" + env.EnvironmentName + " \n The path is: " + context.Request.Path  +"\n  " + json);
            });
        }
    }
}
