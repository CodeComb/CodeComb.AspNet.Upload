using System;

namespace CodeComb.AspNet.Upload
{
    public interface IBlobProvider
    {
        Models.Blob Get(Guid id);
        void Delete(Guid id);
        Guid Set(Models.Blob blob);
    }
}
