using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Vox.Models;

public partial class DBContext : DbContext
{

    public DbSet<UserModel> Users { get; set; }
    public DbSet<OrganizersModel> Organizers { get; set; }
    public DbSet<SportEventsModel> Sports { get; set; }

    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<UserModel>().ToTable("users");
        modelBuilder.Entity<SportEventsModel>().ToTable("sport_events");
        modelBuilder.Entity<OrganizersModel>().ToTable("organizers");


        modelBuilder.Entity<SportEventsModel>()
                .HasOne(m => m.Organizers)
                .WithMany(m => m.SportEvents)
                .HasForeignKey(m => m.OrganizersId).OnDelete(DeleteBehavior.Cascade);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
