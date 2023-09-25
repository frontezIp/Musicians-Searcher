using Chat.API.Extensions;
using Chat.API.Middlewares;
using Chat.BusinessLogic.Extensions;
using Chat.DataAccess.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureAPI(builder.Configuration);
builder.Services.ConfigureDAL(builder.Configuration);
builder.Services.ConfigureBLL(builder.Configuration);
builder.ConfigureLogger();

var app = builder.Build();

app.ConfigureExceptionHandler(app.Logger);

app.UseSerilogRequestLogging();

if (app.Environment.IsProduction())
    app.UseHsts();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat API v1");
});

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
await app.ApplyMigrations();
await DataSeeder.SeedAsync(app);
app.Run();
