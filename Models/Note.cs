using Models.Abstract;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table("Notes")]
public class Note : BaseModel
{
    public string Title { get; set; }
    public string Text { get; set; }
    public int CreatorId { get; set; }
    public DateOnly CreationDate { get; set; }

    public Note(string title,
        string text, User creator)
    {
        Title = title;
        Text = text;
        CreatorId = creator.Id;
        CreationDate = DateOnly.FromDateTime(dateTime: DateTime.UtcNow);
    }

    public Note() { }
}
