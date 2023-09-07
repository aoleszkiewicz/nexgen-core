using Microsoft.AspNetCore.Mvc;
using NexGen.Models.Dtos;
using NexGen.Services;

namespace NexGen.Controllers;

public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    
    public AuthController(AuthService authService) => 
        _authService = authService;

    public async Task<IActionResult> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        string token = await _authService.Login(dto, cancellationToken);
    } 
}