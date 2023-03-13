using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using ToConnection.Models;

namespace ToConnection.Services
{
    /// <summary>
    /// 收藏存储.
    /// </summary>
    public class FavoriteStorage : IFavoriteStorage
    {
        //************************* 公开变量
        /// <summary>
        /// 数据库文件路径.
        /// </summary>
        public static readonly string FavoriteDbPath =
            Path.Combine(
                Environment.GetFolderPath
                (Environment.SpecialFolder
                    .LocalApplicationData), DbName);


        //*****************************私有变量
        /// <summary>
        /// 数据库文件名.
        /// </summary>
        private const string DbName = "favoritedb.sqlite3";

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        private SQLiteAsyncConnection _connection;

        private SQLiteAsyncConnection Connection => _connection ??
                                                    (_connection = new SQLiteAsyncConnection(FavoriteDbPath));

        /// <summary>
        /// 偏好存储.
        /// 就需要 构造函数 来引入
        /// </summary>
        private IPreferenceStorage _preferenceStorage;


        //************************** 继承方法 
        public async Task InitializeAsync()
        {
            await Connection.CreateTableAsync<Favorite>();
            _preferenceStorage.Set(FavoriteStorageConstants.VersionKey, FavoriteStorageConstants.Version);
        }

        public bool IsInitialized() =>
            _preferenceStorage.Get(FavoriteStorageConstants.VersionKey, FavoriteStorageConstants.DefaultVersion) ==
            FavoriteStorageConstants.Version;


        public async Task<Favorite> GetFavoriteAsync(int poetryId) =>
            await Connection.Table<Favorite>()
                .FirstOrDefaultAsync(p => p.PoetryId == poetryId);

        public async Task SaveFavoriteAsync(Favorite favorite) =>
            //  await Connection.UpdateAsync(favorite); 先插入,才能更新 ;
            await Connection.InsertOrReplaceAsync(favorite);


        public async Task<IList<Favorite>> GetFavoritesAsync() =>
            await Connection.Table<Favorite>().Where(p => p.IsFavorite == true).ToListAsync();


        //*********************************公开方法
        /// <summary>
        /// 收藏存储.
        /// </summary>
        /// <param name="preferenceStorage">偏好存储.</param>
        public FavoriteStorage(IPreferenceStorage preferenceStorage)
        {
            _preferenceStorage = preferenceStorage;
        }

        /// <summary>
        /// 关闭收藏数据库.
        /// </summary>
        public async Task CloseAsync() => await Connection.CloseAsync();
    }
}