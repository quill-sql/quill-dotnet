using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Nodes;
using Quill;
using Quill.Tenants;
using Quill.Database;
using Quill.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetEnv;

public static class Program
{
    public static async Task Main(string[] args)
    {
        // Load environment variables from .env file
        Env.Load();
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });
        // Create Quill instance
        var quill = new Quill.Quill(new QuillOptions
        {
            PrivateKey = Environment.GetEnvironmentVariable("PRIVATE_KEY") ?? "",
            DatabaseConnectionString = Environment.GetEnvironmentVariable("DB_URL") ?? "",
            DatabaseConfig = null,
            DatabaseType = Environment.GetEnvironmentVariable("DB_TYPE") ?? "postgresql",
            IsPooled = true,
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline
        app.UseCors("AllowAll");

        // Basic route
        app.MapGet("/", () => "Hello World!");

        // Quill route
        app.MapPost("/quill", async (HttpContext context) =>
        {
            try
            {
                using var reader = new StreamReader(context.Request.Body);
                var body = await reader.ReadToEndAsync();
                var request = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(body);
                if (request == null)
                {
                    return Results.BadRequest(new { error = "Invalid request" });
                }
                var metadata = request["metadata"];
                var tenants = metadata.TryGetProperty("tenants", out var tenantsElement)
                    ? tenantsElement.GetRawText()
                    : JsonSerializer.Serialize(new List<string> { Quill.Tenants.TenantUtils.AllTenants });
                var queryParams = new QueryParams
                {
                    Tenants = JsonSerializer.Deserialize<List<object>>(tenants),
                    Metadata = JsonSerializer.Deserialize<IDictionary<string, object>>(metadata.GetRawText()),
                };
                var result = await quill.Query(queryParams);
                return Results.Json(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        // Start the server
        await app.RunAsync("http://localhost:3008");
    }
}