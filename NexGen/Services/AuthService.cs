using Microsoft.AspNetCore.Mvc;
using NexGen.Models.Dtos;

namespace NexGen.Services;

public class AuthService
{
    public async Task<IActionResult> Login(LoginDto dto)
    {
        
    }
    
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var email = dto.Email;
        var password = dto.Password;
    }
}