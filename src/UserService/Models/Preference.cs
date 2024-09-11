using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.Models;

public class Preference
{
    [Key]
    public int Id {get; set;}

    [Required]
    [MaxLength(255)]
    public string PreferenceName {get; set;}

    public ICollection<UserPreference> UserPreferences {get; set;}
}
