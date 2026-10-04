using Swashbuckle.AspNetCore.SwaggerUI;
using PrimerParcial1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<NumbersService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var numbersService = scope.ServiceProvider
        .GetRequiredService<NumbersService>();

    await numbersService.InitializeAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();