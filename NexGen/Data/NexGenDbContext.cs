﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NexGen.Models.Entities;

namespace NexGen.Data;

public class NexGenDbContext : DbContext
{
    public NexGenDbContext(DbContextOptions<NexGenDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Hash).IsRequired();
        });
    }
}