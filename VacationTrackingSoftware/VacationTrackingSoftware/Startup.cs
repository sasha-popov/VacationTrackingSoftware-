using AutoMapper;
using BLL.IRepositories;
using BLL.Services;
using DAL.Data;
using DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VacationTrackingSoftware.AutoMapper;

namespace VacationTrackingSoftware
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
            //services.AddDbContext<ProjectContext>(option =>
            //    option.UseSqlServer(Configuration.GetConnectionString("Defaultconnection")));
            services.AddDbContext<ProjectContext>(options =>
      options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            //services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITeamUserRepository, TeamUserRepository>();
            //services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserVacationRequestRepository, UserVacationRequestRepository>();
            services.AddScoped<IVacationPolicyRepository, VacationPolicyRepository>();
            services.AddScoped<IVacationTypeRepository, VacationTypeRepository>();
            services.AddScoped<ICompanyHolidayRepository, CompanyHolidayRepository > ();


            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IVacationPoliciesService, VacationPoliciesService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICompanyHolidayService, CompanyHolidayService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
