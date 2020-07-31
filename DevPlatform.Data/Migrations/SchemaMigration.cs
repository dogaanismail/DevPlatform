using FluentMigrator;

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
            //TODO
            //_migrationManager.BuildTable<ForumPost>(Create);
        }
    }
}
