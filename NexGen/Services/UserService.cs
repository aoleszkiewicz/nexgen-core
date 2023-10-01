using Microsoft.EntityFrameworkCore;
using NexGen.Data;
using NexGen.Models.Requests;

namespace NexGen.Services;

public interface IUserService
{
    Task<string> ChangeEmail(Guid userId, ChangeEmailRequest request, CancellationToken cancellationToken);
}

public class UserService : IUserService
{
    private readonly NexGenDbContext _dbContext;
    
    public UserService(NexGenDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<string> ChangeEmail(Guid userId, ChangeEmailRequest request, CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("Wrong JWT Token");

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        
        if (user is null)
            throw new Exception("User not found");

        await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        user.Email = request.Email;
        await _dbContext.SaveChangesAsync(cancellationToken);
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);

        return "Email changed successfully";
    }
}