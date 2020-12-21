using Bonliva.PersonNumber;
using Xunit;

namespace PersonNumberTest
{
    public class SwedishPersonNumberTests
    {
        [Theory]
        [InlineData("19870420-8258")]
        [InlineData("198704208258")]
        [InlineData("8704208258")]
        [InlineData("870420-8258")]
        [InlineData("19870420-8258 - Test doc.pdf")]
        [InlineData("198704208258 - Test doc.pdf")]
        [InlineData("870420-8258 - Test doc.pdf")]
        [InlineData("8704208258 - Test doc.pdf")]
        public void ExtractsFromString(string input)
        {
            var num = SwedishPersonNumber.Parse(input);
            Assert.Equal(1987, num!.Year);
            Assert.Equal(4, num!.Month);
            Assert.Equal(20, num!.Day);
            Assert.Equal(8258, num!.Number);
        }
    }
}