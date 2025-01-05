using AspNetCoreRateLimit;
using URLShortener;
using URLShortener.Middlewares;

var builder = WebApplication.CreateBuilder(args);

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
