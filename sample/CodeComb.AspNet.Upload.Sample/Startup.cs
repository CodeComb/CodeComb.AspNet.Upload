using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using CodeComb.AspNet.Upload.Sample.Models;

namespace CodeComb.AspNet.Upload.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddDbContext<SampleContext>(x => x.UseInMemoryDatabase())
                .AddInMemoryDatabase();

            services.AddFileUpload()
                .AddEntityFrameworkStorage<SampleContext>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();
            app.UseFileUpload();
            app.Run(async (context) =>
            {
                var db = context.RequestServices.GetRequiredService<SampleContext>();
                var images = db.Files.AsNoTracking()
                    .OrderByDescending(x => x.Time)
                    .Select(x => new { Id = x.Id, Name = x.FileName })
                    .ToList();
                var str = @"<html>
<head>
    <script src=""http://cdn.bootcss.com/jquery/1.12.0/jquery.js""></script>
    <script src=""/scripts/jquery.codecomb.fileupload.js""></script>
</head>
<body>
    <textarea id=""txtSample"">Drag an image and drop it in here, or paste an image in here.</textarea>
    <script>
        $('#txtSample').dragDropOrPaste();
    </script>
    <div>
        {IMAGES}
    </div>
</body>
</html>";
                var imageStr = "";
                foreach (var x in images)
                {
                    imageStr += $@"<img alt=""{x.Name}"" src=""/file/download/{x.Id}"" /><br />";
                }
                await context.Response.WriteAsync(str.Replace("{IMAGES}", imageStr));
            });
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
