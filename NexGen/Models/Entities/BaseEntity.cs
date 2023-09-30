namespace NexGen.Models.Entities;

public abstract class BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}