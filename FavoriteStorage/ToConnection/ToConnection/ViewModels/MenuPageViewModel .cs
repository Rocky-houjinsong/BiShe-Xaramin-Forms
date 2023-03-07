using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ToConnection.Services;
using Xamarin.Forms;

namespace ToConnection.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        /******** 构造函数 ********/

        /*private IRootNavigationService rootNavigationService;

        public MenuPageViewModel(IPoetryStorage poetryStorage,
            IFavoriteStorage favoriteStorage,
            IRootNavigationService rootNavigationService)
        {
            this.rootNavigationService = rootNavigationService;

            Messenger.Default.Register<RootNavigationMessage>(this,
                message => SelectedMenuItem =
                    MenuItem.GetMenuItem(message.PageKey));
        }*/

        /******** 绑定属性 ********/

        /*public MenuItem SelectedMenuItem
        {
            get => _selectedMenuItem;
            set => Set(nameof(SelectedMenuItem), ref _selectedMenuItem, value);
        }*/

        /*private MenuItem _selectedMenuItem;*/

        /******** 绑定命令 ********/

        /*public RelayCommand<MenuItem> MenuItemTappedCommand =>
            _menuItemTappedCommand ?? (_menuItemTappedCommand =
                new RelayCommand<MenuItem>(async menuItem =>
                    await rootNavigationService.NavigateToAsync(
                        menuItem.PageKey)));*/

        /*private RelayCommand<MenuItem> _menuItemTappedCommand;*/
    }
}