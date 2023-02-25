using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Demo.Models;
using SQLite;
using Xamarin.Essentials;

namespace Demo.Services
{
    public class PoetryStorage :IPoetryStorage
    {
        //********私有变量
        /// <summary>
        /// 数据库文件名  
        /// </summary>
        private const string DbName = "poetrydb.sqlite3";
        /// <summary>
        /// 数据库文件路径
        /// </summary>
        public static readonly string PoetryDbPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder
                    .LocalApplicationData), DbName);

        /// <summary>
        /// 打开数据库连接
        /// 打开数据库连接
        /// </summary>
        private SQLiteAsyncConnection _connection;

        private SQLiteAsyncConnection Connection => _connection ??
                                                   (_connection = new SQLiteAsyncConnection(PoetryDbPath));
        /// <summary>
        /// 初始化诗词存储
        /// </summary>
        /// <returns></returns>
        /// <returns></returns>
        public async Task InitializeAsync()
        { // 打开文件,传递路径 将需要关闭的初始化 扔到using中,文件操作 必须要关闭,using就是该效果
            using (var dbFilesStream = 
                   new FileStream(PoetryDbPath,FileMode.Create))
                // dbAssertStream 数据资源流
            using (var dbAssertStream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream(DbName))
            {
                await dbAssertStream.CopyToAsync(dbFilesStream);// 将目标文件拷贝到来源文件
            }

            Preferences.Set(PoetryStorageConstants.VersionKey, PoetryStorageConstants.Version); 
        }

        /// <summary>
        /// 诗词存储是否已经初始化.
        /// </summary>
        /// <returns></returns>
        public bool IsInitiallized() =>
            Preferences.Get(PoetryStorageConstants.VersionKey, PoetryStorageConstants.DefaultVersion) ==
            PoetryStorageConstants.Version;

        /// <summary>
        ///获取一个诗词
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Poetry> GetPoetryAsync(int id) =>
            await Connection.Table<Poetry>()
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IList<Poetry>> GetPoetriesAsync(Expression<Func<Poetry, bool>> where, int skip, int take) =>
            await Connection.Table<Poetry>().Where(where).Skip(skip).Take(take).ToListAsync();

    }
}
