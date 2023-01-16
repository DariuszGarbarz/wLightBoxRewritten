using FluentAssertions;

namespace wLightBoxRewrittenTests
{
    public class EntryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var test = true;

            test.Should().BeTrue();
        }
    }
}