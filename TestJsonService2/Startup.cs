using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestJsonService2.Models;

namespace TestJsonService2 {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string CorsPolicy = "AppCorsPolicy";
        readonly string[] AllowedOrigins = new string[] {
            "http://localhost", "https://localhost",
            "http://localhost:4200", "https://localhost:4200",
            "http://localhost:5500", "https://localhost:5500"
        };
        readonly string[] AllowedMethods = new string[] { "GET", "POST", "PUT", "DELETE" };

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
#if DEBUG
            var connStrKey = "DevAppDb";
#else
            var connStrKey = "ProdAppDb";
#endif
            services.AddDbContext<AppDbContext>(x => {
                x.UseSqlServer(Configuration.GetConnectionString(connStrKey));
            });
            services.AddCors(x => {
                x.AddPolicy(CorsPolicy, x => {
                    x.WithOrigins(AllowedOrigins)
                    .WithMethods(AllowedMethods)
                    .AllowAnyHeader();
                });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(CorsPolicy);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            using(var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) {
                scope.ServiceProvider.GetService<AppDbContext>().Database.Migrate();
            }
        }
    }
}
