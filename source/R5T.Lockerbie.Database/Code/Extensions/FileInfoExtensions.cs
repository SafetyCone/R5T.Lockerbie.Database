using System;

using R5T.Sparta;

using AppType = R5T.Lockerbie.FileInfo;
using EntityType = R5T.Lockerbie.Database.Entities.FileInfo;


namespace R5T.Lockerbie.Database
{
    public static class FileInfoExtensions
    {
        public static AppType ToAppType(this EntityType entityType)
        {
            var appType = new AppType()
            {
                FileIdentity = new FileIdentity(entityType.FileIdentity),
                FilePath = new FilePath(entityType.FilePath),
                FileFormat = entityType.FileFormat,
            };

            return appType;
        }

        public static EntityType ToEntityType(this AppType appType)
        {
            var entityType = new EntityType()
            {
                FileIdentity = appType.FileIdentity.Value,
                FilePath = appType.FilePath.Value,
                FileFormat = appType.FileFormat,
            };

            return entityType;
        }
    }
}
