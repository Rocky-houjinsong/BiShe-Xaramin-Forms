using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ToConnection.Models;
using ToConnection.Services;
using ValueConverter.UnitTest.Helpers;

namespace ValueConverter.UnitTest.Services
{
    /// <summary>
    /// 收藏存储测试.
    /// </summary>
    public class FavoriteStorageTest
    {
        /// <summary>
        /// 删除数据库文件.
        /// </summary>
        [SetUp, TearDown]
        public static void RemoveDatabaseFile() =>
            FavoriteStorageHelper.RemoveDatabaseFile();

        /// <summary>
        /// 测试与初始化有关的函数.
        /// </summary>
        [Test]
        public async Task TestInitialize()
        {
            var preferenceStorageMock = new Mock<IPreferenceStorage>();
            preferenceStorageMock
                .Setup(p => p.Get(FavoriteStorageConstants.VersionKey, FavoriteStorageConstants.DefaultVersion))
                .Returns(FavoriteStorageConstants.Version - 1);
            var mockPreferenceStorage = preferenceStorageMock.Object;
            var favoriteStorage = new FavoriteStorage(mockPreferenceStorage);
            Assert.IsFalse(favoriteStorage.IsInitialized());
            preferenceStorageMock
                .Setup(p => p.Get(FavoriteStorageConstants.VersionKey, FavoriteStorageConstants.DefaultVersion))
                .Returns(FavoriteStorageConstants.Version);
            Assert.IsTrue(favoriteStorage.IsInitialized());

            Assert.IsFalse(File.Exists(FavoriteStorage.FavoriteDbPath));
            await favoriteStorage.InitializeAsync();
            Assert.IsTrue(File.Exists(FavoriteStorage.FavoriteDbPath));
            await favoriteStorage.CloseAsync();
            preferenceStorageMock.Verify(
                p => p.Set(FavoriteStorageConstants.VersionKey, FavoriteStorageConstants.Version), Times.Once);
        }

        // ****************测试 增删改查 
        /// <summary>
        /// 测试增删改查
        /// </summary>
        [Test]
        public async Task TestCrud()
        {
            var favoriteStorage = await FavoriteStorageHelper.GetInitalizedFavoriteStorageAsync();
            var favoritesList = new List<Favorite>
            {
                new Favorite { PoetryId = 1, IsFavorite = true },
                new Favorite { PoetryId = 2, IsFavorite = false }
            };
            await favoriteStorage.SaveFavoriteAsync(favoritesList[0]);
            await favoriteStorage.SaveFavoriteAsync(favoritesList[1]);

            var favoriteFromStorage = await favoriteStorage.GetFavoriteAsync(favoritesList[0].PoetryId);

            Assert.AreEqual(favoritesList[0].PoetryId, favoriteFromStorage.PoetryId);
            Assert.AreEqual(favoritesList[0].IsFavorite, favoriteFromStorage.IsFavorite);

            var favoriteListFromStorage = await favoriteStorage.GetFavoritesAsync();
            Assert.AreEqual(favoritesList.Count(p => p.IsFavorite == true),
                favoriteListFromStorage.Count); //返回一条,因为只有一条被收藏
            // Assert.AreEqual(favoritesList[1].PoetryId, favoriteListFromStorage[1].PoetryId);
            // Assert.AreEqual(favoritesList[1].IsFavorite, favoriteListFromStorage[1].IsFavorite);
            //结果数量 为 true 的数量
            Assert.AreEqual(favoritesList.Count(p => p.IsFavorite), favoriteListFromStorage.Count);
            //判断集合 favoritelistFromStorage中的所有项是否都满足 后面的条件
            Assert.IsTrue(favoriteListFromStorage.All(p => p.IsFavorite));
            await favoriteStorage.CloseAsync();
        }
    }
}