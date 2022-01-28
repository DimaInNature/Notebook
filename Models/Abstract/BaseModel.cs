using System;

namespace Models.Abstract;

public abstract class BaseModel : IBaseModel
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}