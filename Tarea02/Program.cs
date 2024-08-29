using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ----

var tareas = new List<Tarea>
{
    new Tarea { Id = 1, Nombre = "Tarea 1", Descripcion = "Descripción de la tarea 1", DuracionHoras = 2, Responsable = "Juan", Fecha = DateTime.Now },
    new Tarea { Id = 2, Nombre = "Tarea 2", Descripcion = "Descripción de la tarea 2", DuracionHoras = 4, Responsable = "Andrés", Fecha = DateTime.Now },
    new Tarea { Id = 3, Nombre = "Tarea 3", Descripcion = "Descripción de la tarea 3", DuracionHoras = 1, Responsable = "Luis", Fecha = DateTime.Now }
};

app.MapGet("/api/tareas", () => tareas);

app.MapGet("/api/tareas/{id:int}", (int id) =>
{
    var tarea = tareas.FirstOrDefault(t => t.Id == id);
    return tarea is not null ? Results.Ok(tarea) : Results.NotFound();
});

app.MapPost("/api/tareas", (Tarea tarea) =>
{
    tarea.Id = tareas.Count + 1;
    tareas.Add(tarea);
    return Results.Created($"/api/tareas/{tarea.Id}", tarea);
});

app.MapDelete("/api/tareas/{id:int}", (int id) =>
{
    var tarea = tareas.FirstOrDefault(t => t.Id == id);
    if (tarea is not null)
    {
        tareas.Remove(tarea);
        return Results.NoContent();
    }
    return Results.NotFound();
});

app.Run();

// Clase Tarea
public class Tarea
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public int? DuracionHoras { get; set; }
    public string? Responsable { get; set; }
    public DateTime? Fecha { get; set; }
}