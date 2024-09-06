using System;
using IdentityService.Application.Dto;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[Route("/identity")]
public class IdentityControllers : ControllerBase
{
    private readonly UserManager<ApplicationUsers> _userManager;

    public IdentityControllers(UserManager<ApplicationUsers> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Register(RegisterDto model)
    {
        var user = new ApplicationUsers
        {
            UserName = model.Username,
            Email = model.Email,
            Name = model.Name
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        
        if (result.Succeeded)
        {
            return Ok("User registered successfully");
        }

        return BadRequest(result.Errors);
    }
}