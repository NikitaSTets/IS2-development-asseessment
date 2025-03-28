﻿using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class ExporterDbContext : DbContext
{
    public DbSet<Policy> Policies { get; set; }

    public DbSet<Note> Notes { get; set; }

    public ExporterDbContext(DbContextOptions<ExporterDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>()
                .HasOne(n => n.Policy)
                .WithMany(p => p.Notes)
                .HasForeignKey(n => n.PolicyId)
                .OnDelete(DeleteBehavior.Cascade);
        
        //better to use migration because it can only be run once
        modelBuilder.Entity<Policy>().HasData(new Policy() { Id = 1, PolicyNumber = "HSCX1001", Premium = 200, StartDate = new DateTime(2024, 4, 1) },
            new Policy() { Id = 2, PolicyNumber = "HSCX1002", Premium = 153, StartDate = new DateTime(2024, 4, 5) },
            new Policy() { Id = 3, PolicyNumber = "HSCX1003", Premium = 220, StartDate = new DateTime(2024, 3, 10) },
            new Policy() { Id = 4, PolicyNumber = "HSCX1004", Premium = 200, StartDate = new DateTime(2024, 5, 1) },
            new Policy() { Id = 5, PolicyNumber = "HSCX1005", Premium = 100, StartDate = new DateTime(2024, 4, 1) });
        
        //better to use migration because it can only be run once
        modelBuilder.Entity<Note>().HasData(
             new Note { Id = 1, Text = "Initial note for policy 1", PolicyId = 1 },
             new Note { Id = 2, Text = "Follow-up note for policy 1", PolicyId = 1, },
             new Note { Id = 3, Text = "Policy 2 review note", PolicyId = 2, }
         );

        base.OnModelCreating(modelBuilder);
    }
}
