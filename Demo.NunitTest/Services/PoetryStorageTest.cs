using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.Models;
using Demo.NunitTest.Helpers;
using Demo.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Demo.NunitTest.Services
{
    /// <summary>
    /// 诗词存储测试.
    /// </summary>
    class PoetryStorageTest
    {
        /// <summary>
        /// 删除数据库文件.
        /// </summary>
        [SetUp, TearDown]
        public static void RemoveDatabaseFile() =>
            //  File.Delete(PoetryStorage.PoetryDbPath);
            PoetryStorageHelper.RemoveDatabaseFile(); //通过帮助类进行管理删除操作

        /// <summary>
        /// 测试初始化诗词数据库存储
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestInitializeAsync()
        {
            Assert.IsFalse(File.Exists(PoetryStorage.PoetryDbPath));

            var preferenceStorageaMock = new Mock<IPreferenceStorage>();
            var mockPreferenceStorage = preferenceStorageaMock.Object;
            var poetryStorage = new PoetryStorage(mockPreferenceStorage);
            await poetryStorage.InitializeAsync();
            Assert.IsTrue(File.Exists(PoetryStorage.PoetryDbPath));
            preferenceStorageaMock.Verify(
                p => p.Set(PoetryStorageConstants.VersionKey,
                    PoetryStorageConstants.Version), Times.Once);
        }
        /// <summary>
        /// 测试诗词存储是否已经初始化.
        /// </summary>
        [Test]
        public void TestIsInitialized()
        {
            var preferenceStorageMock = new Mock<IPreferenceStorage>();
            preferenceStorageMock.Setup(p =>
                    p.Get(PoetryStorageConstants.VersionKey, PoetryStorageConstants.DefaultVersion))
                .Returns(PoetryStorageConstants.Version);
            var mockPreferenceStorage = preferenceStorageMock.Object;
            var poetryStorage = new PoetryStorage(mockPreferenceStorage);
            Assert.IsTrue(poetryStorage.IsInitiallized());
        }

        /// <summary>
        /// 测试诗词存储没有初始化.
        /// </summary>
        [Test]
        public void TestIsNotInitialized()
        {
            var preferenceStorageMock = new Mock<IPreferenceStorage>();
            preferenceStorageMock.Setup(p =>
                    p.Get(PoetryStorageConstants.VersionKey, PoetryStorageConstants.DefaultVersion))
                .Returns(PoetryStorageConstants.Version - 1);
            var mockPreferenceStorage = preferenceStorageMock.Object;
            var poetryStorage = new PoetryStorage(mockPreferenceStorage);
            Assert.IsFalse(poetryStorage.IsInitiallized());
        }
        /// <summary>
        /// 测试获取一个诗词
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestGetPoetryAsync()
        {
            var poetryStorage =
                await PoetryStorageHelper.GetInitializedPoetryStorageAsync();
            var poetry = await poetryStorage.GetPoetryAsync(10001);
            Assert.AreEqual("临江仙 · 夜归临皋", poetry.Name);
            await poetryStorage.CloseAsync();
        }
        /// <summary>
        /// 测试获取满足给定条件的诗词集合.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestGetPoetriesAsync()
        {
            var poetryStorage =
                await PoetryStorageHelper.GetInitializedPoetryStorageAsync();
            var where = Expression.Lambda<Func<Poetry, bool>>(
                Expression.Constant(true),
                Expression.Parameter(typeof(Poetry), "p"));
            var poetries =
                await poetryStorage.GetPoetriesAsync(where, 0, int.MaxValue);
            Assert.AreEqual(PoetryStorageHelper.NumberPoetry, poetries.Count);
            await poetryStorage.CloseAsync();
        }
    }
}
