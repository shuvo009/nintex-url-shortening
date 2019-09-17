using System;
using System.Text;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Filtering;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nintex.Url.Shortening.Core.Interfaces.Auth;
using Nintex.Url.Shortening.Core.Utility;
using Nintex.Url.Shortening.DependencyInjection;
using Nintex.Url.Shortening.Repository.DbContext;
using Nintex.Url.Shortening.Web.Middleware;
using Swashbuckle.AspNetCore.Swagger;

namespace Nintex.Url.Shortening.Web
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
            services.AddCors();

            services.AddDbContext<ShortUrlDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            new DependencyServiceRegister().Register(services);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var currentUser =
                                context.HttpContext.RequestServices.GetRequiredService<ICurrentLoginUser>();
                            currentUser.SetClaims(context.Principal.Claims);
                            return Task.CompletedTask;
                        }
                    };
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    var key = Encoding.ASCII.GetBytes(ApplicationVariable.JwtSigninKey);
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvcCore().AddMetricsCore();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c => {  
                c.SwaggerDoc("v1", new Info {  
                    Version = "v1",  
                    Title = "Short URL API",  
                    Description = "Nintex Short URL"  
                });  
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            try
            {
                using (var serviceScope =
                    app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<ShortUrlDbContext>().Database.Migrate();
                }
            }
            catch (Exception ex)
            {
            }

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Error");

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvc();

            app.UseSwagger();  
            app.UseSwaggerUI(c => {  
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Short URL API");  
                c.RoutePrefix = "docs/swagger";
            });  
        }
    }
}