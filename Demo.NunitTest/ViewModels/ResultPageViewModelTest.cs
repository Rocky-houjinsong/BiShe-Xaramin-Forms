using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.Models;
using Demo.NunitTest.Helpers;
using Demo.ViewModels;
using NUnit.Framework;

namespace Demo.NunitTest.ViewModels
{
    /// <summary>
    /// 搜索结果页 ViewModel测试.
    /// </summary>
    public  class ResultPageViewModelTest
    {
        /// <summary>
        /// 删除数据库文件.
        /// </summary>
        [SetUp, TearDown]
        public static void RemoveDatabaseFile() =>
            //  File.Delete(PoetryStorage.PoetryDbPath);
            PoetryStorageHelper.RemoveDatabaseFile(); //通过帮助类进行管理删除操作
        /// <summary>
        /// 测试诗词集合.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestPoetryCollection()
        {
            var where = Expression.Lambda<Func<Poetry, bool>>(
                Expression.Constant(true),
                Expression.Parameter(typeof(Poetry), "p"));
            var poetryStorage =
                await PoetryStorageHelper.GetInitializedPoetryStorageAsync();

            var resultPageViewModel = new ResultPageViewModel(poetryStorage);
            resultPageViewModel.Where = where;
            //监控status变化情况,自动的不停的变化,测试很麻烦, 需要一个list进行记录
            // 关联 propertychange事件,利用mvvm机制

            var statusList = new List<string>();
            resultPageViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(ResultPageViewModel.Status))
                {
                    statusList.Add(resultPageViewModel.Status);
                }
            };
            Assert.AreEqual(0, resultPageViewModel.PoetryCollection.Count);
            await poetryStorage.CloseAsync();
        }

        
    }
}
