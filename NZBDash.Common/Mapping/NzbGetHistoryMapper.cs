using System.Reflection;

using NZBDash.Api.Models;

using Omu.ValueInjecter.Injections;

namespace NZBDash.Common.Mapping
{
    public class NzbGetHistoryMapper : KnownSourceInjection<NzbGetHistoryResult>
    {
        protected override void Inject(NzbGetHistoryResult source, object target)
        {
            MappingHelper.MapMatchingProperties(target, source);

            var fileSize = target.GetType().GetProperty("FileSize", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (fileSize == null) return;

            var fileSizeVal = source.FileSizeMB;
            if (fileSizeVal == default(int)) return;

            fileSize.SetValue(target, fileSizeVal.ToString());
        }
    }
}
