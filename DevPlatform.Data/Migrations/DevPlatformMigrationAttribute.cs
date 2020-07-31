using FluentMigrator;
using System;
using System.Globalization;

namespace DevPlatform.Data.Migrations
{
    /// <summary>
    /// Attribute for a migration
    /// </summary>
    public partial class DevPlatformMigrationAttribute : MigrationAttribute
    {
        private static readonly string[] _dateFormats = { "yyyy-MM-dd HH:mm:ss", "yyyy.MM.dd HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss:fffffff", "yyyy.MM.dd HH:mm:ss:fffffff", "yyyy/MM/dd HH:mm:ss:fffffff" };

        /// <summary>
        /// Initializes a new instance of the NopMigrationAttribute class
        /// </summary>
        /// <param name="dateTime">The migration date time string to convert on version</param>
        public DevPlatformMigrationAttribute(string dateTime) :
            base(DateTime.ParseExact(dateTime, _dateFormats, CultureInfo.InvariantCulture).Ticks, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the NopMigrationAttribute class
        /// </summary>
        /// <param name="dateTime">The migration date time string to convert on version</param>
        /// <param name="description">The migration description</param>
        public DevPlatformMigrationAttribute(string dateTime, string description) :
            base(DateTime.ParseExact(dateTime, _dateFormats, CultureInfo.InvariantCulture).Ticks, description)
        {
        }
    }
}
