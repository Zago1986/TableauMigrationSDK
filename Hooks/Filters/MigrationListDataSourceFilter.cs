using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Tableau.Migration.Content;
using Tableau.Migration.Engine;
using Tableau.Migration.Engine.Hooks.Filters;
using Tableau.Migration.Resources;

namespace Csharp.ExampleApplication.Hooks.Filters
{
    public class MigrationListDataSourceFilter : ContentFilterBase<IDataSource>
    {
        private readonly HashSet<string> _allowedLuids;

        public MigrationListDataSourceFilter(
            ISharedResourcesLocalizer localizer,
            ILogger<IContentFilter<IDataSource>> logger,
            List<MigrationListEntry> migrationList)
            : base(localizer, logger)
        {
            _allowedLuids = migrationList.Select(e => e.DataSourceLuid).ToHashSet();
        }

        public override bool ShouldMigrate(ContentMigrationItem<IDataSource> item)
        {
            return _allowedLuids.Contains(item.SourceItem.Id.ToString());
        }
    }
}
