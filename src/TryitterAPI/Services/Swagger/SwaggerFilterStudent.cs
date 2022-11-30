using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace TryitterAPI.Services.Swagger
{
    public class SwaggerFilterStudent : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            schema.Properties.Remove("id");
            schema.Properties.Remove("posts");
        }
    }
}
