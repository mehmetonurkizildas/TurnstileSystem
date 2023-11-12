using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence.Common
{
    /// <summary>
    /// Veritabanı bağlantı sorunu için strateji belirleme
    /// </summary>
    public class DatabaseExecutionStrategy : ExecutionStrategy
    {
        public DatabaseExecutionStrategy(DbContext context, int maxRetryCount, TimeSpan maxRetryDelay) : base(context, maxRetryCount, maxRetryDelay)
        {
        }


        public DatabaseExecutionStrategy(ExecutionStrategyDependencies dependencies, int maxRetryCount, TimeSpan maxRetryDelay) : base(dependencies, maxRetryCount, maxRetryDelay)
        {
        }

        int connectionRetryCount = 0;
        protected override bool ShouldRetryOn(Exception exception)
        {
            Console.WriteLine($"Veri tabanı bağlantısı {++connectionRetryCount}. kez tekrardan deneniyor.");
            return true;
        }
    }
}