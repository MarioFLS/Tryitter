using Microsoft.OpenApi.Models;
using TryitterAPI.Repository;
using TryitterAPI.Services.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TryitterContext>();
builder.Services.AddScoped<ITryitterRepository, TryitterRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Tryitter",
            Description = "Uma API de uma rede social chamada: Tryitter, talvez um pouco baseada no Twitter. Mas pode ser coincidência",
        });

        options.SchemaFilter<SwaggerFilterStudent>();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "docs";

});

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
