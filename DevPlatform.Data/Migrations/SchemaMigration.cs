using DevPlatform.Core.Domain.Chat;
using DevPlatform.Core.Domain.Configuration;
using DevPlatform.Core.Domain.Friendship;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Notification;
using DevPlatform.Core.Domain.Portal;
using FluentMigrator;
using LinqToDB.Identity;

namespace DevPlatform.Data.Migrations
{
    [SkipMigrationOnUpdate]
    [DevPlatformMigration("2020/01/31 11:24:16:2551771", "DevPlatform.Data base schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        public SchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }

        /// <summary>
        /// Collect the UP migration expressions
        /// <remarks>
        /// We use an explicit table creation order instead of an automatic one
        /// due to problems creating relationships between tables
        /// </remarks>
        /// </summary>
        public override void Up()
        {
            //AppUser and AppUserDetail have to be created first!

            _migrationManager.BuildTable<AppUserDetail>(Create);
            _migrationManager.BuildTable<AppUser>(Create);
            _migrationManager.BuildTable<AppRole>(Create);
            _migrationManager.BuildTable<AppUserClaim>(Create);
            _migrationManager.BuildTable<AppRoleClaim>(Create);
            _migrationManager.BuildTable<AppUserLogin>(Create);
            _migrationManager.BuildTable<AppUserRole>(Create);
            _migrationManager.BuildTable<AppUserToken>(Create);
            _migrationManager.BuildTable<Setting>(Create);
            _migrationManager.BuildTable<ChatGroup>(Create);
            _migrationManager.BuildTable<ChatGroupUser>(Create);
            _migrationManager.BuildTable<ChatMessage>(Create);
            _migrationManager.BuildTable<UserNotification>(Create);
            _migrationManager.BuildTable<Post>(Create);
            _migrationManager.BuildTable<PostComment>(Create);
            _migrationManager.BuildTable<PostImage>(Create);
            _migrationManager.BuildTable<PostVideo>(Create);
            _migrationManager.BuildTable<Friend>(Create);
            _migrationManager.BuildTable<FriendRequest>(Create);
        }
    }
}
