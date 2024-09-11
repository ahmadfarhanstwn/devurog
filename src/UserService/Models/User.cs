using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.Models;

public class User
{
    [Key]
    public Guid Id {get; set;}
    [Required]
    [MaxLength(255)]
    public string Email {get; set;}
    [Required]
    [MaxLength(255)]
    public string Username {get; set;}
    public string FullName {get; set;}
    [MaxLength(255)]
    public string ProfilePicture {get; set;}
    public string Bio {get; set;}
    [MaxLength(100)]
    public string Location {get; set;}

    public ICollection<UserPreference> UserPreferences {get; set;}
    public ICollection<UserSkill> UserSkills {get; set;}

    public DateTime LastLogin {get; set;}
    public DateTime CreatedAt {get; set;}
}
