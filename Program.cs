using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using URLShortener;
using URLShortener.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "_AllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(   
                                                "http://localhost:8081", 
                                                "https://localhost:8080"
                                            ).AllowAnyHeader().AllowAnyMethod();
                      });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add your dependency injection container service here
builder.Services.AddURLService();

// Add your database context here
builder.Services.AddTransient<URLShortenerContext>();

builder.Services.AddRateLimitServiceConfiguration();

var app = builder.Build();

app.UseCors(AllowSpecificOrigins);

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<URLShortenerContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseIpRateLimiting();

app.Run();
