using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace CodeComb.AspNet.Upload.Models
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder SetupBlob(this ModelBuilder self)
        {
            return self.Entity<File>(e =>
            {
                e.HasIndex(x => x.Time);
                e.HasIndex(x => x.FileName);
            });
        }
    }
}
