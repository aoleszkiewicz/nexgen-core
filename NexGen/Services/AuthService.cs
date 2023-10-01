using Microsoft.EntityFrameworkCore;
using NexGen.Data;
using NexGen.Helpers;
using NexGen.Helpers.Validators;
using NexGen.Models.Dtos;
using NexGen.Models.Entities;

namespace NexGen.Services;

public interface IAuthService
{
    Task<string> Register(RegisterDto dto, CancellationToken cancellationToken);
    Task<string> Login(LoginDto dto, CancellationToken cancellationToken);
}

public class AuthService : IAuthService
{
    private readonly NexGenDbContext _dbContext;
    private readonly string? _jwtSecretKey;
    
    public AuthService(NexGenDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _jwtSecretKey = configuration.GetSection("JwtRelated:SecretKey").Value;
    }
    
    public async Task<string> Register(RegisterDto dto, CancellationToken cancellationToken)
    {
        var isEmailValid = FieldValidators.ValidateEmail(dto.Email);
        
        if (!isEmailValid)
            throw new ArgumentException("Email is not valid");
        
        if (dto.Password is null)
            throw new ArgumentNullException(nameof(dto.Password), "Password cannot be null");

        var userExists = await _dbContext.Users.AnyAsync(u => u.Email == dto.Email, cancellationToken);
        
        if (userExists)
            throw new Exception("User already exists");
        
        var user = new UserEntity()
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            Hash = PasswordHelper.Hash(dto.Password),
        };
        
        await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);

        return JwtHelper.GenerateAccessToken(user.Id, user.Email, _jwtSecretKey);
    }
    
    public async Task<string> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        if (dto.Email is null)
            throw new ArgumentNullException(nameof(dto.Email), "Email cannot be null");
        
        if (dto.Password is null)
            throw new ArgumentNullException(nameof(dto.Password), "Password cannot be null");
        
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email, cancellationToken);

        if (user is null)
            throw new Exception("User does not exist");
        
        if (user.Email != dto.Email)
            throw new ArgumentException("Email does not match");
        
        if (!PasswordHelper.Verify(dto.Password, user.Hash))
            throw new ArgumentException("Password does not match");
        
        return JwtHelper.GenerateAccessToken(user.Id, user.Email, _jwtSecretKey);
    }
}