using System;
using System.Text;
using AutoMapper;
using BLL.IRepositories;
using BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using VacationTrackingSoftware.AutoMapper;
using VacationTrackingSoftware.Token;
using VacationTrackingSoftware.Helpers;
using VacationTrackingSoftware.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using BLL.Services.Interfaces;
using BLL.Services.Classes;
using DAL.Data;
using DAL.Repositories;

namespace VacationTrackingSoftware
{
    public class Startup
    {

        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            //disable automatic validation
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            //EF
            services.AddDbContext<ProjectContext>(options =>
      options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //ADONET
            // services.AddDbContext<AppUserContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("defaultconnection")));


            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITeamUserRepository, TeamUserRepository>();
            services.AddScoped<IUserVacationRequestRepository, UserVacationRequestRepository>();
            services.AddScoped<IVacationPolicyRepository, VacationPolicyRepository>();
            services.AddScoped<IVacationTypeRepository, VacationTypeRepository>();
            services.AddScoped<ICompanyHolidayRepository, CompanyHolidayRepository > ();

            services.AddScoped<IVacationRequestService, VacationRequestService>();
            services.AddScoped<IVacationPoliciesService, VacationPoliciesService>();
            services.AddScoped<IWorkerService, WorkerService>();
            services.AddScoped<ICompanyHolidayService, CompanyHolidayService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<ITeamService, TeamService>();
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IJwtFactory, JwtFactory>();
            // add identity
            var builder = services.AddIdentity<AppUser, IdentityRole>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);

            //For EF
            builder.AddEntityFrameworkStores<ProjectContext>().AddDefaultTokenProviders();
            //For AdoNet
            //builder.AddEntityFrameworkStores<AppUserContext>().AddDefaultTokenProviders();
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

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

            var keyAsBytes = Encoding.ASCII.GetBytes("mysuperdupersecret");

            var tokenValidationParameters = new TokenValidationParameters
            {        
                
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(configureOptions =>
                {
                    configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                    configureOptions.TokenValidationParameters = tokenValidationParameters;
                    configureOptions.SaveToken = true;
                });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });
            services.AddSingleton(mapper);
            services.AddAutoMapper();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseCors(builder => {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
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
