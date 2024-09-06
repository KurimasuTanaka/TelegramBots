using System;
using Microsoft.EntityFrameworkCore;
using SketchpadDatabase.Models;

namespace SketchpadDatabase.Context;

public class SketchpadDbContext : DbContext
{
    public DbSet<NoteModel> Notes { get; set; }

    public SketchpadDbContext() {} 

    public SketchpadDbContext(DbContextOptions<SketchpadDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { //TODO:FIX THis   
        optionsBuilder.UseSqlite($"Data Source={
            "C:\\Users\\Kurimasu Tanaka\\Documents\\Coding projects\\C#\\TelegramBots\\SketchpadDatabase\\DatabaseFile\\Sketchpad.db"
            }");
    }
}
