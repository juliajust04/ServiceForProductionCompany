using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using ConsoleApp1.Models;
using ConsoleApp1.services.implementations;
using ConsoleApp1.services.interfaces;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var container = new Container();
        container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

        container.Register<CalculatorContext>(Lifestyle.Scoped);
        container.Register<ICityService, CityService>(Lifestyle.Scoped);
        container.Register<IModuleService, ModuleService>(Lifestyle.Scoped);
        container.Register<ICalculatorCostService, CalculatorCostService>(Lifestyle.Scoped);
        container.Register<ISearchHistoryService, SearchHistoryService>(Lifestyle.Scoped);
        container.Register<IShowResultService, ShowResultService>(Lifestyle.Scoped);

        container.Verify();

        services.AddSingleton(container);

        services.AddControllers();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
               name: "areas",
               pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            endpoints.MapControllers();
        });
    }
}
