using Microsoft.EntityFrameworkCore;
using ToDoApi;

var builder = WebApplication.CreateBuilder(args);

// Add DI
builder.Services.AddDbContext<ToDoDb>(opt => opt.UseInMemoryDatabase("ToDoList"));

var app = builder.Build();

// Configure pipeline
app.MapGet("/todoitems", async(ToDoDb db) => await db.ToDos.ToListAsync());

app.MapGet("/todoitems/{id}", async(int id, ToDoDb db) => await db.ToDos.FindAsync(id));

app.MapPost("/todoitems", async(ToDoItem toDoItem, ToDoDb db) => 
{
    db.ToDos.Add(toDoItem);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/${toDoItem.Id}", toDoItem);
});

app.MapPut("/todoitems/${id}", async(int id, ToDoItem toDoItem, ToDoDb db) => 
{
    var todo = await db.ToDos.FindAsync(id);
    if(todo == null) return Results.NotFound();
    todo.Name = toDoItem.Name;
    todo.IsComplete = toDoItem.IsComplete;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async(int id, ToDoDb db) => 
{
    if(await db.ToDos.FindAsync(id) is ToDoItem todo)
    {
        db.Remove(id);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});

app.Run();
