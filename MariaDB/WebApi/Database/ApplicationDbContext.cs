using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .ToTable("Items");
            modelBuilder.Entity<Item>().Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("Name");
            modelBuilder.Entity<Item>().Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("Description");
            modelBuilder.Entity<Item>().Property(b => b.LastUpdated)
                .HasColumnName("LastUpdated");
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            BeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(
           bool acceptAllChangesOnSuccess,
           CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            BeforeSave();
            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess,
                          cancellationToken));
        }

        private void BeforeSave()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AbstractEntity trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                        case EntityState.Added:
                            trackable.LastUpdated = DateTime.Now;
                            break;
                    }
                }
            }
        }
    }
}