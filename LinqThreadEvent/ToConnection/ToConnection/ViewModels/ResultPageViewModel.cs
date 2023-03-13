using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ToConnection.Models;
using ToConnection.Services;
using Xamarin.Forms.Extended;

namespace ToConnection.ViewModels
{
    /// <summary>
    /// 搜索结果页ViewModel.
    /// </summary>
    public class ResultPageViewModel : ViewModelBase
    {
        //*****************************************私有变量
        /// <summary>
        /// 诗词存储
        /// </summary>
        //TODO 供演示使用的诗词存储, 未来应该删除
        private IPoetryStorage _poetryStorage;

        /// <summary>
        /// 收藏存储.
        /// </summary>
        //TODO 供演示使用的收藏存储, 未来应该删除
        private IFavoriteStorage _favoriteStorage;

        /// <summary>
        /// 加载状态.
        /// </summary>
        private string _status;

        private RelayCommand _pageAppearingCommand;

        /// <summary>
        /// Where条件
        /// </summary>
        private Expression<Func<Poetry, bool>> _where;

        /// <summary>
        /// 诗词点击命令.
        /// </summary>
        private RelayCommand<Poetry> _poetryTappedCommand;

        /// <summary>
        /// 内容导航服务.
        /// </summary>
        private IContentNavigationService _contentNavigationService;

        /// <summary>
        /// 能否加载更多
        /// </summary>
        private bool _canLoadMore;

        /// <summary>
        /// 是否为新查询
        /// </summary>
        private bool _isNewQuery;


        //******** **********************公开变量
        /// <summary>
        /// 一次显示的诗词数量.
        /// </summary>
        public const int PageSize = 20;

        /// <summary>
        /// 正在载入
        /// </summary>
        public const string Loading = "正在载入";

        /// <summary>
        /// 没有满足条件的结果
        /// </summary>
        public const string NoResult = "没有满足条件的结果";

        /// <summary>
        /// 没有更多结果
        /// </summary>
        public const string NoMoreResult = "没有更多结果";

        //******************************构造函数
        /// <summary>
        /// 搜索结果页ViewModel.
        /// </summary>
        /// <param name="poetryStorage">诗词存储</param>
        /// /// <param name="contentNavigationService">内容导航服务.</param>
        public ResultPageViewModel(IPoetryStorage poetryStorage, IContentNavigationService contentNavigationService,
            IFavoriteStorage favoriteStorage)
        {
            //TODO 供演示使用的诗词存储,未来应该删除.
            _poetryStorage = poetryStorage;
            //这个不需要删除
            _contentNavigationService = contentNavigationService;
            //TODO 供演示使用的收藏存储, 未来应该删除
            _favoriteStorage = favoriteStorage;


            PoetryCollection = new InfiniteScrollCollection<Poetry>
            {
                OnCanLoadMore = () => _canLoadMore,
                OnLoadMore = async () =>
                {
                    Status = Loading;
                    var poetries = await poetryStorage.GetPoetriesAsync(Where, PoetryCollection.Count, PageSize);
                    Status = "";
                    if (poetries.Count < PageSize)
                    {
                        _canLoadMore = false;
                        Status = NoMoreResult;
                    }

                    if (poetries.Count == 0 && PoetryCollection.Count == 0)
                    {
                        Status = NoResult;
                    }

                    return poetries;
                }
            };
        }


        //******************************绑定属性
        /// <summary>
        /// 诗词集合.
        /// </summary>
        public InfiniteScrollCollection<Poetry> PoetryCollection { get; }

        /// <summary>
        /// 加载状态.
        /// </summary>
        public string Status
        {
            get => _status;
            set => Set(nameof(Status), ref _status, value);
        }

        /// <summary>
        /// Where条件
        /// </summary>
        public Expression<Func<Poetry, bool>> Where
        {
            get => _where;
            set
            {
                Set(nameof(Where), ref _where, value);
                _isNewQuery = true;
                //_canLoadMore = true; 转移到 绑定命令中
            }
        }


        //*********************** 绑定命令


        public RelayCommand PageAppearingCommand =>
            _pageAppearingCommand ?? (_pageAppearingCommand = new RelayCommand(
                async () => await PageAppearingCommandFunction()));

        public async Task PageAppearingCommandFunction()
        {
            //TODO 供演示使用的Where条件,未来应该删除
            Where = Expression.Lambda<Func<Poetry, bool>>(
                Expression.Constant(true), Expression.Parameter(typeof(Poetry), "p"));
            // TODO 演示时使用的 ,初始化诗词存储数据库方法调用
            if (!_poetryStorage.IsInitiallized()) //解决无法返回的问题
            {
                await _poetryStorage.InitializeAsync();
            }


            //TODO 供演示使用的收藏存储, 未来应该删除
            //  await _favoriteStorage.InitializeAsync();

            if (!_isNewQuery)
            {
                return;
            }

            _isNewQuery = false;
            PoetryCollection.Clear();
            _canLoadMore = true;
            await PoetryCollection.LoadMoreAsync();
        }

        public RelayCommand<Poetry> PoetryTappedCommand =>
            _poetryTappedCommand ??
            (_poetryTappedCommand = new RelayCommand<Poetry>(
                async poetry => await PoetryTappedCommandFunction(poetry)));


        public async Task PoetryTappedCommandFunction(Poetry poetry) =>
            await _contentNavigationService.NavigateToAsync(ContentNavigationConstants.DetailPage, poetry);
    }
}