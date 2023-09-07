namespace NexGen.Models.Entities;

public class UserEntity : BaseEntity
{
    public string Email { get; set; }
    public string Hash { get; set; }
    public string Salt { get; set; }
}