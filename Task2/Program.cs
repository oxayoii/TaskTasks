namespace Task2
{
    class Program
    {
        static class Server
        {
            private static int count = 0;
            private static readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
            public static async Task<int> GetCount()
            {
                return await Task.Run(() =>
                {
                    _lock.EnterReadLock();
                    try
                    {
                        return count;
                    }
                    finally
                    {
                        _lock.ExitReadLock();
                    }
                });
            }
            public static async Task AddToCount(int value)
            {
                await Task.Run(() =>
                {
                    _lock.EnterWriteLock();
                    try
                    {
                        count += value;
                    }
                    finally
                    {
                        _lock.ExitWriteLock();
                    }
                });
            }
        }

        static async Task Main(string[] args)
        {
            // Запускаем несколько задач, которые одновременно увеличивают счетчик
            var tasks = new Task[]
            {
                Server.AddToCount(10),
                Server.AddToCount(20),
                Server.AddToCount(30),
            };

            await Task.WhenAll(tasks);

            int currentCount = await Server.GetCount();
            Console.WriteLine($"Общее значение счётчика после параллельных операций: {currentCount}");

            // Демонстрация асинхронных вызовов
            int value = await Server.GetCount();
            Console.WriteLine($"Текущее значение счётчика: {value}");
        }
    }
}
