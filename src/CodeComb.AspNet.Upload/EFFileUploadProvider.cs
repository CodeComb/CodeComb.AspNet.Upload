using System;
using System.Linq;
using Microsoft.Data.Entity;
using CodeComb.AspNet.Upload;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EFFileUploadProviderServiceCollectionExtensions
    {
        public static IFileUploadBuilder AddEntityFrameworkStorage<TContext>(this IFileUploadBuilder self)
            where TContext : DbContext, IBlobDbContext
        {
            self.Services.AddScoped<IFileUploadProvider, EFFileUploadProvider<TContext>>();
            return self;
        }
    }
}

namespace CodeComb.AspNet.Upload
{
    public class EFFileUploadProvider<TContext> : IFileUploadProvider
        where TContext : DbContext, IBlobDbContext
    {
        protected IBlobDbContext DbContext { get; set; }

        public EFFileUploadProvider(TContext db)
        {
            DbContext = db;
        }

        public void Delete(Guid id)
        {
            var blob = DbContext.Blobs.Where(x => x.Id == id).SingleOrDefault();
            if (blob != null)
            {
                DbContext.Blobs.Remove(blob);
                DbContext.SaveChanges();
            }
        }

        public Models.Blob Get(Guid id)
        {
            return DbContext.Blobs.Where(x => x.Id == id).SingleOrDefault(); 
        }

        public Guid Set(Models.Blob blob)
        {
            if (blob.Id != default(Guid) && DbContext.Blobs.Where(x => x.Id == blob.Id).SingleOrDefault() != null)
                Delete(blob.Id);
            DbContext.Blobs.Add(blob);
            DbContext.SaveChanges();
            return blob.Id;
        }
    }
}
