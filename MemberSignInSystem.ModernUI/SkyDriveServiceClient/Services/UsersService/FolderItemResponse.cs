using System;
using System.Xml.Serialization;
using HgCo.WindowsLive.SkyDrive.Support.Net;

namespace HgCo.WindowsLive.SkyDrive.Services.UsersService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
    public class FolderItemResponse : XmlWebResponse
    {
        /// <summary>Gets or sets the id.</summary>
        /// <value>The id.</value>
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        /// <summary>Gets or sets the subtitle.</summary>
        /// <value>The subtitle.</value>
        [XmlElement(ElementName = "subtitle")]
        public string Subtitle { get; set; }

        /// <summary>Gets or sets the summary.</summary>
        /// <value>The summary.</value>
        [XmlElement(ElementName = "summary")]
        public string Summary { get; set; }

        /// <summary>Gets or sets the published.</summary>
        /// <value>The published.</value>
        [XmlElement(ElementName = "published")]
        public DateTime Published { get; set; }

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

        /// <summary>Gets or sets the sequence number.</summary>
        /// <value>The sequence number.</value>
        [XmlElement(ElementName = "sequenceNumber", Namespace = "http://api.live.com/schemas")]
        public int SequenceNumber { get; set; }

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

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        [XmlElement(ElementName = "category", Namespace = "http://api.live.com/schemas")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is special.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is special; otherwise, <c>false</c>.
        /// </value>
        [XmlElement(ElementName = "specialFolder", Namespace = "http://api.live.com/schemas")]
        public bool IsSpecial { get; set; }

        /// <summary>Gets or sets the name of the canonical.</summary>
        /// <value>The name of the canonical.</value>
        [XmlElement(ElementName = "canonicalName", Namespace = "http://api.live.com/schemas")]
        public string CanonicalName { get; set; }

        /// <summary>Gets or sets the content.</summary>
        /// <value>The content.</value>
        [XmlElement(ElementName = "content")]
        public Content Content { get; set; }

        /// <summary>Gets or sets the thumbnail.</summary>
        /// <value>The thumbnail.</value>
        [XmlElement(ElementName = "thumbnail", Namespace = "http://search.yahoo.com/mrss/")]
        public MediaThumbnail[] Thumbnails { get; set; }

        /// <summary>Gets or sets the content.</summary>
        /// <value>The content.</value>
        [XmlElement(ElementName = "content", Namespace = "http://search.yahoo.com/mrss/")]
        public MediaContent MediaContent { get; set; }

        /// <summary>Gets or sets the when taken.</summary>
        /// <value>The when taken.</value>
        [XmlElement(ElementName = "whenTaken", Namespace = "http://api.live.com/schemas")]
        public DateTime WhenTaken { get; set; }

        /// <summary>Gets or sets the tag count.</summary>
        /// <value>The tag count.</value>
        [XmlElement(ElementName = "numTags", Namespace = "http://api.live.com/schemas")]
        public int TagCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is tag enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is tag enabled; otherwise, <c>false</c>.
        /// </value>
        [XmlElement(ElementName = "taggingEnabled", Namespace = "http://api.live.com/schemas")]
        public bool IsTagEnabled { get; set; }
    }

}
