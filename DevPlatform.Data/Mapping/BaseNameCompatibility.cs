using DevPlatform.Core.Domain.Chat;
using DevPlatform.Core.Domain.Friendship;
using DevPlatform.Core.Domain.Portal;
using System;
using System.Collections.Generic;

namespace DevPlatform.Data.Mapping
{
    /// <summary>
    /// Base instance of backward compatibility of table and column naming
    /// </summary>
    public partial class BaseNameCompatibility : INameCompatibility
    {
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            //{ typeof(ForumPost), "Forums_Post" },
        };

        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            { (typeof(Post), "CreatedBy"), "CreatedBy_Id" },
            { (typeof(Post), "ModifiedBy"), "ModifiedBy_Id" },
            { (typeof(PostComment), "CreatedBy"), "CreatedBy_Id" },
            { (typeof(PostComment), "ModifiedBy"), "ModifiedBy_Id" },
            { (typeof(PostImage), "CreatedBy"), "CreatedBy_Id" },
            { (typeof(PostImage), "ModifiedBy"), "ModifiedBy_Id" },
            { (typeof(PostVideo), "CreatedBy"), "CreatedBy_Id" },
            { (typeof(PostVideo), "ModifiedBy"), "ModifiedBy_Id" },
            { (typeof(Friend), "CreatedBy"), "CreatedBy_Id" },
            { (typeof(Friend), "ModifiedBy"), "ModifiedBy_Id" },
            { (typeof(FriendRequest), "CreatedBy"), "CreatedBy_Id" },
            { (typeof(FriendRequest), "ModifiedBy"), "ModifiedBy_Id" },
            { (typeof(ChatGroup), "CreatedBy"), "CreatedBy_Id" },
            { (typeof(ChatGroup), "ModifiedBy"), "ModifiedBy_Id" },
            { (typeof(ChatMessage), "CreatedBy"), "CreatedBy_Id" },
            { (typeof(ChatMessage), "ModifiedBy"), "ModifiedBy_Id" }
        };
    }
}
