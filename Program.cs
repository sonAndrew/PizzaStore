using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
	Args = args,
//	ApplicationName = typeof(Program).Assembly.FullName,
//	ContentRootPath = Directory.GetCurrentDirectory(),
//	EnvironmentName = Environments.Staging,
//	WebRootPath = "LinuxPizzas"
});

Console.WriteLine($"Application Name: {builder.Environment.ApplicationName}");
Console.WriteLine($"Environment Name: {builder.Environment.EnvironmentName}");
Console.WriteLine($"ContentRoot Path: {builder.Environment.ContentRootPath}");
Console.WriteLine($"WebRootPath: {builder.Environment.WebRootPath}");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<PizzaDbContext>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddSwaggerGen(c => {
	c.SwaggerDoc("v1", new OpenApiInfo { 
		Title = "PizzaStore API", 
		Description = "Making the Pizzas you love", 
		 Version = "v1" });
});

var app = builder.Build();

app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c => {
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

app.MapGet("/pizzas", async (PizzaDbContext db) => await db.Pizzas.ToListAsync());
app.MapPost("/pizza", async (PizzaDbContext db, Pizza pizza) =>
{
	await db.Pizzas.AddAsync(pizza);
	await db.SaveChangesAsync();
	return Results.Created($"/pizza/{pizza.Id}", pizza);
});
app.MapGet("/pizz/{id}", async (PizzaDbContext db, int id) => await db.Pizzas.FindAsync(id));
app.MapPut("/pizza/{id}", async (PizzaDbContext db, Pizza updatePizza, int id) =>
{
	var pizza = await db.Pizzas.FindAsync(id);
	if(pizza is null) return Results.NotFound();
	pizza.Name = updatePizza.Name;
	pizza.Description = updatePizza?.Description;
	await db.SaveChangesAsync();
	return Results.NoContent();
});
app.MapDelete("/pizza/{id}", async (PizzaDbContext db, int id) =>
{
	var pizza = await db.Pizzas.FindAsync(id);
	if (pizza is null) return Results.NotFound();
	db.Pizzas.Remove(pizza);
	await db.SaveChangesAsync();
	return Results.Ok();
});

app.Run();
