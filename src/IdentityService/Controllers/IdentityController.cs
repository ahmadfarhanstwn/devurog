using System;
using IdentityService.Application.Dto;
using IdentityService.Helpers;
using IdentityService.Models;
using IdentityService.Repository;
using IdentityService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly JWTTokenService _jwtTokenService;

    public IdentityController(UserRepository userRepository, JWTTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerDto">The user registration data (email, password, username, name).</param>
    /// <returns>Success message or error details.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
       if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _userRepository.GetUserByUsername(registerDto.Username);
        if (existingUser != null)
            return BadRequest("Username already exists.");

        var newUser = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = PasswordHasher.HashPassword(registerDto.Password),
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.CreateUser(newUser);

        return Ok("User registered successfully.");
    }

    /// <summary>
    /// Login a user.
    /// </summary>
    /// <param name="loginDto">The user login data (username, password).</param>
    /// <returns>Success message or error details.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userRepository.GetUserByUsername(loginDto.Username);

        if (user == null || !PasswordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
            return Unauthorized("Invalid username or password.");

        var token = _jwtTokenService.GenerateToken(user);
        return Ok(new { Token = token });
    }
}