using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using ToConnection.Models;

namespace ToConnection.ViewModels
{
    /// <summary>
    /// 诗词详情页ViewModel.
    /// </summary>
    public class DetailPageViewModel:ViewModelBase
    {
        //******************绑定属性

        public Poetry Poetry
        {
            get => _poetry;
            set => Set(nameof(Poetry), ref _poetry, value);
        }
        /// <summary>
        /// 诗词.
        /// </summary>
        private Poetry _poetry;
    }
}
