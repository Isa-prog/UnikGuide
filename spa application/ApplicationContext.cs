using Destinationosh.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class ApplicationContext : IdentityDbContext<User>
{
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<FileModel> Files { get; set; } = null!;
    public DbSet<PostVisit> PostVisits { get; set; } = null!;
    public DbSet<RouteModel> Routes { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<User>()
            .Property(user => user.Role)
            .HasConversion(new EnumToStringConverter<UserRole>());
        
        modelBuilder
            .Entity<RouteModel>(entity =>
            {
                entity.HasOne(route => route.ParentRoute)
                    .WithMany(route => route.ChildrenRoutes)
                    .HasForeignKey(route => route.ParrentId);
                
                entity.HasOne(route => route.Post)
                     .WithOne(post => post.Route);
            });
    }
}