using System;
using System.Linq;
using Microsoft.Data.Entity;
using CodeComb.AspNet.Upload;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EFFileUploadProviderServiceCollectionExtensions
    {
        public static IFileUploadBuilder AddEntityFrameworkStorage<TContext>(this IFileUploadBuilder self)
            where TContext : DbContext, IFileUploadDbContext
        {
            self.Services.AddScoped<IFileUploadProvider, EFFileUploadProvider<TContext>>();
            return self;
        }
    }
}

namespace CodeComb.AspNet.Upload
{
    public class EFFileUploadProvider<TContext> : IFileUploadProvider
        where TContext : DbContext, IFileUploadDbContext
    {
        protected IFileUploadDbContext DbContext { get; set; }

        public EFFileUploadProvider(TContext db)
        {
            DbContext = db;
        }

        public void Delete(Guid id)
        {
            var blob = DbContext.Files
                .SingleOrDefault(x => x.Id == id);
            if (blob != null)
            {
                DbContext.Files.Remove(blob);
                DbContext.SaveChanges();
            }
        }

        public Models.File Get(Guid id)
        {
            return DbContext.Files.Where(x => x.Id == id).SingleOrDefault(); 
        }

        public Guid Set(Models.File file)
        {
            if (file.Id != default(Guid) && DbContext.Files.Where(x => x.Id == file.Id).SingleOrDefault() != null)
                Delete(file.Id);
            DbContext.Files.Add(file);
            DbContext.SaveChanges();
            return file.Id;
        }
    }
}
