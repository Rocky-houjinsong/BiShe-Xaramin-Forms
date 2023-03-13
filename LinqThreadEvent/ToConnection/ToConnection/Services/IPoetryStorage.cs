using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToConnection.Models;

namespace ToConnection.Services
{
    /// <summary>
    /// 诗词存储接口.
    /// </summary>
    public interface IPoetryStorage
    {
        /// <summary>
        /// 初始化诗词存储.
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();

        /// <summary>
        /// 诗词存储是否已经初始化.
        /// </summary>
        /// <returns></returns>
        bool IsInitiallized();

        /// <summary>
        /// 获取一个诗词
        /// </summary>
        /// <param name="id">诗词id.</param>
        /// <returns></returns>
        Task<Poetry> GetPoetryAsync(int id);

        /// <summary>
        /// 动态查询,获取满足给定条件的诗词集合
        /// </summary>
        /// <param name="where">Where条件</param>
        /// <param name="skip">跳过结果的数量</param>
        /// <param name="take">返回结果的数量</param>
        /// <returns></returns>
        Task<IList<Poetry>> GetPoetriesAsync(
            Expression<Func<Poetry, bool>> where, int skip, int take);
    }

    /// <summary>
    /// 诗词存储常量.
    /// </summary>
    public static class PoetryStorageConstants
    {
        /// <summary>
        /// 诗词数据库版本号
        /// </summary>
        public const int Version = 1;

        /// <summary>
        /// 默认的诗词数据库版本号.
        /// </summary>
        public const int DefaultVersion = 0;

        /// <summary>
        /// 诗词数据库版本号键
        /// </summary>
        public const string VersionKey =
            nameof(PoetryStorageConstants) + "." + nameof(Version);
        //"PoetryStorageConstants.Version"  不是计算得到的,是硬编码的,在编译阶段 变成字符串
    }
}