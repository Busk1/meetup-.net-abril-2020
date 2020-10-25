using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MeetupAbril.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace MeetupAbril.Db.Contexts
{
    public class MeetupAbrilContextFactory : IDesignTimeDbContextFactory<MeetupAbrilContext>
    {
        public MeetupAbrilContext CreateDbContext(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MeetupAbril.Host"))
                .AddJsonFile($"appsettings.{environmentName}.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            var builder = new DbContextOptionsBuilder<MeetupAbrilContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");            
            builder.UseSqlServer(connectionString);
            return new MeetupAbrilContext(builder.Options);
        }
    }

    public partial class MeetupAbrilContext : DbContext
    {
        #region context
        public MeetupAbrilContext()
        {
        }

        public MeetupAbrilContext(DbContextOptions<MeetupAbrilContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Books> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>(entity =>
            {
                entity.Property(e => e.Lastname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.AuthorIdNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK__Libros__AuthorId__2E1BDC42");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        #endregion

    }
}
