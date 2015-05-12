using System;
using HgCo.WindowsLive.SkyDrive.Services.UsersService;

namespace HgCo.WindowsLive.SkyDrive.Support
{
    /// <summary>
    /// Provides methods for handling <see cref="WebFolderInfo"/> specific properties and tasks.
    /// </summary>
    internal static class WebFolderHelper
    {
        /// <summary>
        /// Parses the CategoryType.
        /// </summary>
        /// <param name="folderItem">The folder item.</param>
        /// <returns>The parsed CategoryType.</returns>
        public static WebFolderCategoryType ParseCategoryType(FolderItemResponse folderItem)
        {
            WebFolderCategoryType categoryType = WebFolderCategoryType.None;
            if (folderItem != null && !String.IsNullOrEmpty(folderItem.Category))
            {
                var categoryTypeText = folderItem.Category.ToUpperInvariant();
                if (categoryTypeText.Contains("DOCUMENT"))
                    categoryType = WebFolderCategoryType.Documents;
                else if (categoryTypeText.Contains("FAVORITE"))
                    categoryType = WebFolderCategoryType.Favorites;
                else if (categoryTypeText.Contains("PHOTO"))
                    categoryType = WebFolderCategoryType.Photos;
            }
            return categoryType;
        }
    }
}
