using Microsoft.EntityFrameworkCore;
using Tech.Domain.Entities;

namespace Tech.DAL.DbContexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Registry> Registry { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<CourseEnrollment> CourseEnrollments { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

        builder.Entity<CourseEnrollment>()
            .HasKey(c => new { c.UserId, c.CourseId});

        builder.Entity<CourseEnrollment>()
            .HasOne(c => c.Course)
            .WithMany(c => c.Students)
            .HasForeignKey(c => c.CourseId);

        builder.Entity<CourseEnrollment>()
            .HasOne(c => c.User)
            .WithMany(c => c.Courses)
            .HasForeignKey(c => c.UserId);

        builder.Entity<Attendance>()
            .HasOne(a => a.Course)
            .WithMany(c => c.Attendaces)
            .HasForeignKey(a => a.CourseId);

        builder.Entity<Attendance>()
            .HasOne(a => a.User)
            .WithMany(c => c.Attendaces)
            .HasForeignKey(a => a.UserId);
	}
}
