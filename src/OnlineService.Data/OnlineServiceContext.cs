using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OnlineService.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineService.Data
{
    public class OnlineServiceContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public OnlineServiceContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().Property(e => e.UserName).HasMaxLength(150);
            modelBuilder.Entity<User>().Property(e => e.FullName).HasMaxLength(250);
            modelBuilder.Entity<User>().Property(e => e.Password).HasMaxLength(256);
            modelBuilder.Entity<User>().Property(e => e.CreatedDate).ForNpgsqlHasDefaultValueSql("current_timestamp");
            modelBuilder.Entity<User>().Property(e => e.CreatedBy).ForNpgsqlHasDefaultValue("System");
            modelBuilder.Entity<User>().Property(e => e.ModifiedDate).ForNpgsqlHasDefaultValueSql("current_timestamp");
            modelBuilder.Entity<User>().Property(e => e.isActive).ForNpgsqlHasDefaultValue(true);

            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Role>().Property(e => e.RoleName).HasMaxLength(150);
            modelBuilder.Entity<Role>().Property(e => e.CreatedDate).ForNpgsqlHasDefaultValueSql("current_timestamp");
            modelBuilder.Entity<Role>().Property(e => e.ModifiedDate).ForNpgsqlHasDefaultValueSql("current_timestamp");
            modelBuilder.Entity<Role>().Property(e => e.isActive).ForNpgsqlHasDefaultValue(true);
            modelBuilder.Entity<Role>().Property(e => e.CreatedBy).ForNpgsqlHasDefaultValue("System");

            modelBuilder.Entity<UserInRole>().ToTable("UserInRole");
            modelBuilder.Entity<UserInRole>().HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<UserInRole>().HasOne(x => x.User).WithMany(r => r.UserRole)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserInRole>().HasOne(x => x.Role).WithMany(r => r.RoleUser)
                .HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);        
           
            
            base.OnModelCreating(modelBuilder);
        }



    }
}
