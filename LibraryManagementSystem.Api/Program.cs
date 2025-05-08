using LibraryManagementSystem.Api.Middlewares;
using LibraryManagementSystem.Application.DependencyInjection;
using LibraryManagementSystem.Infrastructure.DependencyInjection;
using LibraryManagementSystem.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostClient", policy =>
    {
        policy.WithOrigins(builder.Configuration["CorsUrl"]!)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // if needed
    });
});
builder.Services.AddSwaggerGen(c =>
{
    var APIKey = builder.Configuration["ApiKeySettings:Key"];
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library API", Version = "v1" });

    // Define the API key scheme
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API Key Header using x-api-key. Enter only the key value.",
        Type = SecuritySchemeType.ApiKey,              
        Name = "x-api-key",                             
        In = ParameterLocation.Header,
    });

    // Apply the API key scheme to all operations
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "ApiKey" // must match the key in AddSecurityDefinition
            }
        },
        Array.Empty<string>()
    }
});

});


var app = builder.Build();
app.UseCors("AllowLocalhostClient");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<ApiKeyMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var config = app.Services.GetRequiredService<IConfiguration>();
if (config.GetValue<bool>("RunDbInitializerOnStartup"))
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInit = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
        await dbInit.InitializeAsync();
    }

}

app.Run();
