using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace UploadMVC.Models
{
    public class FileContext : DbContext
    {
        public FileContext() : base("DbStoreConnection") { }
        public DbSet<File> Files { get; set; }
    }
}