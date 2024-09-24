using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Series> Series { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

       
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)  
                    .IsRequired();      
            });

           
            modelBuilder.Entity<Producer>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100) 
                    .IsRequired();     
            });

           
            modelBuilder.Entity<Series>(entity =>
            {
                entity.Property(e => e.VideoLink)
                    .HasMaxLength(500)  
                    .IsRequired();  
                     
                entity.Property(e => e.ImgLink)
                    .HasMaxLength(500)
                    .IsRequired();
              
                entity.HasOne(s => s.Producer)
                    .WithMany(p => p.Series)
                    .HasForeignKey(s => s.ProducerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.PrimaryGenre)
                    .WithMany(g => g.PrimarySeries)
                    .HasForeignKey(s => s.PrimaryGenreId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.SecondaryGenre)
                    .WithMany(g => g.SecondarySeries)
                    .HasForeignKey(s => s.SecondaryGenreId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
