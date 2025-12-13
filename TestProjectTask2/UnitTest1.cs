using Task2;
namespace TestProjectTask2
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetCount_Initial_ReturnsZero()
        {
            await ResetCount();
            var count = await Program_.Server.GetCount();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task AddToCount_IncreasesCount()
        {
            await ResetCount();
            await Program_.Server.AddToCount(5);
            var count = await Program_.Server.GetCount();
            Assert.Equal(5, count);
        }

        [Fact]
        public async Task MultipleAddToCount_WorkCorrectly()
        {
            await ResetCount();
            await Program_.Server.AddToCount(3);
            await Program_.Server.AddToCount(7);
            var count = await Program_.Server.GetCount();
            Assert.Equal(10, count);
        }
        private async Task ResetCount()
        {
            await Program_.Server.AddToCount(-await Program_.Server.GetCount());
        }
    }
}
