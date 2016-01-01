using System.Globalization;
using Confetti;
using Xunit;

namespace ConfettiTests
{
    public class DefaultSettingsParserTests
    {
        [Fact]
        public void CanParseStrings()
        {
            var sut = new DefaultSettingParser();
            var result = sut.Parse<string>("some string");

            Assert.False(result.IsFailure);
            Assert.Equal("some string", result.Value);
        }

        [Fact]
        public void CanParseInts()
        {
            var sut = new DefaultSettingParser();
            var result = sut.Parse<int>("112233");

            Assert.False(result.IsFailure);
            Assert.Equal(112233, result.Value);
        }

        [Fact]
        public void CanParseDoubles()
        {
            var sut = new DefaultSettingParser();
            var result = sut.Parse<double>("12.5");

            Assert.False(result.IsFailure);
            Assert.Equal(12.5, result.Value);
        }

        [Fact]
        public void CanParseDoublesInNonEnUsLocale()
        {
            var oldCulture = CultureInfo.DefaultThreadCurrentCulture;
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ro");

            var sut = new DefaultSettingParser();
            var result = sut.Parse<double>("12.5");

            Assert.False(result.IsFailure);
            Assert.Equal(12.5, result.Value);

            CultureInfo.DefaultThreadCurrentCulture = oldCulture;
        }

        [Fact]
        public void ParsingRandomStringReturnsFailure()
        {
            var sut = new DefaultSettingParser();
            var result = sut.Parse<double>("this is not a double");

            Assert.True(result.IsFailure);
        }
    }
}
