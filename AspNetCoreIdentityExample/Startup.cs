using AFS.Business.CustomExtensions;
using AFS.Core;
using AFS.Core.DataAccess.EntityFramework;
using AFS.DataAccess.Models.Context;
using AspNetCoreIdentityExample.Models.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentityExample
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext2>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("AFS.DataAccess")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext2>();
            services.AddMvc();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<DbContext>(provider => provider.GetService<AppDbContext2>());
            services.AddScoped(typeof(IEntityRepository<>), typeof(efRepositoryBase<>));

            services.AddBusinessModule(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(_ => _.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}"));
        }
    }
}
