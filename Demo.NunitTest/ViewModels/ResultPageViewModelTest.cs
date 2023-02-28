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
          //  resultPageViewModel.PageAppearingCommand.Execute(null); 多线程
          await resultPageViewModel.PageAppearingCommandFunction();
            Assert.AreEqual(20, resultPageViewModel.PoetryCollection.Count);
            Assert.AreEqual(2, statusList.Count); //验证status状态
            Assert.AreEqual(ResultPageViewModel.Loading, statusList[0]);
            Assert.AreEqual("", statusList[1]); 
            // 上述验证 状态 ,还可以验证数量,和内容是否正确
            // 验证 PageAppearingCommand在没有新的where条件下,不会再次加载 
            var poetryCollectionChanged = false;
            resultPageViewModel.PoetryCollection.CollectionChanged += (sender, args) => poetryCollectionChanged = true;
            await resultPageViewModel.PageAppearingCommandFunction();
            Assert.IsFalse(poetryCollectionChanged);

           // await resultPageViewModel.PoetryCollection.LoadMoreAsync();
            //Assert.AreEqual(30, resultPageViewModel.PoetryCollection.Count);
            //Assert.IsFalse(resultPageViewModel.PoetryCollection.CanLoadMore);
            //Assert.AreEqual(4, statusList.Count); 诗词数量为30 可以通过
            //Assert.AreEqual(ResultPageViewModel.Loading, statusList[2]);
            //Assert.AreEqual("", statusList[3]);
            //Assert.AreEqual(ResultPageViewModel.NoMoreResult, statusList[4]);

            await poetryStorage.CloseAsync();
        }

        
    }
}
