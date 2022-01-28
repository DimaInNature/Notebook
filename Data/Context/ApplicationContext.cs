using System;

namespace Data.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Note> Notes => Set<Note>();

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(user => user.Id).IsUnique();
        modelBuilder.Entity<User>().HasData(GetUsers());
        modelBuilder.Entity<Note>().HasIndex(note => note.Id).IsUnique();
        modelBuilder.Entity<Note>().HasData(GetNotes());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    private List<User> GetUsers()
    {
        return new()
        {
            new()
            {
                Id = 1,
                Login = "Admin",
                Password = "Root"
            }
        };
    }

    private List<Note> GetNotes()
    {
        return new()
        {
            new()
            {
                Id = 1,
                Text = "This is first note",
                Title = "Hello World",
                CreationDate = DateOnly.FromDateTime(DateTime.UtcNow),
                CreatorId = 1
            }
        };
    }
}