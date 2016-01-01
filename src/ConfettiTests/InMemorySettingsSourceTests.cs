using System.Collections.Generic;
using Confetti;
using Xunit;

namespace ConfettiTests
{
    public class InMemorySettingsSourceTests
    {
        private readonly IReadOnlyDictionary<string, string> settings = new Dictionary<string, string>
        {
            {"SomeKey", "Some value"}
        };

        [Fact]
        public void GetsExistingEnvironmentVariable()
        {
            var sut = new InMemorySettingsSource(settings);
            string value;
            var found = sut.TryGetRawSetting("SomeKey", out value);

            Assert.True(found, "SomeKey was not found");
            Assert.Equal("Some value", value);
        }

        [Fact]
        public void DoesntGetNonExistingEnvironmentVariable()
        {
            var sut = new InMemorySettingsSource(settings);
            string value;
            var found = sut.TryGetRawSetting("ThisKeyDoesNotExist", out value);

            Assert.False(found, "Strangely, a non existing key was found");
        }
    }
}
