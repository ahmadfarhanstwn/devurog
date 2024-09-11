using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.Models;

public class Skill
{
    [Key]
    public int Id {get; set;}

    [Required]
    [MaxLength(255)]
    public string Name {get; set;}

    public ICollection<UserSkill> UserSkills {get; set;}
}
