using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using NUnit.Framework;

using NZBDash.Api.Models;
using NZBDash.Common.Mapping;
using NZBDash.Common.Models.ViewModels.NzbGet;

using Omu.ValueInjecter;

namespace NZBDash.Common.Tests.Mapping
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class NzbGetHistoryMapperTest
    {
        [TestCaseSource("TestCaseData")]
        public void NzbGetHistoryMapper(int id, string nzbName, int fileSize, string category, int health, int historyTime, string status, string name)
        {
            var source = new NzbGetHistoryResult
            {
                ID = id,
                NZBName = nzbName,
                FileSizeMB = fileSize,
                Category = category,
                Health = health,
                HistoryTime = historyTime,
                Status = status,
                Name = name
            };
            var target = new NzbGetHistoryViewModel();
            target.InjectFrom(new NzbGetHistoryMapper(), source);

            Assert.That(target.Id, Is.EqualTo(id));
            Assert.That(target.NzbName, Is.EqualTo(nzbName));
            Assert.That(target.FileSize, Is.EqualTo(fileSize.ToString()));
            Assert.That(target.Category, Is.EqualTo(category));
            Assert.That(target.Health, Is.EqualTo(health));
            Assert.That(target.HistoryTime, Is.EqualTo(historyTime));
            Assert.That(target.Status, Is.EqualTo(status));
            Assert.That(target.Name, Is.EqualTo(name));
        }

        private static IEnumerable<ITestCaseData> TestCaseData
        {
            get
            {
                var emptyStrings = new string[0];

                yield return
                    new TestCaseData(1,
                        "Name",
                        2042,
                        "Cat1",
                        97,
                        2000,
                        "Running",
                        "name2");

                yield return
                    new TestCaseData(1,
                        "LongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongName",
                        int.MaxValue,
                        "LongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongName",
                        int.MaxValue,
                        int.MaxValue,
                        "LongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongName",
                        "LongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongName");
            }
        }
    }
}
