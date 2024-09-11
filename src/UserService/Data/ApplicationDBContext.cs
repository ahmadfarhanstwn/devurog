using System;
using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data;

public class ApplicationDBContext : DbContext
{
    public DbSet<User> Users {get; set;}
    public DbSet<Preference> Preferences {get; set;}
    public DbSet<UserPreference> UserPreferences {get; set;}
    public DbSet<Skill> Skills {get; set;}
    public DbSet<UserSkill> UserSkills {get; set;}

    public ApplicationDBContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserPreference>()
            .HasKey(up => new { up.UserId, up.PreferenceId });

        modelBuilder.Entity<UserPreference>()
            .HasOne(up => up.User)
            .WithMany(u => u.UserPreferences)
            .HasForeignKey(up => up.UserId);

        modelBuilder.Entity<UserPreference>()
            .HasOne(up => up.Preference)
            .WithMany(p => p.UserPreferences)
            .HasForeignKey(up => up.PreferenceId);


        modelBuilder.Entity<UserSkill>()
            .HasKey(up => new { up.UserId, up.SkillId });

        modelBuilder.Entity<UserSkill>()
            .HasOne(up => up.User)
            .WithMany(u => u.UserSkills)
            .HasForeignKey(up => up.UserId);

        modelBuilder.Entity<UserSkill>()
            .HasOne(up => up.Skill)
            .WithMany(p => p.UserSkills)
            .HasForeignKey(up => up.SkillId);
    }
}
