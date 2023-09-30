using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexGen.Helpers;
using NexGen.Models.Dtos;
using NexGen.Services;

namespace NexGen.Controllers;

[Route("/api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    } 

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _authService.Login(dto);
        
        return Ok(user);
    } 
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = await _authService.Register(dto);
        
        return Ok(user);
    }
}