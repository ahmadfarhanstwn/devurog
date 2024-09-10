using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityService.Application.Dto;

public class RegisterDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    public string Name { get; set; }
}
