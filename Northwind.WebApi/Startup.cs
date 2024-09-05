using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.DataAccess;
using Northwind.UnitOfWork;
using Northwind.Web.Authentication;
using Northwind.WebApi.GlobalErrorHanding;

namespace Northwind.Web
{
    public class Startup
    {
        readonly string allowSpecificOrigins = "_allowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
         

            services.AddSingleton<IUnityOfWork>(option => new NorthwindUnitOfWork(Configuration.GetConnectionString("Northwind")));

            var tokenProvider = new JwtProvider("issuer", "audience", "northwind");
            services.AddSingleton<ITokenProvider>(tokenProvider);


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = tokenProvider.GetValidationParameters();
                    });
            services.AddAuthorization(auth =>
            {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors(options =>

            {

                options.AddPolicy(allowSpecificOrigins,

                builder =>

                {

                    builder.AllowAnyOrigin()

                            .AllowAnyHeader()

                            .AllowAnyMethod();

                });

            });
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.ConfigureExceptionHandler();
            app.UseMvc();

            app.UseCors(allowSpecificOrigins);
        }
    }
}
