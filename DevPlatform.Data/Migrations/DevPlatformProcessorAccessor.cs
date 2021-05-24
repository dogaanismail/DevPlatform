using FluentMigrator;
using FluentMigrator.Exceptions;
using FluentMigrator.Runner.Processors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevPlatform.Data.Migrations
{
    /// <summary>
    /// An <see cref="IProcessorAccessor"/> implementation that selects one generator by data settings
    /// </summary>
    public class DevPlatformProcessorAccessor : IProcessorAccessor
    {
        #region Ctor

        public DevPlatformProcessorAccessor(IEnumerable<IMigrationProcessor> processors)
        {
            ConfigureProcessor(processors.ToList());
        }

        #endregion

        #region Utils

        /// <summary>
        /// Gets single processor by DatabaseType or DatabaseTypeAlias
        /// </summary>
        /// <param name="processors">Collection of migration processors</param>
        /// <param name="processorsId">DatabaseType or DatabaseTypeAlias</param>
        /// <returns></returns>
        protected virtual IMigrationProcessor FindGenerator(IList<IMigrationProcessor> processors,
            string processorsId)
        {
            if (processors.FirstOrDefault(p =>
                    p.DatabaseType.Equals(processorsId, StringComparison.OrdinalIgnoreCase) ||
                    p.DatabaseTypeAliases.Any(a => a.Equals(processorsId, StringComparison.OrdinalIgnoreCase))) is
                IMigrationProcessor processor)
                return processor;

            var generatorNames = string.Join(", ",
                processors.Select(p => p.DatabaseType).Union(processors.SelectMany(p => p.DatabaseTypeAliases)));

            throw new ProcessorFactoryNotFoundException(
                $@"A migration generator with the ID {processorsId} couldn't be found. Available generators are: {generatorNames}");
        }


        /// <summary>
        /// Configure processor
        /// </summary>
        /// <param name="processors">Collection of migration processors</param>
        protected virtual void ConfigureProcessor(IList<IMigrationProcessor> processors)
        {
            var dataSettings = DataSettingsManager.LoadSettings();

            var procs = processors.ToList();
            if (procs.Count == 0)
                throw new ProcessorFactoryNotFoundException("No migration processor registered.");

            if (dataSettings is null)
                Processor = procs.FirstOrDefault();
            else
            {
                switch (dataSettings.DataProvider)
                {
                    case DataProviderType.SqlServer:
                        Processor = FindGenerator(procs, "SqlServer");
                        break;
                    case DataProviderType.MySql:
                        Processor = FindGenerator(procs, "MySQL");
                        break;
                    default:
                        throw new ProcessorFactoryNotFoundException($@"A migration generator for Data provider type {dataSettings.DataProvider} couldn't be found.");
                }
            }
        }

        #endregion

        #region  Properties

        public IMigrationProcessor Processor { get; protected set; }

        #endregion
    }
}
