using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Solution_RepositoryPattern.Core.Interfaces;
using Solution_RepositoryPattern.EFCore;
using Solution_RepositoryPattern.EFCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution_RepositoryPattern.API
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

            //add dbcontext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                                     b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            //allow Cors
            services.AddCors();

            //injection avant ajout unitOfwork
            //services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            
            services.AddTransient<IUnitOfWork,UnitOfWork>();
            
            //config de swagger
            services.AddSwaggerGen(options =>
            {
                #region config-Sawagger
                options.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Solution_RepositoryPattern.API",
                    Version = "v1",
                    Description ="Projet Pro Api",
                    TermsOfService =new Uri("https://www.google.com"),
                    Contact = new OpenApiContact
                    {
                        Name="Yacine.H",
                        Email="yacine.hayat@gmail.com",
                        Url =new Uri("https://www.google.com")
                    },
                    License =new OpenApiLicense
                    {
                        Name="My License",
                        Url = new Uri("https://www.google.com")
                    }
                    

                });
                #endregion
                #region security swagger
                ////1-security authorization for all endpoints 
                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer",
                //    BearerFormat = "JWT",
                //    In = ParameterLocation.Header,
                //    Description = "Entrer votre Key JWT"
                //});

                ////2-security authorization for individual endpoints
                //options.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference =new OpenApiReference
                //            {
                //               Type = ReferenceType.SecurityScheme,
                //               Id = "Bearer"
                //            },
                //            Name="Bearer",
                //            In = ParameterLocation.Header
                //        },
                //        new List<string>()
                //    }
                //});
                #endregion

            });

            //add automapper
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Solution_RepositoryPattern.API v1"));
            }

            app.UseRouting();

            //avant authentification
            app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
