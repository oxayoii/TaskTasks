using Microsoft.VisualStudio.TestPlatform.TestHost;
using Task1;

namespace TestProjectTask1
{
    public class UnitTest1
    {
        [Fact]
        public void CountSymbol_SingleCharacter()
        {
            string input = "a";
            var result = Program_.CountSymbol(input);
            Assert.Equal("a", result);
        }

        [Fact]
        public void CountSymbol_NoRepeats()
        {
            string input = "abc";
            var result = Program_.CountSymbol(input);
            Assert.Equal("abc", result);
        }
        [Fact]
        public void CountSymbol_AllRepeats()
        {
            string input = "aaabbb";
            var result = Program_.CountSymbol(input);
            Assert.Equal("a3b3", result);
        }
    }
}
