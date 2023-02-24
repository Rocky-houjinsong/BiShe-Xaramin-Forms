using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.Models;

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
        private static readonly string PoetryDbPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder
                    .LocalApplicationData), DbName);
        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsInitiallized()
        {
            throw new NotImplementedException();
        }

        public Task<Poetry> GetPoetryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Poetry>> GetPoetriesAsync(Expression<Func<Poetry, bool>> where, int skip, int take)
        {
            throw new NotImplementedException();
        }
    }
}
