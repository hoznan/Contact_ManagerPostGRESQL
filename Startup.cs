// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// // using CSA.API.Data;
// using ContactManager.Data;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.HttpsPolicy;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
// // using ContactManager.AuthenticationRepository;
// // using ContactManager.AuthenticationRepository
// using AutoMapper;
// // using CSA.API.Models;
// using ContactManager.Models;
// using Microsoft.IdentityModel.Logging;

// namespace ContactManager
// {
//     public class Startup
//     {
//         public Startup(IConfiguration configuration)
//         {
//             Configuration = configuration;
//         }

//         readonly string MyAllowSpecificOrigins = "CorsPolicy";
//         public IConfiguration Configuration { get; }

//         // This method gets called by the runtime. Use this method to add services to the container.
//         public void ConfigureServices(IServiceCollection services)
//         {
//             //services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
//             services.AddDbContext<DataContext>
//                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
//             services.AddControllers();
//             services.AddControllersWithViews().AddNewtonsoftJson(option=>{
//                 option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
//             });
//             //services.AddCors();
//             services.AddSwaggerGen();

//             services.AddCors(options =>
//            {
//                options.AddPolicy(MyAllowSpecificOrigins,
//                builder =>
//                {
//                    builder.WithOrigins("http://localhost:4200","*").AllowAnyOrigin()
//                            .AllowAnyMethod()
//                            .AllowAnyHeader().AllowAnyMethod();
//                            builder.SetIsOriginAllowedToAllowWildcardSubdomains();
                           
//                });
//            });

//             // services.AddScoped<IAuthRepository, AuthRepository>();

//             // var mappingConfig = new MapperConfiguration(mc =>
//             // {
//             //     mc.AddProfile(new MappingObject());
//             // });

//             // IMapper mapper = mappingConfig.CreateMapper();
//             // services.AddSingleton(mapper);
//             // IdentityModelEventSource.ShowPII = true;
//         }

//         // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//         {
//             if (env.IsDevelopment())
//             {
//                 app.UseDeveloperExceptionPage();
//             }

//             //app.UseHttpsRedirection();
//                         app.UseSwagger();

//             app.UseSwaggerUI(c=>{c.SwaggerEndpoint("/swagger/v1/swagger.json", "Riseup API V1");
//             });

//             app.UseRouting();
//             app.UseCors("CorsPolicy");

//             app.UseAuthorization();
//             //app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}")
//                 .RequireCors("CorsPolicy");
//             });
//         }
//     }
// }
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.IdentityModel.Logging;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace ContactManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "CorsPolicy";
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddControllersWithViews().AddNewtonsoftJson(option=>{
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddSwaggerGen();
services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

// Add the following line to enable legacy timestamp behavior
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200","*").AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader().AllowAnyMethod();
                        builder.SetIsOriginAllowedToAllowWildcardSubdomains();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Riseup API V1");
            });

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}")
                .RequireCors("CorsPolicy");
            });
        }
    }
}

