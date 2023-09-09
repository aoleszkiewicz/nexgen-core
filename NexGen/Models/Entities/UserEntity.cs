using System.ComponentModel.DataAnnotations;

namespace NexGen.Models.Entities;

public class UserEntity : BaseEntity
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Hash { get; set; }
}