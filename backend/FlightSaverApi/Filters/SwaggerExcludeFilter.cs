using System.Reflection;
using FlightSaverApi.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FlightSaverApi.Filters;

public class SwaggerExcludeFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var schemasToRemove = context.SchemaRepository.Schemas
            .Where(schema => ShouldExcludeSchema(schema.Key, context))
            .Select(schema => schema.Key)
            .ToList();
        
        foreach (var schemaName in schemasToRemove)
        {
            swaggerDoc.Components.Schemas.Remove(schemaName);
        }
    }
    
    private bool ShouldExcludeSchema(string schemaName, DocumentFilterContext context)
    {
        var schemaType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.Name == schemaName);

        return schemaType?.GetCustomAttribute<SwaggerExcludeAttribute>() != null;
    }
}