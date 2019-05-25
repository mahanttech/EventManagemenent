using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Web.Api.Core.Entity;


namespace Web.Api.Core.Context
{
    public partial class NodeTestContext: DbContext
    {
        public NodeTestContext()
        {
        }

        public NodeTestContext(DbContextOptions<NodeTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EventMaster> EventMaster { get; set; }
        //   public virtual DbSet<MstTest> MstTest { get; set; }
        //   public virtual DbSet<ErrorLogs> ErrorLogs { get; set; }
        //   public virtual DbSet<MstUser> MstUser { get; set; }
        public DbSet<SystemUsers> SystemUsers { get; set; }
     //   public virtual DbSet<TestAthleteMapping> TestAthleteMapping { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=your server Name;Database=NodeTest;Trusted_Connection=False;User=userName;password=pass");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");
          
            modelBuilder.Entity<SystemUsers>(entity =>
            {
                entity.ToTable("SystemUsers");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.Username)
                    .HasColumnName("Username")
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordHash)
                    .HasColumnName("PasswordHash");
                entity.Property(e => e.PasswordSalt)
                  .HasColumnName("PasswordSalt");
                entity.Property(e => e.role)
                  .HasColumnName("role");
            });


            modelBuilder.Entity<EventMaster>(entity =>
            {
                entity.ToTable("EventMaster");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.event_Type)
                    .HasColumnName("event_Type")
                    .HasMaxLength(100);

                entity.Property(e => e.name)
                    .HasColumnName("name");
                entity.Property(e => e.startDateTime)
                  .HasColumnName("startDateTime");
                entity.Property(e => e.endDateTime)
                  .HasColumnName("endDateTime");
                entity.Property(e => e.user_Id)
                 .HasColumnName("user_Id");
            });

        }





    }
}
