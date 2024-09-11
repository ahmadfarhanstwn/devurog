using System;

namespace UserService.Models;

public class UserSkill
{
    public Guid UserId {get; set;}
    public User User {get; set;}
    public int SkillId {get; set;}
    public Skill Skill {get; set;}
}
