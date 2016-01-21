using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeComb.AspNet.Upload
{
    public class AnonymousUploadAuthorizationProvider : IUploadAuthorizationProvider
    {
        public bool IsAbleToUpload()
        {
            return true;
        }
    }
}
