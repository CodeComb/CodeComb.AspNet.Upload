﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeComb.AspNet.Upload.Models;
using Microsoft.Data.Entity;

namespace CodeComb.AspNet.Upload.Sample.Models
{
    public class SampleContext : DbContext, IFileUploadDbContext
    {
        public DbSet<File> Files { get; set; }
    }
}
