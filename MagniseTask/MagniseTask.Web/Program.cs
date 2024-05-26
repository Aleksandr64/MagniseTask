using MagniseTask.Application.Service;
using MagniseTask.Application.Service.Interfaces;
using MagniseTask.Infrastructure.API.CoinApi;
using MagniseTask.Infrastructure.API.CoinApi.Interface;
using MagniseTask.Infrastructure.Repository;
using MagniseTask.Infrastructure.Repository.Interface;
using MagniseTask.Web.Controllers;
using MagniseTask.Web.Middleware;
using MagniseTask.Web.TempFile;
using MagniseTask.Web.TempFile.Interface;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddScoped<ICoinApiClient, CoinApiClient>();
builder.Services.AddScoped<ICryptoRepository, CryptoRepository>();
builder.Services.AddScoped<ICryptoInfoService, CryptoInfoService>();
builder.Services.AddScoped<ICoinApiService, CoinApiService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddHttpClient<ICryptoRepository, CryptoRepository>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5125");
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
