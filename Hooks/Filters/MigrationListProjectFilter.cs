using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Tableau.Migration.Content;
using Tableau.Migration.Engine;
using Tableau.Migration.Engine.Hooks.Filters;
using Tableau.Migration.Resources;

namespace Csharp.ExampleApplication.Hooks.Filters
{
    public class MigrationListProjectFilter : ContentFilterBase<IProject>
    {
        private readonly HashSet<string> _allowedLuids;

        public MigrationListProjectFilter(
            ISharedResourcesLocalizer localizer,
            ILogger<IContentFilter<IProject>> logger,
            List<MigrationListEntry> migrationList)
            : base(localizer, logger)
        {
            _allowedLuids = migrationList.Select(e => e.ProjectLuid).ToHashSet();
        }

        public override bool ShouldMigrate(ContentMigrationItem<IProject> item)
        {
            return _allowedLuids.Contains(item.SourceItem.Id.ToString());
        }
    }
}
