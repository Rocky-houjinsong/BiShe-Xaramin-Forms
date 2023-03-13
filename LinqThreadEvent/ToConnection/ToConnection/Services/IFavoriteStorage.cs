using System.Collections.Generic;
using System.Threading.Tasks;
using ToConnection.Models;

namespace ToConnection.Services
{
    /// <summary>
    /// 收藏存储接口.
    /// </summary>
    public interface IFavoriteStorage
    {
        /// <summary>
        /// 初始化诗词收藏存储.
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();

        /// <summary>
        /// 收藏存储是否初始化.
        /// </summary>
        /// <returns></returns>
        bool IsInitialized();

        /// <summary>
        /// 获得诗词的收藏信息.
        /// </summary>
        /// <param name="poetryId">诗词Id.</param> 
        /// <returns></returns>
        Task<Favorite> GetFavoriteAsync(int poetryId);

        /// <summary>
        /// 保存收藏信息.
        /// </summary>
        /// <remarks>
        ///  收藏类中 已经隐含了诗词信息(PoetryId,是否收藏)
        /// </remarks>
        /// <param name="favorite">收藏.</param>
        /// <returns></returns>
        Task SaveFavoriteAsync(Favorite favorite);

        /// <summary>
        /// 获得所有收藏信息.
        /// </summary>
        /// <remarks>
        /// 不是获得所有诗词的 信息.一次性读取, 没有做翻页; 
        /// </remarks>
        /// <returns></returns>
        Task<IList<Favorite>> GetFavoritesAsync();
    }

    /// <summary>
    /// 收藏存储常量
    /// </summary>
    public static class FavoriteStorageConstants
    {
        /// <summary>
        /// 收藏数据库版本号.
        /// </summary>
        public const int Version = 1;

        /// <summary>
        /// 默认的收藏数据库版本号.
        /// </summary>
        public const int DefaultVersion = 0;

        /// <summary>
        /// 收藏数据库版本号键.
        /// </summary>
        public const string VersionKey =
            nameof(FavoriteStorageConstants) + "." + nameof(Version);
    }
}