using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityService.Application.Dto;

public class LoginDto
{
    [Required]
    public string Username {get; set;}

    [Required]
    public string Password {get; set;}
}
