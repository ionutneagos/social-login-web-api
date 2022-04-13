using Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApi.Extensions;
using WebApi.Interfaces;
using WebApi.Middlewares;
using WebApi.Services;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddCors();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.ConfigureAppSwagger();
    builder.Services.ConfigureAppSqlDatabase(builder.Configuration);
    builder.Services.AddAppIdentity(builder.Configuration);
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddTransient<ExceptionHandlingMiddleware>();
    builder.Services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());

    builder.Services.AddMvc(options => options.RespectBrowserAcceptHeader = true);
  
    var app = builder.Build();

    app.MapSwagger();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "GOOGLE AUTH SAMPLE API V1");
        options.RoutePrefix = string.Empty;
    });
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    app.UseHttpsRedirection();

    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseRouting();
    app.UseHttpsRedirection();
    app.UseCors(it => it.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    var scopeFactory = app.Services?.GetService<IServiceScopeFactory>();
    if (scopeFactory != null)
    {
        await scopeFactory.MigrateCatalogDbToLatestVersionAsync();
    }

    await app.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Unhandled exception on starting ap: Error: {ex}.");
}
