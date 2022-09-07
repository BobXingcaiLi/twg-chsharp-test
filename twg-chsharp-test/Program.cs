using CSharpTest.Services.BusinessLogic;
using CSharpTest.Services.Log;
using CSharpTest.Models;
using CSharpTest.Services.Middleware;
using CSharpTest.Services.Repository;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddSingleton<ISearchService, SearchService>();
builder.Services.AddSingleton<ILogService, LogService>();


builder.Services.AddSingleton<IRequestRepository, RequestRepository>();
builder.Services.AddSingleton<ISearchRequestRepository, SearchRequestRepository>();
builder.Services.AddSingleton<ISearchTopProductsRepository, SearchTopProductsRepository>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddHttpClient("twg-test-client", httpClient =>
    {
        httpClient.BaseAddress = new Uri(configuration.GetSection("API_BASE_URL").Value);
        httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", configuration.GetSection("SUB_KEY").Value);
    });

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment ()) {
    app.UseExceptionHandler ("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts ();
}

app.UseAuthorization();

app.UseMiddleware<RequestLoggingHandler>();
app.UseMiddleware<ExceptionHandler>();

app.MapControllers();

app.Run();
