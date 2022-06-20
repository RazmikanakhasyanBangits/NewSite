using Microsoft.EntityFrameworkCore;
using Repository.Entity;

namespace Repository
{
    public partial class NewSiteContext : DbContext
    {
        public NewSiteContext()
        {
        }

        public NewSiteContext(DbContextOptions<NewSiteContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=DESKTOP-DP52C93\\SQLEXPRESS;Database=NewSite;integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasOne(p => p.Role)
                        .WithMany(b => b.Users)
                        .HasForeignKey(x => x.RoleId);
            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" },
                new Role { Id = 3, Name = "Developer" }
                );

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, ActiveStatus = "Active" },
                new Status { Id = 2, ActiveStatus = "Inactive" },
                new Status { Id = 3, ActiveStatus = "Blocked" },
                new Status { Id = 4, ActiveStatus = "Not Verified" }
                );
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);



    }
}
