using System.IO;
using System.Threading.Tasks;
using Moq;
using ToConnection.Services;

namespace ValueConverter.UnitTest.Helpers
{
    /// <summary>
    /// 收藏存储帮助类.
    /// </summary>
    public class FavoriteStorageHelper
    {
        /// <summary>
        /// 获得已初始化的收藏存储
        /// </summary>
        public static async Task<FavoriteStorage> GetInitalizedFavoriteStorageAsync()
        {
            var favoriteStorage = new FavoriteStorage(new Mock<IPreferenceStorage>().Object);
            await favoriteStorage.InitializeAsync();
            return favoriteStorage;
        }

        /// <summary>
        /// 删除诗词收藏数据库文件.
        /// </summary>
        public static void RemoveDatabaseFile() =>
            File.Delete(FavoriteStorage.FavoriteDbPath);
    }
}