namespace WebApi.ActionFilters
{
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Linq;
    using System.Reflection;
    using WebApi.Helpers;
    using static System.Char;

    public class SwaggerExcludeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
                return;

            var excludedProperties = context.Type.GetProperties().Where(t => t.GetCustomAttribute<SwaggerExcludeAttribute>() != null);

            foreach (var excludedProperty in excludedProperties)
            {
                var propertyName = $"{ToLowerInvariant(excludedProperty.Name[0])}{excludedProperty.Name.Substring(1)}";
                if (schema.Properties.ContainsKey(propertyName))
                    schema.Properties.Remove(propertyName);
            }
        }
    }
}
