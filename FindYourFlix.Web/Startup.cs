using System;
using System.Text;
using FindYourFlix.Business;
using FindYourFlix.Business.Movies;
using FindYourFlix.Business.Tags;
using FindYourFlix.Business.Users;
using FindYourFlix.Data;
using FindYourFlix.Data.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace FindYourFlix.Web
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
            services.AddTransient<UserInsert>();
            services.AddTransient<UserList>();
            services.AddTransient<UserDelete>();
            services.AddTransient<UserLikeAction>();
            services.AddTransient<UserSelect>();
            services.AddTransient<UserUpdate>();
            services.AddTransient<UserUpdatePassword>();
            services.AddTransient<UserRoleUpdate>();
            services.AddTransient<TagInsert>();
            services.AddTransient<TagDelete>();
            services.AddTransient<TagList>();
            services.AddTransient<MovieList>();
            services.AddTransient<MovieSelect>();
            services.AddTransient<MovieInsert>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IUserInfo, UserInfo>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddRazorPages();
            services.AddControllers();
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
            });
           services.AddDbContext<ApplicationContext>();
           services.AddCors(options =>
           {
               options.AddPolicy("AllowAll", p =>
               {
                   p.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
               });
           });
           
           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = Configuration["Jwt:Issuer"],
                       ValidAudience = Configuration["Jwt:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                   };
               });
           
           // services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

           // In production, the Angular files will be served from this directory
           services.AddSpaStaticFiles(configuration =>
           {
               configuration.RootPath = "ClientApp/dist";
           });
           services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowAll");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
            app.UseAuthentication();
            
            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(
            //         name: "default",
            //         template: "{controller}/{action=Index}/{id?}");
            // });

            // app.UseSpa(spa =>
            // {
            //     spa.Options.SourcePath = "ClientApp";
            //
            //     if (env.IsDevelopment())
            //     {
            //         spa.UseAngularCliServer(npmScript: "start");
            //     }
            // });
        }
    }
}