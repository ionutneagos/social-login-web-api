using Microsoft.OpenApi.Models;
using WebApi.ActionFilters;

namespace WebApi.Extensions
{
    public static class ConfigureSwagger
    {
        public static void ConfigureAppSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WEB API",
                    Description = "Sample on how to register/authenitcate with google provider from angular 9 powered by ASP.NET Core backend",
                    Contact = new OpenApiContact
                    {
                        Name = "Ionut Neagos",
                        Email = "ionutneagos@yahoo.com",
                        Url = new Uri("https://ro.linkedin.com/in/ionutneagos"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                    }
                });
                c.SchemaFilter<SwaggerExcludeFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                                    Enter 'Bearer' [space] and then your token in the text input below. 
                                    Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });
            });
        }
    }
}
