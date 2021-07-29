using EmployeesAPI.Mappings;
using EmployeesAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace EmployeesAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Models.DatabaseContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EmployeeAPI",
                    Version = "v1",
                    Description = "Учёт сотрудников"
                });
            });

            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            services.AddMemoryCache();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<ISubdivisionService, SubdivisionService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.DefaultModelRendering(ModelRendering.Example);
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.List);
                c.EnableDeepLinking();
                c.EnableFilter();
                c.ShowExtensions();
                c.ShowCommonExtensions();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"api v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
