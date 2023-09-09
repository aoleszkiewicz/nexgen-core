using Microsoft.AspNetCore.Mvc;
using NexGen.Models.Dtos;
using NexGen.Services;

namespace NexGen.Controllers;

[Route("/api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    
    public AuthController(AuthService authService) => 
        _authService = authService;

    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        return await _authService.Login(dto);
    } 
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        return await _authService.Register(dto);
    }
}