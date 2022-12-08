using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TryitterAPI.Services.Swagger
{
    public class SwaggerFilterStudent : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            schema.Properties.Remove("id");
            schema.Properties.Remove("posts");
            schema.Properties.Remove("studentId");
            schema.Properties.Remove("postId");
        }
    }
}
