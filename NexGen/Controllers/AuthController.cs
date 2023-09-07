using Microsoft.AspNetCore.Mvc;
using NexGen.Models.Dtos;
using NexGen.Services;

namespace NexGen.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    
    public AuthController(AuthService authService) => 
        _authService = authService;

    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        // string token = await _authService.Login(dto);
        return Ok("OK");
    } 
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        // string token = await _authService.Login(dto);
        return Ok("OK");
    }
}