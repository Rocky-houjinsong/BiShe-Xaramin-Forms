using SQLite;

namespace ToConnection.Models
{
    /// <summary>
    /// 收藏类.
    /// Favorite存储于sqlite-net自动生成的数据库中，而Poetry存储于预置的数据库中，因此两者并不能进行表连接
    /// </summary>
    public class Favorite
    {
        /// <summary>
        /// 主键:诗词Id.
        /// </summary>
        [PrimaryKey]
        public int PoetryId { get; set; }

        /// <summary>
        /// 是否收藏.
        /// </summary>
        public bool IsFavorite { get; set; }
    }
}