using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Tableau.Migration.Content;
using Tableau.Migration.Engine.Hooks.Mappings;
using Tableau.Migration.Resources;

namespace Csharp.ExampleApplication.Hooks.Mappings
{
    public class MigrationListWorkbookOwnerMapping : ContentMappingBase<IWorkbook>
    {
        private readonly Dictionary<string, string> _workbookOwnerEmails;

        public MigrationListWorkbookOwnerMapping(
            ISharedResourcesLocalizer localizer,
            ILogger<IContentMapping<IWorkbook>> logger,
            List<MigrationListEntry> migrationList)
            : base(localizer, logger)
        {
            // Use Id string for lookup
            _workbookOwnerEmails = migrationList.ToDictionary(e => e.WorkbookLuid, e => e.OwnerEmail);
        }

        public override Task<ContentMappingContext<IWorkbook>?> MapAsync(ContentMappingContext<IWorkbook> ctx, CancellationToken cancel)
        {
            var workbookId = ctx.ContentItem.Id.ToString();
            if (_workbookOwnerEmails.TryGetValue(workbookId, out var ownerEmail))
            {
                // Try to set the owner email if the property exists
                var owner = ctx.ContentItem.Owner;
                if (owner != null && owner.GetType().GetProperty("Email") != null)
                {
                    owner.GetType().GetProperty("Email")?.SetValue(owner, ownerEmail);
                }
                // If not possible, log a warning (optional)
                // _logger?.LogWarning("Could not set owner email for workbook {Id}", workbookId);
            }
            return ctx.ToTask();
        }
    }
}
