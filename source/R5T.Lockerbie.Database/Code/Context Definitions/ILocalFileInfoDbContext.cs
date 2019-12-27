using System;

using Microsoft.EntityFrameworkCore;


namespace R5T.Lockerbie.Database
{
    public interface ILocalFileInfoDbContext
    {
        DbSet<Entities.FileInfo> FileInfos { get; set; }
    }
}
