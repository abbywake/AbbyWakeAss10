using AbbyWakeAss10.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbbyWakeAss10
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<BowlingLeagueContext>(options =>
            options.UseSqlite(Configuration["ConnectionStrings:BowlingDBConnection"]));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //endpoints, the ? mark makes it not required so you can click on the teamname and it will show up bolded. 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("teamNamePageNum", 
                    "TeamName/{team}/{teamName}/{pageNum?}",
                    new { Controller = "Home", Action = "Index" }
                    );
                endpoints.MapControllerRoute("teamName",
                    "Team/{team}", 
                    new {Controller = "Home", Action = "Index"}
                    );
                endpoints.MapControllerRoute("pageNum", 
                    "{pageNum}",
                    new { Controller = "Home", Action = "Index" }
                    );
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
