using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Demo.Models;

namespace Demo.Services
{
    /// <summary>
    /// 诗词存储接口.
    /// </summary>
    public interface IPoetryStorage
    {
        /// <summary>
        /// 初始化诗词存储.
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();
        /// <summary>
        /// 诗词存储是否已经初始化.
        /// </summary>
        /// <returns></returns>
        bool IsInitiallized();
        /// <summary>
        /// 获取一个诗词
        /// </summary>
        /// <param name="id">诗词id.</param>
        /// <returns></returns>
        Task<Poetry> GetPoetryAsync(int id);

        /// <summary>
        /// 动态查询,获取满足给定条件的诗词集合
        /// </summary>
        /// <param name="where">Where条件</param>
        /// <param name="skip">跳过结果的数量</param>
        /// <param name="take">返回结果的数量</param>
        /// <returns></returns>
        Task<IList<Poetry>> GetPoetriesAsync(
            Expression<Func<Poetry, bool>> where, int skip, int take);

    }
}
