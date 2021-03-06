﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeComb.AspNet.Upload;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BlobServiceCollectionExtensions
    {
        public static IFileUploadBuilder AddFileUpload(this IServiceCollection self)
        {
            var builder = new FileUploadBuilder();
            builder.Services = self.AddRouting();
            return builder;
        }
    }
}
