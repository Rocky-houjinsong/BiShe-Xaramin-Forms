using System;
using System.Collections.Generic;
using Demo.Models;
using Demo.Services;
// using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmHelpers;

namespace Demo.ViewModels
{
    public class DataBasePageViewModel : ViewModelBase
    {
        private IFavoriteStorage _favoriteStorage; 
        private RelayCommand _createDatabaseCommand;  //私有字段,
        /// <summary>
        /// 借助构造函数进行依赖注入
        /// </summary>
        /// <param name="favoriteStorage">参数</param>
        public DataBasePageViewModel(IFavoriteStorage favoriteStorage)
        {
            _favoriteStorage = favoriteStorage;
        }
        #region // 公有属性,值来自私有的字段成员,私有值为空,就初始化私有成员
        //初始化操作为一个RelayCommand,执行内容是抛出一个新异常[`还没实现`]
        public RelayCommand CreateDatabaseCommand =>
            _createDatabaseCommand ?? (_createDatabaseCommand =
                new RelayCommand(async () => await _favoriteStorage.CreateDatabaseAsync()));
        private RelayCommand _insertDatabaseCommand;  
       
        public RelayCommand InsertDatabaseCommand =>
            _insertDatabaseCommand ?? (_insertDatabaseCommand =
                new RelayCommand(async() => await _favoriteStorage.InsertDataAsync(new Models.Favorite{IsFavorite = true})));  
        private RelayCommand _readDatabaseCommand;  
       
        public RelayCommand ReadDatabaseCommand =>
            _readDatabaseCommand ?? (_readDatabaseCommand =
                new RelayCommand(async() => {
                   var  results = await _favoriteStorage.ReadDataAsync();
                   Favorites.AddRange(results);
                }));
        // 此处 可以简化成 async()=> Favorites.AddRange( await _favoriteStorage.ReadDataAsync())
        #endregion
        // 深切明白, 机遇和良人,平时有所积累才是自己的把握和 掌控的
        public ObservableRangeCollection<Favorite> Favorites { get; } =
            new ObservableRangeCollection<Favorite>();
    }
}
