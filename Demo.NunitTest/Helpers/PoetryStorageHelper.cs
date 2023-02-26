using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Demo.Services;
using Moq;

namespace Demo.NunitTest.Helpers
{
    /// <summary>
    /// 诗词存储帮助类.
    /// </summary>
    public class PoetryStorageHelper
    {
        /// <summary>
        /// 诗词数据库中诗词的总数量.
        /// </summary>
        public const int NumberPoetry = 139;
        /// <summary>
        /// 获得已初始化的诗词存储.
        /// </summary>
        /// <returns></returns>
        public static async Task<PoetryStorage>
            GetInitializedPoetryStorageAsync()
        {
            var poetryStorage = new PoetryStorage(new Mock<IPreferenceStorage>().Object);
            await poetryStorage.InitializeAsync();
            return poetryStorage;
        }
        /// <summary>
        /// 删除诗词数据库文件.
        /// </summary>
        public static void RemoveDatabaseFile() =>
            File.Delete(PoetryStorage.PoetryDbPath);
    }
}
