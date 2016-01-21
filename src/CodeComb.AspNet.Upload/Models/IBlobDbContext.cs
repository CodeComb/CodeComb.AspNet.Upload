using CodeComb.AspNet.Upload.Models;

namespace Microsoft.Data.Entity
{
    public interface IBlobDbContext
    {
        DbSet<Blob> Blobs { get; set; }
        int SaveChanges();
    }
}
