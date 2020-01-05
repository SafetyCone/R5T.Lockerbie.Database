using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<FileIdentity> Add(FilePath filePath, FileFormat fileFormat)
        {
            var guid = Guid.NewGuid();

            var fileIdentity = new FileIdentity(guid);

            var entity = new FileInfoEntity()
            {
                FileIdentity = fileIdentity.Value,
                FilePath = filePath.Value,
                FileFormat = fileFormat,
            };

            await this.Add(entity);

            return fileIdentity;
        }

        public async Task Add(FileInfo fileInfo)
        {
            var fileInfoEntity = fileInfo.ToEntityType();

            await this.Add(fileInfoEntity);
        }

        private async Task Add(FileInfoEntity fileInfoEntity)
        {
            await this.ExecuteInContextAsync(async dbContext =>
            {
                dbContext.Add(fileInfoEntity);

                await dbContext.SaveChangesAsync();
            });
        }

        public async Task Delete(FileIdentity fileIdentity)
        {
            await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.FileInfos.Where(x => x.FileIdentity == fileIdentity.Value).SingleAsync();

                dbContext.Remove(entity);

                await dbContext.SaveChangesAsync();
            });
        }

        public async Task Delete(FilePath filePath)
        {
            await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.FileInfos.Where(x => x.FilePath == filePath.Value).SingleAsync();

                dbContext.Remove(entity);

                await dbContext.SaveChangesAsync();
            });
        }

        public async Task<bool> Exists(FileIdentity fileIdentity)
        {
            var exists = await this.ExecuteInContextAsync(async dbContext =>
            {
                var entityOrDefault = await dbContext.FileInfos.Where(x => x.FileIdentity == fileIdentity.Value).SingleOrDefaultAsync();

                var output = entityOrDefault == default;
                return output;
            });

            return exists;
        }

        public async Task<bool> Exists(FilePath filePath)
        {
            var exists = await this.ExecuteInContextAsync(async dbContext =>
            {
                var entityOrDefault = await dbContext.FileInfos.Where(x => x.FilePath == filePath.Value).SingleOrDefaultAsync();

                var output = entityOrDefault == default;
                return output;
            });

            return exists;
        }

        public async Task<IEnumerable<FileInfo>> GetAll()
        {
            var fileInfos = await this.ExecuteInContextAsync(async dbContext =>
            {
                var fileInfoEntities = await dbContext.FileInfos.ToListAsync(); // Query now since DB context will get disposed.

                var output = fileInfoEntities.Select(x => x.ToAppType());
                return output;
            });

            return fileInfos;
        }

        public async Task<FileFormat> GetFileFormat(FileIdentity fileIdentity)
        {
            var fileFormat = await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.FileInfos.Where(x => x.FileIdentity == fileIdentity.Value).SingleAsync();

                var output = entity.FileFormat;
                return output;
            });

            return fileFormat;
        }

        public async Task<FileFormat> GetFileFormat(FilePath filePath)
        {
            var fileFormat = await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.FileInfos.Where(x => x.FilePath == filePath.Value).SingleAsync();

                var output = entity.FileFormat;
                return output;
            });

            return fileFormat;
        }

        public async Task<FileIdentity> GetFileIdentity(FilePath filePath)
        {
            var fileIdentity = await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.FileInfos.Where(x => x.FilePath == filePath.Value).SingleAsync();

                var output = new FileIdentity(entity.FileIdentity);
                return output;
            });

            return fileIdentity;
        }

        public async Task<FileInfo> GetFileInfo(FileIdentity fileIdentity)
        {
            var fileInfo = await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.FileInfos.Where(x => x.FileIdentity == fileIdentity.Value).SingleAsync();

                var output = entity.ToAppType();
                return output;
            });

            return fileInfo;
        }

        public async Task<FileInfo> GetFileInfo(FilePath filePath)
        {
            var fileInfo = await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.FileInfos.Where(x => x.FilePath == filePath.Value).SingleAsync();

                var output = entity.ToAppType();
                return output;
            });

            return fileInfo;
        }

        public async Task<FilePath> GetFilePath(FileIdentity fileIdentity)
        {
            var filePath = await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.FileInfos.Where(x => x.FileIdentity == fileIdentity.Value).SingleAsync();

                var output = new FilePath(entity.FilePath);
                return output;
            });

            return filePath;
        }

        public async Task SetFileFormat(FileIdentity fileIdentity, FileFormat fileFormat)
        {
            await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.FileInfos.Where(x => x.FileIdentity == fileIdentity.Value).SingleAsync();

                entity.FileFormat = fileFormat;

                await dbContext.SaveChangesAsync();
            });
        }

        public async Task SetFileFormat(FilePath filePath, FileFormat fileFormat)
        {
            await this.ExecuteInContextAsync(async dbContext =>
            {
                var entity = await dbContext.FileInfos.Where(x => x.FilePath == filePath.Value).SingleAsync();

                entity.FileFormat = fileFormat;

                await dbContext.SaveChangesAsync();
            });
        }
    }
}
