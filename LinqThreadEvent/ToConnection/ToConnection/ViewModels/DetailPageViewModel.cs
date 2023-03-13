using System;
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
            set
            {
                Set(nameof(Poetry), ref _poetry, value);
                _isNewPoetry = true; //自己给放到 Favorite的Set访问器 无大语
            }
        }

        /// <summary>
        /// 收藏.
        /// </summary>
        public Favorite Favorite
        {
            get => _favorite;
            set => Set(nameof(Favorite), ref _favorite, value);
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
                // ReSharper disable once AsyncVoidLambda
                new RelayCommand(async () =>
                    await PageAppearingCommandFunction()));

        public async Task PageAppearingCommandFunction()
        {
            //新收藏,才会执行
            if (!_isNewPoetry) //TODO: 这里应该是 !_isNewPoetry
            {
                return;
            }

            //是新诗,先标记为false,即不是新诗
            _isNewPoetry = false;
            //读取 收藏
            //TODO:优化 
            /*先读取 数据库中,原始的收藏状态;
             * 将收藏状态保存到 原始收藏状态的属性汇中去
             * 然后再更新Favorite属性
             */
            var favorite = await _favoriteStorage.GetFavoriteAsync(Poetry.Id) ??
                           new Favorite { PoetryId = Poetry.Id };
            _isFavorite = favorite.IsFavorite;
            Favorite = favorite;
        }

        /// <summary>
        /// 收藏切换命令.
        /// </summary>
        private RelayCommand _favoriteToggledCommand;

        /// <summary>
        /// 收藏切换命令.
        /// </summary>
        public RelayCommand FavoriteToggledCommand =>
            _favoriteToggledCommand ?? (_favoriteToggledCommand =
                new RelayCommand(async () => await FavoriteToggledCommandFunction()));

        public async Task FavoriteToggledCommandFunction()
        {
            //TODO 判断更新后的 ToggleCommand 和原始的一样不,一样就跳出,说明用户没有修改收藏状态
            //一样执行,不一样就不执行
            if (Favorite.IsFavorite == _isFavorite)
            {
                return;
            }

            //第二次就不起作用,需要 更新该字段
            _isFavorite = Favorite.IsFavorite;

            await _favoriteStorage.SaveFavoriteAsync(Favorite);
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

        /// <summary>
        /// 诗词从数据库中的原始收藏状态;
        /// </summary>
        /// <remarks>应为私有,但为单元测试,所以公开</remarks>
        private bool _isFavorite;
    }
}