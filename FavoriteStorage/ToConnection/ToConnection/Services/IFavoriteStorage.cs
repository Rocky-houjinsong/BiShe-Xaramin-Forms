using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToConnection.Services
{
    /// <summary>
    /// 收藏存储.
    /// </summary>
    public interface IFavoriteStorage
    {
        Task InitializeAsync();

        bool IsInitialized();
    }
}
