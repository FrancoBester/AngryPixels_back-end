using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using application_programming_interface.Models;
using application_programming_interface.Interfaces;
using application_programming_interface.Services;
using System;

namespace application_programming_interface
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
            services.AddControllers();
            if (Configuration.GetConnectionString("DefaultConnections") == null)
            {
                throw new Exception("Please add the coonnection strings to the appsettings.json");
            }
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnections")));
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddCors(x=>x.AddPolicy("AllowAllHeaders", builder => builder.AllowAnyOrigin()
                                                                .AllowAnyHeader()
                                                                .AllowAnyMethod()
                                                                .AllowAnyOrigin()
                                                                .WithOrigins("https://www.carnagehosting.com/",
                                                                             "https://localhost/")
                                                                .SetIsOriginAllowedToAllowWildcardSubdomains()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAllHeaders");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
