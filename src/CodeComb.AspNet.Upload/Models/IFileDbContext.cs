using CodeComb.AspNet.Upload.Models;

namespace Microsoft.Data.Entity
{
    public interface IFileUploadDbContext
    {
        DbSet<File> Files { get; set; }
        int SaveChanges();
    }
}
