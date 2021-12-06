using Microsoft.EntityFrameworkCore;
using Model.Profile;
using Model.Profile.Roles;
using Model.Timetable;
using Timetable.Timetable.Model;

namespace Model.Dal
{
    public class TimetableDbContext: DbContext
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

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<TimetableUser>(e =>
            {
                e.HasIndex(u => u.NormalizedEmail).IsUnique();
                
                e.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

                e.Property(u => u.Email).HasMaxLength(256);
                e.Property(u => u.NormalizedEmail).HasMaxLength(256);
                
                e.OwnsOne<RoleSet>(u => u.RoleSet,
                    sa =>
                    {
                        sa.Ignore(sa => sa.Roles);
                        sa.Property("_roleBitSet");
                    });

                e.OwnsOne<FullName>(u => u.FullName);
                e.OwnsOne<Address>(u => u.Address);
            });
            mb.Entity<Student>(e =>
            {
                e.HasOne<TimetableUser>(s => s.User)
                    .WithOne()
                    .HasForeignKey<Student>(s => s.Id)
                    .IsRequired();

                e.HasOne<Group>(s => s.Group)
                    .WithMany(g => g.Students)
                    .HasForeignKey(s => s.GroupId)
                    .IsRequired();
                
                e.OwnsOne<ParentContacts>(u => u.MotherContacts,
                    pc =>
                    {
                        pc.OwnsOne<FullName>(c => c.FullName);
                    });
                e.OwnsOne<ParentContacts>(u => u.FatherContacts,
                    pc =>
                    {
                        pc.OwnsOne<FullName>(c => c.FullName);
                    });

                e.Navigation(s => s.FatherContacts).IsRequired();
                e.Navigation(s => s.MotherContacts).IsRequired();
            });
            mb.Entity<Teacher>(e =>
            {
                e.HasOne<TimetableUser>(s => s.User)
                    .WithOne()
                    .HasForeignKey<Teacher>(s => s.Id)
                    .IsRequired();

                e.HasMany<TeacherLoad>(t => t.ClassGroups)
                    .WithOne(c => c.Teacher)
                    .IsRequired();
            });
            mb.Entity<Group>(e =>
            {
                e.HasMany<Student>(g => g.Students)
                    .WithOne(s => s.Group)
                    .IsRequired();

                e.HasMany<TeacherLoad>(group => group.ClassGroups)
                    .WithOne(cg => cg.Group)
                    .IsRequired();
            });
            mb.Entity<Subject>(e =>
            {
                
            });
            mb.Entity<Discipline>(e =>
            {
                e.HasOne<Subject>(d => d.Subject)
                    .WithMany()
                    .IsRequired();
            });
            mb.Entity<TeacherLoad>(e =>
            {
                e.HasOne<Discipline>()
                    .WithMany();
            });
        }
    }
}