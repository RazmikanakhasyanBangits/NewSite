using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NewSite.Entity;

namespace NewSite
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=DESKTOP-DP52C93\\SQLEXPRESS;Database=NewSite;integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public DbSet<User> Users { get; set; }

    }
}
