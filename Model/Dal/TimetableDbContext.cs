using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Model.Entities;

namespace Model.Dal;

public class TimetableDbContext : DbContext
{
    public TimetableDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<TimetableUser> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<TeacherLoad> ClassGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(b =>
        {
            b.Log(RelationalEventId.AmbientTransactionWarning);
        });
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<TimetableUser>(e =>
        {
            e.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            e.Property(u => u.Email).HasMaxLength(256);

            e.OwnsOne(u => u.RoleSet,
                sa =>
                {
                    sa.Ignore(sa => sa.Roles);
                    sa.Property("_roleBitSet");
                });

            e.OwnsOne(u => u.FullName);
            e.OwnsOne(u => u.Address);
        });
        mb.Entity<Student>(e =>
        {
            e.HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Student>(s => s.UserId)
                .IsRequired();

            e.HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId)
                .IsRequired();

            e.OwnsOne(u => u.MotherContacts,
                pc => { pc.OwnsOne(c => c.FullName); });
            e.OwnsOne(u => u.FatherContacts,
                pc => { pc.OwnsOne(c => c.FullName); });

            e.Navigation(s => s.FatherContacts).IsRequired();
            e.Navigation(s => s.MotherContacts).IsRequired();
        });
        mb.Entity<Teacher>(e =>
        {
            e.HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Teacher>(s => s.UserId)
                .IsRequired();

            e.HasMany(t => t.TeacherLoads)
                .WithOne(c => c.Teacher)
                .IsRequired();
        });
        mb.Entity<Group>(e =>
        {
            e.HasMany(g => g.Students)
                .WithOne(s => s.Group)
                .IsRequired();

            e.HasMany(group => group.Disciplines)
                .WithOne(d => d.Group)
                .HasForeignKey(d => d.GroupId)
                .IsRequired();

            e.HasOne<Teacher>(group => group.Curator)
                .WithMany()
                .HasForeignKey(g => g.CuratorId)
                .IsRequired();
        });
        mb.Entity<Subject>(e =>
        {
            
        });
        mb.Entity<Discipline>(e =>
        {
            e.HasOne(d => d.Subject)
                .WithMany()
                .HasForeignKey(d => d.SubjectId)
                .IsRequired();
            
        });
        mb.Entity<TeacherLoad>(e =>
        {
            e.HasOne<Discipline>()
                .WithMany()
                .HasForeignKey(l => l.DisciplineId)
                .IsRequired();

            e.HasMany<TimetableEntry>(l => l.TimetableEntries)
                .WithOne(e => e.TeacherLoad)
                .HasForeignKey(l => l.TeacherLoadId)
                .IsRequired();
        });
        mb.Entity<TimetableEntry>(e =>
        {
        });
    }
}