using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ToConnection.Models;
using ToConnection.Services;

namespace ToConnection.ViewModels
{
    /// <summary>
    /// 诗词详情页ViewModel.
    /// </summary>
    public class DetailPageViewModel : ViewModelBase
    {
        //******************绑定属性

        public Poetry Poetry
        {
            get => _poetry;
            set => Set(nameof(Poetry), ref _poetry, value);
        }

        /// <summary>
        /// 收藏.
        /// </summary>
        public Favorite Favorite
        {
            get => _favorite;
            set
            {
                Set(nameof(Favorite), ref _favorite, value);
                _isNewPoetry = true;
            }
        }


        /// <summary>
        /// 诗词.
        /// </summary>
        private Poetry _poetry;


        /// <summary>
        /// 收藏.
        /// </summary>
        private Favorite _favorite;

        //********************绑定命令
        /// <summary>
        /// 页面显示命令.
        /// </summary>
        private RelayCommand _pageAppearingCommand;

        /// <summary>
        /// 页面显示命令. 读取诗词的收藏状态
        /// </summary>
        public RelayCommand PageAppearingCommand =>
            _pageAppearingCommand ?? (_pageAppearingCommand =
                new RelayCommand(async () => await PageAppearingCommandFunction()));

        public async Task PageAppearingCommandFunction()
        {
            //新收藏,才会执行

            if (!_isNewPoetry)
            {
                return;
            }

            //是新诗,先标记为false,即不是新诗
            _isNewPoetry = false;
            //读取 收藏
            Favorite = await _favoriteStorage.GetFavoriteAsync(Poetry.Id);
        }


        //***********************构造函数
        private IFavoriteStorage _favoriteStorage;

        /// <summary>
        /// 诗词详情页ViewModel.
        /// </summary>
        /// <param name="favoriteStorage">收藏存储.</param>
        public DetailPageViewModel(IFavoriteStorage favoriteStorage)
        {
            _favoriteStorage = favoriteStorage;
        }

        //*******************************私有变量
        /// <summary>
        /// 是否是新诗词.
        /// </summary>
        private bool _isNewPoetry;
    }
}