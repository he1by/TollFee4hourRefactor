using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TollFee.Application.Interfaces;
using TollFee.Domain.Configuration;
using TollFee.Infrastructure.Services;
using FluentValidation;
using System;
using TollFee.Api.Models.Requests;
using TollFee.Api.Validators;

namespace TollFee.Api;

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
        var appSettings = new AppSettings();
        Configuration.GetSection("AppSettings").Bind(appSettings);
        services.AddSingleton(appSettings);
        services.AddScoped<IValidator<GetFeeRequestModel>, GetFeeValidator>();
        services.AddTransient<IFeeService, FeeService>();
        services.AddControllers();
        //TODO: add example of new requests
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TollFee.Api", Version = "v1" });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TollFee.Api v1"));
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
