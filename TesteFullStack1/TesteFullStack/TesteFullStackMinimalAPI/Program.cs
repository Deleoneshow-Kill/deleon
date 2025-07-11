using TesteFullStackMinimalAPI.Models;
using TesteFullStackMinimalAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TaskManegerDB>(options =>
        options.UseInMemoryDatabase("TaskManagerDB"));
// UseInMemoryDatabase é usado para fins de teste, em produção você deve usar um banco de dados real    
  builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
c.SwaggerDoc("v1",new() { Title = "TaskManager.MinimalAPI",Version ="v1"}));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/tarefas", async (TaskManegerDB db) => 
await db.Tarefas.ToListAsync());

app.MapGet("/tarefas/{id} ", async ( Guid id ,TaskManegerDB db) =>
await db.Tarefas.FindAsync() is Tarefa tarefa  
  ? Results.Ok(tarefa)
  : Results.NotFound("Tarefa não encontrada"));
  
app.MapPost("/tarefas", async (Tarefa tarefa, TaskManegerDB db) =>
    {
    
    db.Tarefas.Add(tarefa);
    await db.SaveChangesAsync();
    return Results.Created($"/tarefas/{tarefa.Id}", tarefa);
}); 

app.MapPut("/tarefas/{id}", async (Guid id, Tarefa tarefaAtualizada, TaskManegerDB db) =>
{
    var tarefa = await db.Tarefas.FindAsync(id);
    if (tarefa is null)
    {
        return Results.NotFound("Tarefa não encontrada");
    }
    tarefa.AtualizarTarefa(tarefaAtualizada.Nome, tarefaAtualizada.Detalhes, tarefaAtualizada.Concluida);
    await db.SaveChangesAsync();
    return Results.Ok(tarefa);
}); 

app.MapDelete("/tarefas/{id}", async (Guid id, TaskManegerDB db) =>
{
    var tarefa = await db.Tarefas.FindAsync(id);
    if (tarefa is null)
    {
        return Results.NotFound("Tarefa não encontrada");
    }
    db.Tarefas.Remove(tarefa);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManager.MinimalAPI v1"));
}

app.Run();
