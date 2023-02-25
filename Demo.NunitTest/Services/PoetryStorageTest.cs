using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Demo.Services;
using NUnit.Framework;

namespace Demo.NunitTest.Services
{
    /// <summary>
    /// 诗词存储测试.
    /// </summary>
    class PoetryStorageTest
    {
        /// <summary>
        /// 测试初始化诗词数据库存储
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestInitializeAsync()
        {
            Assert.IsFalse(File.Exists(PoetryStorage.PoetryDbPath));
            var poetryStorage = new PoetryStorage();
            await poetryStorage.InitializeAsync();
            Assert.IsTrue(File.Exists(PoetryStorage.PoetryDbPath));
        }
    }
}
