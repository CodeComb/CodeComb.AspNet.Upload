using System.Linq;
using Microsoft.AspNet.Http;
using CodeComb.AspNet.Upload;

namespace CodeComb.AspNet.Upload
{
    public class SignedUserUploadAuthorizationProvider : IUploadAuthorizationProvider
    {
        protected HttpContext httpContext { get; set; }

        public SignedUserUploadAuthorizationProvider (IHttpContextAccessor accessor)
        {
            httpContext = accessor.HttpContext;
        }

        public bool IsAbleToUpload()
        {
            return httpContext.User.Identities.Count() > 0;
        }
    }
}

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SignedUserUploadAuthorizationProviderServiceCollectionExtensions
    {
        public static IFileUploadBuilder AddSignedUserBlobUploadAuthorization(this IFileUploadBuilder self)
        {
            self.Services.AddSingleton<IUploadAuthorizationProvider, SignedUserUploadAuthorizationProvider>();
            return self;
        }
    }
}