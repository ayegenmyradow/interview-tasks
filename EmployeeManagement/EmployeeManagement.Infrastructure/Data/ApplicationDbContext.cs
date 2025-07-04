using EmployeeManagement.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Employee configuration
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DateOfBirth)
                    .IsRequired()
                    .HasColumnType("date"); // Store as date without time
                entity.Property(e => e.HireDate)
                    .IsRequired()
                    .HasColumnType("timestamp with time zone"); // Store as timestamp with timezone
                entity.Property(e => e.Salary).IsRequired().HasPrecision(18, 2);

                // Relationship with Position
                entity.HasOne(e => e.Position)
                      .WithMany(p => p.Employees)
                      .HasForeignKey(e => e.PositionId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Email should be unique
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Position configuration
            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Department).HasMaxLength(100);
                entity.Property(p => p.BaseSalary).IsRequired().HasPrecision(18, 2);
                entity.Property(p => p.Description).HasMaxLength(500);

                entity.HasIndex(p => p.Name).IsUnique();
            });
        }
    }
} 