using Microsoft.EntityFrameworkCore;
using NexGen.Models.Entities;

namespace NexGen.Data;

public class NexGenDbContext : DbContext
{
    public NexGenDbContext(DbContextOptions<NexGenDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(user =>
        {
            user.HasKey(u => u.Id);
            user.Property(u => u.Id).IsRequired();
            user.Property(u => u.Email).IsRequired();
            user.Property(u => u.Hash).IsRequired();
        });
    }
}