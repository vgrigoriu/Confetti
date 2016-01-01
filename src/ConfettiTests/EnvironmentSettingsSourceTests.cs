using System;
using Confetti;
using Xunit;

namespace ConfettiTests
{
    public class EnvironmentSettingsSourceTests
    {
        [Fact]
        public void GetsExistingEnvironmentVariable()
        {
            var key = Guid.NewGuid().ToString();
            Environment.SetEnvironmentVariable(key, "this is a test");

            var sut = new EnvironmentSettingsSource();
            string value;
            var found = sut.TryGetRawSetting(key, out value);

            Assert.True(found, "Randomly generated key was not found");
            Assert.Equal("this is a test", value);
        }

        [Fact]
        public void DoesntGetNonExistingEnvironmentVariable()
        {
            var key = Guid.NewGuid().ToString();

            var sut = new EnvironmentSettingsSource();
            string value;
            var found = sut.TryGetRawSetting(key, out value);

            Assert.False(found, "Strangely, a randomly generated key was found");
        }
    }
}
