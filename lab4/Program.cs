using lab4.Models;
using lab4.Services.Abstract;
using lab4.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IAnimal, Cat>();
//builder.Services.AddTransient<IAnimal, Dog>();
//builder.Services.AddTransient<IAnimalSender, HtmlAnimalSender>();
builder.Services.AddTransient<IAnimalSender, FileAnimalSender>();


var app = builder.Build();

var animalSender = app.Services.GetRequiredService<IAnimalSender>();

app.MapGet("/animal", async (HttpContext context) =>
{
    await animalSender.GetAnimal(context);
});

app.Run();
