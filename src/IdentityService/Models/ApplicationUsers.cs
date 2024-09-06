using System;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Models;

public class ApplicationUsers : IdentityUser
{
    public string Name {get; set;}
}
