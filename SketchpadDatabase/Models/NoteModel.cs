using System;
using System.ComponentModel.DataAnnotations;

namespace SketchpadDatabase.Models;

public class NoteModel
{
    [Key] public int Id { get; set; }
    
    public string Content { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; }
    public long chatId { get; set; }
}
