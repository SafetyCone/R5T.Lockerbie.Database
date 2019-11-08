using System;
using System.ComponentModel.DataAnnotations;

using R5T.Philippi;


namespace R5T.Lockerbie.Database.Entities
{
    public class FileInfo
    {
        public int ID { get; set; }

        public Guid FileIdentity { get; set; }

        [StringLength(1028)]
        public string FilePath { get; set; }

        public FileFormat FileFormat { get; set; }
    }
}
