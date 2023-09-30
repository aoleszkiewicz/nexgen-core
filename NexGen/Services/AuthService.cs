using Microsoft.EntityFrameworkCore;
using NexGen.Data;
using NexGen.Helpers;
using NexGen.Models.Dtos;
using NexGen.Models.Entities;

namespace NexGen.Services;

public interface IAuthService
{
    Task<UserEntity> Login(LoginDto dto);
    Task<UserEntity> Register(RegisterDto dto);
}

public class AuthService : IAuthService
{
    private readonly NexGenDbContext _dbContext;
    
    public AuthService(NexGenDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<UserEntity> Login(LoginDto dto)
    {
        if (dto.Email is null)
        {
            throw new ArgumentNullException(nameof(dto.Email), "Email cannot be null");
        }
        
        if (dto.Password is null)
        {
            throw new ArgumentNullException(nameof(dto.Password), "Password cannot be null");
        }
        
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user is null)
        {
            throw new Exception("User not found");
        }
        
        if (user.Email != dto.Email)
        {
            throw new ArgumentException("Email does not match");
        }
        
        if (!PasswordHelper.Verify(dto.Password, user.Hash))
        {
            throw new ArgumentException("Password does not match");
        }

        return user;
    }
    
    public async Task<UserEntity> Register(RegisterDto dto)
    {
        if (dto.Email is null)
        {
            throw new ArgumentNullException(nameof(dto.Email), "Email cannot be null");
        }
        
        if (dto.Password is null)
        {
            throw new ArgumentNullException(nameof(dto.Password), "Password cannot be null");
        }

        var userExists = await _dbContext.Users.AnyAsync(u => u.Email == dto.Email);
        
        if (userExists)
        {
            throw new Exception("User already exists");
        }
        
        var user = new UserEntity()
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            Hash = PasswordHelper.Hash(dto.Password)
        };
        
        await _dbContext.Database.BeginTransactionAsync();
        
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        
        await _dbContext.Database.CommitTransactionAsync();

        return user;
    }
}