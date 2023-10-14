using Musicians.API.Extensions;
using Musicians.API.Middlewares;
using Musicians.Application.Extensions;
using Musicians.Application.Grpc.v1.Services;
using Musicians.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureInfrastructure(builder.Configuration);
builder.Services.ConfigureAPI(builder.Configuration);
builder.Services.ConfigureApplication();
builder.ConfigureLogger();

var app = builder.Build();

app.ConfigureExceptionHandler(app.Logger);

app.UseSerilogRequestLogging();


if (app.Environment.IsProduction())
    app.UseHsts();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Musicians API v1");
});

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<MusicianService>();

app.MapControllers();

await app.SeedAsync();

app.Run();
