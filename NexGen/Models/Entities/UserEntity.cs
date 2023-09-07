namespace NexGen.Models.Entities;

public class UserEntity : BaseEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Hash { get; set; }
}