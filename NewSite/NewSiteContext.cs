﻿using System;
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

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=DESKTOP-DP52C93\\SQLEXPRESS;Database=NewSite;integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(option =>
            {
                option.HasIndex(x => new { x.Email, x.Id });

                option.HasOne(p => p.Role)
                        .WithMany(b => b.Users)
                        .HasForeignKey(x => x.RoleId);

            });
                        



            OnModelCreatingPartial(modelBuilder);

            

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1,Name="Admin" },
                new Role { Id = 2,Name="User" },
                new Role { Id = 3,Name="Developer" }
                );
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);



    }
}
