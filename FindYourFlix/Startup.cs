using FindYourFlix.Business.Movies;
using FindYourFlix.Business.Tags;
using FindYourFlix.Business.Users;
using FindYourFlix.Data;
using FindYourFlix.Data.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace FindYourFlix
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
            services.AddTransient<TagInsert>();
            services.AddTransient<TagDelete>();
            services.AddTransient<TagList>();
            services.AddTransient<MovieList>();
            services.AddTransient<MovieSelect>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}