using System;

namespace DevPlatform.Data.Migrations
{
    /// <summary>
    /// Attribute to exclude migration from the list for use during the install process
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class SkipMigrationOnInstallAttribute : Attribute
    {
    }
}
