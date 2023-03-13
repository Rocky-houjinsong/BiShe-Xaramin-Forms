using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmHelpers;
using ToConnection.Models;
using ToConnection.Services;

namespace ToConnection.ViewModels
{
    /// <summary>
    /// 诗词收藏页ViewModel.
    /// </summary>
    public class FavoritePageViewModel : ViewModelBase
    {
        //***********************构造函数
        /// <summary>
        /// 诗词存储.
        /// </summary>
        private IPoetryStorage _poetryStorage;

        /// <summary>
        /// 收藏存储.
        /// </summary>
        private IFavoriteStorage _favoriteStorage;

        /// <summary>
        /// 内容导航服务.
        /// </summary>
        private IContentNavigationService _contentNavigationService;

        /// <summary>
        /// 构造函数 诗词收藏页ViewModel. Alt+ Insert 快速生成
        /// </summary>
        /// <param name="poetryStorage">诗词存储</param>
        /// <param name="favoriteStorage">收藏存储</param>
        /// <param name="contentNavigationService">内容导航服务</param>
        public FavoritePageViewModel(IPoetryStorage poetryStorage, IFavoriteStorage favoriteStorage,
            IContentNavigationService contentNavigationService)
        {
            _poetryStorage = poetryStorage;
            _favoriteStorage = favoriteStorage;
            _contentNavigationService = contentNavigationService;
        }

        //***********************绑定属性
        /// <summary>
        /// 诗词集合.
        /// </summary>
        public ObservableRangeCollection<Poetry> PoetryCollection { get; } = new
            ObservableRangeCollection<Poetry>();

        //*************************绑定命令
        /// <summary>
        /// 页面显示命令.
        /// </summary>
        private RelayCommand _pageAppearingCommand;

        /// <summary>
        /// 页面显示命令.
        /// </summary>
        public RelayCommand PageAppearingCommand =>
            _pageAppearingCommand ?? (_pageAppearingCommand =
                new RelayCommand(async () => await PageAppearingCommandFunction()));

        public async Task PageAppearingCommandFunction()
        {
            // PoetryCollection.Clear(); //每次导航前进行清空
            var canRun = false;

            if (!_isLoaded)
            {
                lock (_isLoadedLock)
                {
                    if (!_isLoaded)
                    {
                        _isLoaded = true;
                        canRun = true;
                    }
                }
            }

            if (!canRun)
            {
                return;
            }

            //  await Task.Delay(5000); // 5秒延时查看变化状态
            _isLoaded = true;
            var favoriteList = await _favoriteStorage.GetFavoritesAsync();
            // var poetryList = new List<Poetry>();
            // foreach (var favorite in favoriteList)
            // {
            //     await _poetryStorage.GetPoetryAsync(favorite.PoetryId);
            // }
            // PoetryCollection.AddRange(poetryList);   

            var poetryTaskList =
                favoriteList.Select(async p => await _poetryStorage.GetPoetryAsync(p.PoetryId)).ToList();

            var poetryList = (await Task.WhenAll(poetryTaskList)).ToList();
            PoetryCollection.AddRange(poetryList);
        }

        /// <summary>
        /// 诗词点击命令.
        /// </summary>
        private RelayCommand<Poetry> _poetryTappedCommand;

        public RelayCommand<Poetry> PoetryTappedCommand =>
            _poetryTappedCommand ??
            (_poetryTappedCommand = new RelayCommand<Poetry>(
                async poetry => await PoetryTappedCommandFunction(poetry)));


        public async Task PoetryTappedCommandFunction(Poetry poetry) =>
            await _contentNavigationService.NavigateToAsync(ContentNavigationConstants.DetailPage, poetry);

        //*******************************私有变量
        /// <summary>
        /// 页面是否已经加载
        /// </summary>
        private volatile bool _isLoaded;

        /// <summary>
        /// 页面是否已经加载锁.
        /// </summary>
        private readonly object _isLoadedLock = new object();
    }
}