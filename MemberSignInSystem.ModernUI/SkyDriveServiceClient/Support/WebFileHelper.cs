using System;
using HgCo.WindowsLive.SkyDrive.Services.UsersService;

namespace HgCo.WindowsLive.SkyDrive.Support
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebFileHelper
    {
        /// <summary>
        /// Parses the category type.
        /// </summary>
        /// <param name="folderItem">The folder item.</param>
        /// <returns></returns>
        public static WebFileCategoryType ParseCategoryType(FolderItemResponse folderItem)
        {
            var categoryType = (WebFileCategoryType)Enum.Parse(
                typeof(WebFileCategoryType),
                folderItem.Type,
                true);
            return categoryType;
        }

        /// <summary>
        /// Parses the content type.
        /// </summary>
        /// <param name="folderItem">The folder item.</param>
        /// <returns></returns>
        public static string ParseContentType(FolderItemResponse folderItem)
        {
            var contentType = folderItem != null && folderItem.Content != null ?
                folderItem.Content.Type : null;
            return contentType;
        }
    }
}
