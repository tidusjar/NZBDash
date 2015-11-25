using System;
using System.Diagnostics.CodeAnalysis;

using NUnit.Framework;

using NZBDash.Common.Mapping;

namespace NZBDash.Common.Tests.Mapping
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class MappingHelperTests
    {
        [Test]
        public void MappingHelperTest()
        {
            var source = new MockClassOne
            {
                boolType = true,
                intType = 2,
                longType = 100,
                stringType = "string"
            };
            var target = new MockClassOne();

            MappingHelper.MapMatchingProperties(target, source);


            Assert.That(target.boolType, Is.EqualTo(source.boolType));
            Assert.That(target.stringType, Is.EqualTo(source.stringType));
            Assert.That(target.longType, Is.EqualTo(source.longType));
            Assert.That(target.intType, Is.EqualTo(source.intType));

        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void MappingHelperTestMissingType()
        {
            var source = new MockClassTwo()
            {
                boolType = true,
                intType = 2,
                longType = 100,
                stringType = "string",
                uintType = 200
            };
            var target = new MockClassTwo();

            MappingHelper.MapMatchingProperties(target, source);
        }
    }


    public class MockClassOne
    {
        public int intType { get; set; }
        public string stringType { get; set; }
        public long longType { get; set; }
        public bool boolType { get; set; }
    }

    public class MockClassTwo
    {
        public int intType { get; set; }
        public string stringType { get; set; }
        public long longType { get; set; }
        public bool boolType { get; set; }
        public uint uintType { get; set; } // Invalid type for the mapper
    }
}
