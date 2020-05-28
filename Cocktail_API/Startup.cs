using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Google;
using System;

namespace Cocktail_API
{
    public class Startup
    {
        private readonly RecipesContext recipesContext;
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddDbContext<RecipesContext>(
            //    options => options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")
            //        )
            //);

            services.AddDbContext<RecipesContext>(
                options => options.UseMySQL(
                    Configuration.GetConnectionString("DefaultConnection")
                )
            );

            services.AddCors(o => o.AddPolicy(name: MyAllowSpecificOrigins, builder =>
               {
                   builder
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
               }));
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
                //options.Authority = "https://accounts.google.com";

                //options.Audience = "998171199839-2061ud931cfaqgckitsfimod47c8nkhn.apps.googleusercontent.com";
                ////options.requirehttpsmetadata = false;

            });
            /*
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Audience = this.recipesContext.Cocktails;
            });*/

                //services.AddAuthentication(options =>
                //{
                //    options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                //    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                //})
                //.AddGoogle(options =>
                //{
                //    options.ClientId = "998171199839-2061ud931cfaqgckitsfimod47c8nkhn.apps.googleusercontent.com";
                //    options.ClientSecret = "mEsW0g5jL5uxhgv0WAdBQiiG";
                //});


            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RecipesContext recContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            //app.UseHttpsRedirection();

            app.UseRouting();


            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            DBInitializer.Initialize(recContext);
        }
    }
}
