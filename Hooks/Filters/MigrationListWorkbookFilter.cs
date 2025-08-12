using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Tableau.Migration.Content;
using Tableau.Migration.Engine;
using Tableau.Migration.Engine.Hooks.Filters;
using Tableau.Migration.Resources;

namespace Csharp.ExampleApplication.Hooks.Filters
{
    public class MigrationListWorkbookFilter : ContentFilterBase<IWorkbook>
    {
        private readonly HashSet<string> _allowedLuids;

        public MigrationListWorkbookFilter(
            ISharedResourcesLocalizer localizer,
            ILogger<IContentFilter<IWorkbook>> logger,
            List<MigrationListEntry> migrationList)
            : base(localizer, logger)
        {
            _allowedLuids = migrationList.Select(e => e.WorkbookLuid).ToHashSet();
        }

        public override bool ShouldMigrate(ContentMigrationItem<IWorkbook> item)
        {
            return _allowedLuids.Contains(item.SourceItem.Id.ToString());
        }
    }
}
