using System;

namespace UserService.Models;

public class UserPreference
{
    public Guid UserId {get; set;}
    public User User {get; set;}
    public int PreferenceId {get; set;}
    public Preference Preference {get; set;}
    public bool IsEnable {get; set;}
}
