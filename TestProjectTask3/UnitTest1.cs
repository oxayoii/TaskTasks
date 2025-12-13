using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;

namespace TestProjectTask3
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("01.01.2022 12:00:00.12345 INFORMATION ВызвавшийМетод:TestMethod", true)]
        [InlineData("2022-01-01 12:00:00.1234|ERROR|123|TestMethod|Error message", false)]
        public void IsFormat1_ReturnsExpected(string line, bool expected)
        {
            bool result = Program_.IsFormat1(line);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("01.01.2022 12:00:00.12345 INFORMATION ВызвавшийМетод:TestMethod", false)]
        [InlineData("2022-01-01 12:00:00.1234|ERROR|123|TestMethod|Error message", true)]
        public void IsFormat2_ReturnsExpected(string line, bool expected)
        {
            bool result = Program_.IsFormat2(line);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ProcessFormat1_WritesExpectedOutput()
        {
            string line = "01.01.2022 12:00:00.12345 INFORMATION ВызвавшийМетод:TestMethod";

            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                Program_.ProcessFormat1(line, writer);
            }

            string output = sb.ToString();
            Assert.Contains("01-01-2022", output);
            Assert.Contains("12:00:00.12345", output);
            Assert.Contains("INFO", output);
            Assert.Contains("TestMethod", output);
            Assert.Contains("ВызвавшийМетод:", output);
        }

        [Fact]
        public void ProcessFormat2_WritesExpectedOutput()
        {
            string line = "2022-01-01 12:00:00.1234|ERROR|123|TestMethod|Error message";

            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                Program_.ProcessFormat2(line, writer);
            }

            string output = sb.ToString();
            Assert.Contains("01-01-2022", output);
            Assert.Contains("12:00:00.1234", output);
            Assert.Contains("ERROR", output);
            Assert.Contains("TestMethod", output);
            Assert.Contains("Error message", output);
        }
    }
}
