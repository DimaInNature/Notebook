using System;

namespace Models.Abstract;

public interface IBaseModel
{
    int Id { get; set; }
    DateTime CreatedDate { get; set; }
}