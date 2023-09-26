using Identity.API;
using Identity.API.Extensions;
using Identity.API.Middlewares;
using Identity.Application.Extensions;
using Identity.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureAPI(builder.Configuration);
builder.Services.ConfigureInfrastructure(builder.Configuration);
builder.Services.ConfigureApplication(builder.Configuration);
builder.ConfigureLogger();

var app = builder.Build();

app.ConfigureExceptionHandler(app.Logger);

app.UseSerilogRequestLogging();

if (app.Environment.IsProduction())
    app.UseHsts();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API v1");
});

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");


app.UseAuthentication();
app.UseAuthorization();

app.UseIdentityServer();

app.MapControllers();
await app.ApplyMigrations();
await DataSeeder.SeedAsync(app);
app.Run();
