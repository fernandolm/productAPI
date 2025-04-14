using Application;
using Infrastructure;
using Infrastructure.Database;
using Microsoft.Extensions.Options;
using ProductAPI;
using ProductAPI.Extensions;
using ProductAPI.Jobs.Domain;
using ProductAPI.Jobs.Service;
using ProductAPI.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();
builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

// background service
builder.Services.AddHostedService<BackgroundProcessingService>();
builder.Services.AddSingleton(provider =>
    provider.GetServices<IHostedService>()
        .OfType<BackgroundProcessingService>()
        .First());
builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = 1024;
    options.CompactionPercentage = 0.25;
});
builder.Services.Configure<ProcessingSettings>(builder.Configuration.GetSection("ProcessingSettings"));
builder.Services.AddSingleton<IBackgroundTaskQueue>(provider =>
    new BackgroundTaskQueue(provider.GetRequiredService<IOptions<ProcessingSettings>>().Value.QueueCapacity)
);
// end background service

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.MapEndpoints();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureDeletedAsync().ConfigureAwait(false);
        await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
    }
    catch (Exception exception)
    {
        Console.WriteLine($"An error occurred while creating the database ProductDatabase: {exception.Message}");
    }
}
await app.RunAsync().ConfigureAwait(false);
