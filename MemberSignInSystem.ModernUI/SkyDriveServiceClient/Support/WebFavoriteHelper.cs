using System;
using HgCo.WindowsLive.SkyDrive.Services.UsersService;

namespace HgCo.WindowsLive.SkyDrive.Support
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebFavoriteHelper
    {
        /// <summary>
        /// Parses the URL.
        /// </summary>
        /// <param name="folderItem">The folder item.</param>
        /// <returns></returns>
        public static string ParseUrl(FolderItemResponse folderItem)
        {
            string url = null;

            if (folderItem.Links != null)
            {
                foreach (var link in folderItem.Links)
                {
                    if (link.Rel.Equals("related", StringComparison.OrdinalIgnoreCase))
                    {
                        url = link.Url;
                        break;
                    }
                }
            }

            return url;
        }
    }
}
