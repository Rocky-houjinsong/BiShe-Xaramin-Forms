using Xamarin.Forms;

namespace ToConnection.Services
{
    /// <summary>
    /// 内容页激活服务接口.
    /// </summary>
    public interface IContentPageActivationService
    {
        /// <summary>
        /// 激活内容页.
        /// </summary>
        /// <param name="pageKey">页面键.</param>
        ContentPage Activate(string pageKey);
    }
}