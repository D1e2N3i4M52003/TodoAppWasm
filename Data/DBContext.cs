using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using System.Data;

namespace Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => x.UserName)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Id);
            modelBuilder.Entity<Post>()
    .HasIndex(x => x.Id);
            modelBuilder.Entity<Post>()
   .HasOne(e => e.Owner)
   .WithMany(e => e.Posts)
   .HasForeignKey(e => e.OwnerId)
   .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}