var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scoped = app.Services.CreateScope())
{
    //TODO
    /*var services = scoped.ServiceProvider;
    var context = services.GetRequiredService<>();
    context.Database.Migrate();*/
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
