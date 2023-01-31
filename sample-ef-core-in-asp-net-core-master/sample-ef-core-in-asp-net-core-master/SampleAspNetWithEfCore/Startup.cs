using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleAspNetWithEfCore.DataAccess;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Security.Claims;

namespace SampleAspNetWithEfCore
{
    public class Startup
    {
        private const string ApiName = "SampleAspNetWithEfCore";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connectionString = Configuration.GetConnectionString("SampleAspNetWithEfCoreDatabase");
            services.AddDbContext<MetaDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<PeopleDbContext>(options => options.UseSqlServer(connectionString));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>
                {
                    opts.Authority = "https://demo.identityserver.io";
                    opts.Audience = "api";
                });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddOptions<SystemOptions>(Configuration.GetSection("System"));

            services.AddScoped<ClaimsPrincipal>(s => s.GetService<IHttpContextAccessor>()?.HttpContext?.User);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = ApiName, Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiName); c.RoutePrefix = ""; });

            app.UseDeveloperExceptionPage();

            app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
            
            var db = services.GetService<PeopleDbContext>();
            db.Database.Migrate();
            PeopleDbContext.Seed(db);
        }
    }
}
