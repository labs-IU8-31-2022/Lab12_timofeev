using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace App;

public sealed partial class University : DbContext
{
    private static string? _dataPath;

    public University()
    {
        if (_dataPath is not null)
            Database.EnsureCreated();
        else
            throw new Exception();
    }

    public University(DbContextOptions<University> options)
        : base(options)
    {
    }

    public University(string path)
    {
        Database.EnsureCreated();
        _dataPath = path;
    }

    public DbSet<Grade> Grades { get; set; } = null!;

    public DbSet<Group> Groups { get; set; } = null!;

    public DbSet<Student> Students { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseSqlite($"Filename={_dataPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("Grades_pk");

            entity.HasOne(d => d.Students).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Grades_Students_null_fk");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("Groups_pk");

            entity.Property(e => e.GroupName).HasMaxLength(20);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("Students_pk");

            entity.Property(e => e.StudentName).HasMaxLength(20);

            entity.HasOne(d => d.Groups).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Students_Groups_null_fk");
        });
    }
}