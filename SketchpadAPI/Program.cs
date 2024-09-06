using SketchpadDatabase.Context;
using SketchpadDatabase.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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


app.MapPut("/addNote/", (NoteModel note) =>
{
    using (var context = new SketchpadDbContext())
    {
        context.Notes.Add(note);
        context.SaveChanges();
    }
})
.WithOpenApi();

app.MapGet("/getNote/{id}", (int id) =>
{
    using (var context = new SketchpadDbContext())
    {
        return context.Notes.Find(id);
    }
}).WithOpenApi();

app.MapGet("/getNotes/{chatId}", (long chatId) =>
{
    using (var context = new SketchpadDbContext())
    {
         return context.Notes.Where(note => note.chatId == chatId).ToList();
    }
}).WithOpenApi();

app.Run();
