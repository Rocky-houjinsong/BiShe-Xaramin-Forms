using System;
using System.Collections.Generic;
using System.ComponentModel; // INotifyPropertyChanged引用
using System.Runtime.CompilerServices;
using System.Text;
using GalaSoft.MvvmLight.Command;

//using GalaSoft.MvvmLight.Command;

namespace Demo.ViewModels
{
    public class AnotherPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 结果
        /// </summary>
        private string _result;
        /// <summary>
        /// 提供机制进行数据的处理
        /// </summary>
        public string Result
        {
            get => _result;
            set
            {
                if (value == _result)
                    return;
                _result = value;
                //PCEA 该参数是告诉监听PC事件的人,哪一个属性发生了变化,把属性的名字告诉你
                // 当PC事件触发的时候,,我 就会收到该属性
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
            }
        }
        /// <summary>
        /// Hello命令
        /// 通过 OpenSilver.MvvmLightLibs进行导入,是最原始的MVVM创建方式
        /// </summary>
        private RelayCommand _helloCommand;

        public RelayCommand HelloCommand => _helloCommand ?? new RelayCommand(() => Result = "HelloWorld!");
        //后续需要将 View 和ViewModel 关联起来
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
