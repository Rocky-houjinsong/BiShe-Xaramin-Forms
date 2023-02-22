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
        // 公有,值来自私有的成员,私有值为空,就初始化私有成员
        //初始化操作为一个RelayCommand,执行内容是抛出一个新异常[`还没实现`]
        public RelayCommand CreateDatabaseCommand =>
            _createDatabaseCommand ?? (_createDatabaseCommand =
                new RelayCommand(async () => await _favoriteStorage.CreateDatabaseAsync()));
        private RelayCommand _insertDatabaseCommand;  //私有字段,
        // 公有,值来自私有的成员,私有值为空,就初始化私有成员
        //初始化操作为一个RelayCommand,执行内容是抛出一个新异常[`还没实现`]
        public RelayCommand InsertDatabaseCommand =>
            _insertDatabaseCommand ?? (_insertDatabaseCommand =
                new RelayCommand(async() => await _favoriteStorage.InsertDataAsync(new Models.Favorite{IsFavorite = true})));  //这个才是业务
        private RelayCommand _readDatabaseCommand;  //私有字段,
        // 公有,值来自私有的成员,私有值为空,就初始化私有成员
        //初始化操作为一个RelayCommand,执行内容是抛出一个新异常[`还没实现`]
        public RelayCommand ReadDatabaseCommand =>
            _readDatabaseCommand ?? (_readDatabaseCommand =
                new RelayCommand(async() => {
                   var  results = await _favoriteStorage.ReadDataAsync();
                       }));

        public ObservableRangeCollection<Favorite> Favorites { get; }
    }
}
