using System;

using Microsoft.EntityFrameworkCore;


namespace R5T.Lockerbie.Database
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ForLocalFileInfoDbContext(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.FileInfo>().HasAlternateKey(x => x.FileIdentity);
            modelBuilder.Entity<Entities.FileInfo>().HasAlternateKey(x => x.FilePath);

            return modelBuilder;
        }
    }
}
