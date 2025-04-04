using Microsoft.Extensions.Hosting;

namespace Cofoundry.Web;

/// <summary>
/// A path resolver that uses the asp.net hosting environment to get the application
/// content root path and use it to resolve relative paths to absolute paths.
/// </summary>
public class SitePathResolver : IPathResolver
{
    private readonly IHostEnvironment _hostingEnvironment;

    public SitePathResolver(
        IHostEnvironment hostingEnvironment
        )
    {
        _hostingEnvironment = hostingEnvironment;
    }

    public string MapPath(string? path)
    {   /*
         * IHostEnvironment.ContentRootPath is the default directory where appsettings.json and other content files are loaded in a hosted application,including ASP.NET apps.
         * This value defaults to Environment.CurrentDirectory, the current working directory of the application.
         * This allows the same app to be executed under different working directories and use the content from each directory.
         * 获取或设置包含应用程序内容文件的目录的绝对路径。
         * */
        var root = _hostingEnvironment.ContentRootPath;
        if (string.IsNullOrWhiteSpace(path))
        {
            return root;
        }
        path = path.TrimStart('~').TrimStart(['/', '\\']);

        var combinedPath = Path.Combine(root, path);

        return combinedPath;
    }
}
