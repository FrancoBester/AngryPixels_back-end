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
using Azure.Storage.Blobs;
using MailKit.Net.Smtp;
using MailKit.Security;


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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            if (Configuration.GetConnectionString("DefaultConnections") == null)
            {
                throw new Exception("Please add the coonnection strings to the appsettings.json");
            }
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnections")));
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IQueriesService, QueriesService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISchemaRequestService, SchemaRequestService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton(x => new BlobServiceClient(Configuration.GetConnectionString("StorageAccount")));
            services.AddScoped<IBlobStorageService, BlobStorageService>();
            services.AddCors(x=>x.AddPolicy("AllowAllHeaders", builder => builder.AllowAnyOrigin()
                                                                .AllowAnyHeader()
                                                                .AllowAnyMethod()
                                                                .AllowAnyOrigin()
                                                                .WithOrigins("https://localhost/")
                                                                .SetIsOriginAllowedToAllowWildcardSubdomains()));
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, Services.MailService>();

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
