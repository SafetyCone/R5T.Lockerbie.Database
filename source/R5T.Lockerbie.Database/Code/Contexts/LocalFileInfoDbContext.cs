using System;

using Microsoft.EntityFrameworkCore;


namespace R5T.Lockerbie.Database
{
    public class LocalFileInfoDbContext : DbContext
    {
        public DbSet<Entities.FileInfo> FileInfos { get; set; }


        public LocalFileInfoDbContext(DbContextOptions<LocalFileInfoDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.FileInfo>().HasAlternateKey(x => x.FileIdentity);
            modelBuilder.Entity<Entities.FileInfo>().HasAlternateKey(x => x.FilePath);
        }
    }
}
