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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LaCantine.Security.Services;
using System.Reflection;
using System.IO;

namespace LaCantine
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string corsOrigin = "corsOrigin";
        public IConfiguration Configuration { get; }
        public object WebApplication { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy(name: corsOrigin,
                    policy =>
                    {
                        policy
                .AllowAnyOrigin()
               // .WithOrigins(Configuration["Front:BaseUrl"])
                .AllowAnyMethod()
                .AllowAnyHeader();
                    });
            });

            services.AddControllers();
            //services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LaCantine", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            });

            services.AddDbContext<LaCantineContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("LaCantineContext")));
            services.AddScoped<ICommandesService, CommandesService>();
            services.AddScoped<ICommandesRepository, DBCommandesRepository>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMenusRepository, DBMenuRepository>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddControllers();
            services.AddScoped<IEmailSender, EmailSender>();

            // Add Identity implementation
            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<LaCantineContext>();

            //Add JWT authentication
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey =
                            bool.Parse(Configuration["JsonWebTokenKeys:ValidateIssuerSigningKey"]),
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Configuration["JsonWebTokenKeys:SymmetricKey"])),
                        ValidateIssuer = bool.Parse(Configuration["JsonWebTokenKeys:ValidateIssuer"]),
                        ValidAudience = Configuration["JsonWebTokenKeys:ValidAudience"],
                        ValidIssuer = Configuration["JsonWebTokenKeys:ValidIssuer"],
                        ValidateAudience = bool.Parse(Configuration["JsonWebTokenKeys:ValidateAudience"]),
                        RequireExpirationTime = bool.Parse(Configuration["JsonWebTokenKeys:RequireExpirationTime"]),
                        ValidateLifetime = bool.Parse(Configuration["JsonWebTokenKeys:ValidateLifetime"])
                    };
                });
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
            
            app.UseCors(corsOrigin);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
      
        
    }
}
