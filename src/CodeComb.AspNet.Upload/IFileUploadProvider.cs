using System;

namespace CodeComb.AspNet.Upload
{
    public interface IFileUploadProvider
    {
        Models.File Get(Guid id);
        void Delete(Guid id);
        Guid Set(Models.File blob);
    }
}
