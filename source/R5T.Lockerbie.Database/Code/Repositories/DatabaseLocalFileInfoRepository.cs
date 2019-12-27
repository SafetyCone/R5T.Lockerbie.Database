using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using R5T.Philippi;
using R5T.Sparta;
using R5T.Venetia;

using FileInfoEntity = R5T.Lockerbie.Database.Entities.FileInfo;


namespace R5T.Lockerbie.Database
{
    public class DatabaseLocalFileInfoRepository<TDbContext> : ProvidedDatabaseRepositoryBase<TDbContext>, ILocalFileInfoRepository
        where TDbContext: DbContext, ILocalFileInfoDbContext
    {
        public DatabaseLocalFileInfoRepository(DbContextOptions<TDbContext> dbContextOptions, IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextOptions, dbContextProvider)
        {
        }

        public FileIdentity Add(FilePath filePath, FileFormat fileFormat)
        {
            var guid = Guid.NewGuid();

            var fileIdentity = new FileIdentity(guid);

            var entity = new FileInfoEntity()
            {
                FileIdentity = fileIdentity.Value,
                FilePath = filePath.Value,
                FileFormat = fileFormat,
            };

            this.Add(entity);

            return fileIdentity;
        }

        public void Add(FileInfo fileInfo)
        {
            var fileInfoEntity = fileInfo.ToEntityType();

            this.Add(fileInfoEntity);
        }

        private void Add(FileInfoEntity fileInfoEntity)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                dbContext.FileInfos.Add(fileInfoEntity);

                dbContext.SaveChanges();
            }
        }

        public void Delete(FileIdentity fileIdentity)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var entity = dbContext.FileInfos.Where(x => x.FileIdentity == fileIdentity.Value).Single();

                dbContext.Remove(entity);

                dbContext.SaveChanges();
            }
        }

        public void Delete(FilePath filePath)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var entity = dbContext.FileInfos.Where(x => x.FilePath == filePath.Value).Single();

                dbContext.Remove(entity);

                dbContext.SaveChanges();
            }
        }

        public bool Exists(FileIdentity fileIdentity)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var entityOrDefault = dbContext.FileInfos.Where(x => x.FileIdentity == fileIdentity.Value).SingleOrDefault();

                var output = entityOrDefault == default;
                return output;
            }
        }

        public bool Exists(FilePath filePath)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var entityOrDefault = dbContext.FileInfos.Where(x => x.FilePath == filePath.Value).SingleOrDefault();

                var output = entityOrDefault == default;
                return output;
            }
        }

        public IEnumerable<FileInfo> GetAll()
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var output = dbContext.FileInfos.Select(x => x.ToAppType()).ToList(); // Query now since DB context will get disposed.
                return output;
            }
        }

        public FileFormat GetFileFormat(FileIdentity fileIdentity)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var entity = dbContext.FileInfos.Where(x => x.FileIdentity == fileIdentity.Value).Single();

                var output = entity.FileFormat;
                return output;
            }
        }

        public FileFormat GetFileFormat(FilePath filePath)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var entity = dbContext.FileInfos.Where(x => x.FilePath == filePath.Value).Single();

                var output = entity.FileFormat;
                return output;
            }
        }

        public FileIdentity GetFileIdentity(FilePath filePath)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var entity = dbContext.FileInfos.Where(x => x.FilePath == filePath.Value).Single();

                var output = new FileIdentity(entity.FileIdentity);
                return output;
            }
        }

        public FileInfo GetFileInfo(FileIdentity fileIdentity)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var entity = dbContext.FileInfos.Where(x => x.FileIdentity == fileIdentity.Value).Single();

                var output = entity.ToAppType();
                return output;
            }
        }

        public FileInfo GetFileInfo(FilePath filePath)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var entity = dbContext.FileInfos.Where(x => x.FilePath == filePath.Value).Single();

                var output = entity.ToAppType();
                return output;
            }
        }

        public FilePath GetFilePath(FileIdentity fileIdentity)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var entity = dbContext.FileInfos.Where(x => x.FileIdentity == fileIdentity.Value).Single();

                var output = new FilePath(entity.FilePath);
                return output;
            }
        }

        public void SetFileFormat(FileIdentity fileIdentity, FileFormat fileFormat)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var entity = dbContext.FileInfos.Where(x => x.FileIdentity == fileIdentity.Value).Single();

                entity.FileFormat = fileFormat;

                dbContext.SaveChanges();
            }
        }

        public void SetFileFormat(FilePath filePath, FileFormat fileFormat)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var entity = dbContext.FileInfos.Where(x => x.FilePath == filePath.Value).Single();

                entity.FileFormat = fileFormat;

                dbContext.SaveChanges();
            }
        }
    }
}
