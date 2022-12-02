using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

// Código para tirar o símbolo de cadeado das rotas que não precisam de autorização
// https://stackoverflow.com/questions/56745739/in-swagger-ui-how-can-i-remove-the-padlock-icon-from-anonymous-methods
namespace TryitterAPI.Services.Swagger
{
    public class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context?.MethodInfo?.DeclaringType?.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>();

            if (authAttributes!.Any())
            {
                var securityRequirement = new OpenApiSecurityRequirement()
            {
                {
                    // Put here you own security scheme, this one is an example
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                    },
                    new List<string>()
                }
            };
                operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            }
        }
    }
}
