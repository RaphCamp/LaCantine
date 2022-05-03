using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LaCantine.Data;
using LaCantine.Model;
using LaCantine.Service;
using SampleJwtApp.Security.Services;
using Microsoft.AspNetCore.Identity;
using SampleJwtApp.Security.DataAccess;

namespace LaCantine
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LaCantine", Version = "v1" });
            });

            services.AddDbContext<LaCantineContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("LaCantineContext")));
            services.AddScoped<ICommandesService, CommandesService>();
            services.AddScoped<ICommandesRepository, DBCommandesRepository>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMenusRepository, DBMenuRepository>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddControllers();

            // Add Identity implementation
            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>();

            // Add JWT authentication
            //services
            //    .AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(options =>
            //    {
            //        options.SaveToken = true;
            //        options.RequireHttpsMetadata = false;
            //        options.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            ValidateIssuerSigningKey =
            //                bool.Parse(Configuration["JsonWebTokenKeys:ValidateIssuerSigningKey"]),
            //            IssuerSigningKey =
            //                new SymmetricSecurityKey(
            //                    Encoding.UTF8.GetBytes(Configuration["JsonWebTokenKeys:SymmetricKey"])),
            //            ValidateIssuer = bool.Parse(Configuration["JsonWebTokenKeys:ValidateIssuer"]),
            //            ValidAudience = Configuration["JsonWebTokenKeys:ValidAudience"],
            //            ValidIssuer = Configuration["JsonWebTokenKeys:ValidIssuer"],
            //            ValidateAudience = bool.Parse(Configuration["JsonWebTokenKeys:ValidateAudience"]),
            //            RequireExpirationTime = bool.Parse(Configuration["JsonWebTokenKeys:RequireExpirationTime"]),
            //            ValidateLifetime = bool.Parse(Configuration["JsonWebTokenKeys:ValidateLifetime"])
            //        };
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LaCantine v1"));
            }

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
