using System.Net.Http.Headers;
using MagniseTask.Application.Service;
using MagniseTask.Application.Service.Interfaces;
using MagniseTask.Infrastructure.CoinApi.Repository;
using MagniseTask.Infrastructure.CoinApi.Repository.Interface;
using MagniseTask.Infrastructure.Repository;
using MagniseTask.Infrastructure.Repository.Interface;
using MagniseTask.Web.Controllers;
using MagniseTask.Web.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();

builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddScoped<ICryptoRepository, CryptoRepository>();
builder.Services.AddScoped<ICryptoService, CryptoService>();
builder.Services.AddScoped<ICoinApiRepository, CoinApiRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddHttpClient<ICryptoRepository, CryptoRepository>((serviceProvider, client) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var baseUrl = configuration["CryptoApiSettings:InternalDockerBaseUrl"];
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHttpClient<ICoinApiRepository, CoinApiRepository>((serviceProvider, client) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(configuration["CoinApi:BaseHttpUrl"]); 
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Add("X-CoinAPI-Key", configuration["CoinApi:MarketDataApiToken"]);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.MapControllers();
app.MapHub<CryptoHub>("/cryptoTrade");

app.Run();
