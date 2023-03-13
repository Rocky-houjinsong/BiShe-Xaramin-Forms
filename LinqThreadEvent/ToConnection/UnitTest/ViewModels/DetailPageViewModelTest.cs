using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ToConnection.Models;
using ToConnection.Services;
using ToConnection.ViewModels;

namespace ValueConverter.UnitTest.ViewModels
{
    /// <summary>
    /// 诗词详情页测试.
    /// </summary>
    public class DetailPageViewModelTest
    {
        [Test]
        public async Task TestPageAppearingCommand()
        {
            var poetry = new Poetry { Id = 1 };
            var favorite = new Favorite { PoetryId = poetry.Id, IsFavorite = true };
            var favoriteStorageMock = new Mock<IFavoriteStorage>();
            favoriteStorageMock.Setup(p => p.GetFavoriteAsync(poetry.Id)).ReturnsAsync(favorite);
            var mockFavoriteStorage = favoriteStorageMock.Object;

            var detailPageViewModel = new DetailPageViewModel(mockFavoriteStorage);

            detailPageViewModel.Poetry = poetry;
            await detailPageViewModel.PageAppearingCommandFunction();


            try
            {
                Assert.AreSame(favorite, detailPageViewModel.Favorite);
            }
            catch (Exception e)
            {
                // Console.WriteLine("收藏实例: " + favorite.ToString());   //ToConnection.Models.Favorite 
                //  Console.WriteLine("详情页收藏值: " + detailPageViewModel.Favorite.ToString());  //null
                Console.WriteLine("完整堆栈输出:" + e.ToString());
                throw;
            }
        }
    }
}