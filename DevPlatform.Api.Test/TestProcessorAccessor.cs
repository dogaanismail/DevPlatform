using DevPlatform.Data.Migrations;
using FluentMigrator;
using System.Collections.Generic;

namespace DevPlatform.Tests
{
    /// <summary>
    /// An <see cref="IProcessorAccessor"/> implementation that selects one generator by data settings
    /// </summary>
    public class TestProcessorAccessor : DevPlatformProcessorAccessor
    {
        #region Ctor

        public TestProcessorAccessor(IEnumerable<IMigrationProcessor> processors) : base(processors)
        {
        }

        #endregion

        #region Utils

        /// <summary>
        /// Configure processor
        /// </summary>
        /// <param name="processors">Collection of migration processors</param>
        protected override void ConfigureProcessor(IList<IMigrationProcessor> processors)
        {
            Processor = FindGenerator(processors, "SqlServer");
        }

        #endregion
    }
}
