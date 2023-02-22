using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Demo.Models;
using SQLite;

namespace Demo.Services
{
    public class FavoriteStorage : IFavoriteStorage
    {
        private SQLiteAsyncConnection connection = null; // 类成员变量,因为经常用
        public async Task CreateDatabaseAsync()
        //这是获取文件夹,但是我是要存文件,PC和Android的文件路径不一样
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db.db");
            connection = new SQLiteAsyncConnection(databasePath); // 数据库建立连接 
            // 创建数据库,仅只有Android平台下无法发现时候建立成功
            await connection.CreateTableAsync<Favorite>();
            // UWP,按照上面显示的路径,copy到 DB Browser里面
            
        }

        public async Task InsertDataAsync(Favorite favorite)
            => await connection.InsertAllAsync((System.Collections.IEnumerable)favorite);

        public async Task<IList<Favorite>> ReadDataAsync()
            => await connection.Table<Favorite>().ToListAsync();
        
    }
}
