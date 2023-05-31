using Microsoft.OpenApi.Models;

using Microsoft.EntityFrameworkCore;
using PizzaStore.Context;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PizzaDb>(options => options.UseInMemoryDatabase("items"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
   {
       c.SwaggerDoc("v1", new OpenApiInfo {
         Title = "PizzaStore API",
         Description = "Making the Pizzas you love",
         Version = "v1" });
   });


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
   {
     c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
     c.RoutePrefix = string.Empty; // esto se usa para no tener que colocar en la url swagger
   });



app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync());

app.MapPost("/pizza", async (PizzaDb db, Pizza pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});

// app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));

// app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
// app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
// app.MapDelete("/pizzas/{id}", (int id) => PizzaDB.RemovePizza(id));


app.Run();
