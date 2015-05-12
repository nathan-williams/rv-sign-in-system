using System;
using System.Xml.Serialization;
using HgCo.WindowsLive.SkyDrive.Support.Net;

namespace HgCo.WindowsLive.SkyDrive.Services.UsersService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName="feed", Namespace = "http://www.w3.org/2005/Atom")]
    public class ListFolderItemsResponse : XmlWebResponse
    {
        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        /// <summary>Gets or sets the subtitle.</summary>
        /// <value>The subtitle.</value>
        [XmlElement(ElementName = "subtitle")]
        public string Subtitle { get; set; }

        /// <summary>Gets or sets the id.</summary>
        /// <value>The id.</value>
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        /// <summary>Gets or sets the updated.</summary>
        /// <value>The updated.</value>
        [XmlElement(ElementName = "updated")]
        public DateTime Updated { get; set; }

        /// <summary>Gets or sets the author.</summary>
        /// <value>The author.</value>
        [XmlElement(ElementName = "author")]
        public Author Author { get; set; }

        /// <summary>Gets or sets the links.</summary>
        /// <value>The links.</value>
        [XmlElement(ElementName = "link")]
        public Link[] Links { get; set; }

        /// <summary>Gets or sets the published.</summary>
        /// <value>The published.</value>
        [XmlElement(ElementName = "created", Namespace = "http://api.live.com/schemas")]
        public DateTime Created { get; set; }

        /// <summary>Gets or sets the owner cid.</summary>
        /// <value>The owner cid.</value>
        [XmlElement(ElementName = "ownerCid", Namespace = "http://api.live.com/schemas")]
        public long OwnerCid { get; set; }

        /// <summary>Gets or sets the resource id.</summary>
        /// <value>The resource id.</value>
        [XmlElement(ElementName = "resourceId", Namespace = "http://api.live.com/schemas")]
        public string ResourceId { get; set; }

        /// <summary>Gets or sets the parent id.</summary>
        /// <value>The parent id.</value>
        [XmlElement(ElementName = "parentId", Namespace = "http://api.live.com/schemas")]
        public string ParentId { get; set; }

        /// <summary>Gets or sets the size.</summary>
        /// <value>The size.</value>
        [XmlElement(ElementName = "size", Namespace = "http://api.live.com/schemas")]
        public long Size { get; set; }

        /// <summary>Gets or sets the storage version.</summary>
        /// <value>The storage version.</value>
        [XmlElement(ElementName = "storageVersion", Namespace = "http://api.live.com/schemas")]
        public int StorageVersion { get; set; }

        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [XmlElement(ElementName = "type", Namespace = "http://api.live.com/schemas")]
        public string Type { get; set; }

        /// <summary>Gets or sets the user role.</summary>
        /// <value>The user role.</value>
        [XmlElement(ElementName = "userRole", Namespace = "http://api.live.com/schemas")]
        public string UserRole { get; set; }

        /// <summary>Gets or sets the comment count.</summary>
        /// <value>The comment count.</value>
        [XmlElement(ElementName = "commentCount", Namespace = "http://api.live.com/schemas")]
        public int CommentCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is comment enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is comment enabled; otherwise, <c>false</c>.
        /// </value>
        [XmlElement(ElementName = "commentsEnabled", Namespace = "http://api.live.com/schemas")]
        public bool IsCommentEnabled { get; set; }

        /// <summary>Gets or sets the item count.</summary>
        /// <value>The item count.</value>
        [XmlElement(ElementName = "itemCount", Namespace = "http://api.live.com/schemas")]
        public int ItemCount { get; set; }

        /// <summary>Gets or sets the sharing level.</summary>
        /// <value>The sharing level.</value>
        [XmlElement(ElementName = "sharingLevel", Namespace = "http://api.live.com/schemas")]
        public SharingLevel SharingLevel { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        [XmlElement(ElementName = "sortOrder", Namespace = "http://api.live.com/schemas")]
        public string SortOrder { get; set; }

        /// <summary>Gets or sets the sort reverse.</summary>
        /// <value>The sort reverse.</value>
        [XmlElement(ElementName = "sortReverse", Namespace = "http://api.live.com/schemas")]
        public string SortReverse { get; set; }

        /// <summary>Gets or sets the quota used.</summary>
        /// <value>The quota used.</value>
        [XmlElement(ElementName = "quotaUsed", Namespace = "http://api.live.com/schemas")]
        public long QuotaUsed { get; set; }

        /// <summary>Gets or sets the item count.</summary>
        /// <value>The item count.</value>
        [XmlElement(ElementName = "emailKeyword", Namespace = "http://api.live.com/schemas")]
        public int EmailKeyword { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        [XmlElement(ElementName = "category", Namespace = "http://api.live.com/schemas")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is hidden.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is hidden; otherwise, <c>false</c>.
        /// </value>
        [XmlElement(ElementName = "hidden", Namespace = "http://api.live.com/schemas")]
        public bool IsHidden { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is special.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is special; otherwise, <c>false</c>.
        /// </value>
        [XmlElement(ElementName = "specialFolder", Namespace = "http://api.live.com/schemas")]
        public bool IsSpecial { get; set; }
        
        /// <summary>Gets or sets the total quota.</summary>
        /// <value>The total quota.</value>
        [XmlElement(ElementName = "totalQuota", Namespace = "http://api.live.com/schemas")]
        public long TotalQuota { get; set; }

        /// <summary>Gets or sets the size of the max file.</summary>
        /// <value>The size of the max file.</value>
        [XmlElement(ElementName = "maxFileSize", Namespace = "http://api.live.com/schemas")]
        public long MaxFileSize { get; set; }

        /// <summary>Gets or sets the folder items.</summary>
        /// <value>The folder items.</value>
        [XmlElement(ElementName = "entry")]
        public FolderItemResponse[] FolderItems { get; set; }
    }
}
