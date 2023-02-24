using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Models
{
    /// <summary>
    /// 诗词类.
    /// </summary>
    [SQLite.Table("works")]
    public class Poetry
    {
        /// <summary>
        /// 主键.
        /// </summary>
        [SQLite.Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 标题.
        /// </summary>
        [SQLite.Column("author_name")]
        public string AuthorName { get; set; }
        /// <summary>
        /// 朝代.
        /// </summary>
        [SQLite.Column("dynasty")]
        public string Dynasty { get; set; }
        /// <summary>
        /// 正文.
        /// </summary>
        [SQLite.Column("content")]
        public string Content { get; set; }
        /// <summary>
        /// 译文.
        /// </summary>
        [SQLite.Column("translation")]
        public string Translation { get; set; }
        /// <summary>
        /// 显示布局.
        /// </summary>
        [SQLite.Column("layout")]
        public string Layout { get; set; }
        /// <summary>
        /// 居中布局.
        /// </summary>
        public const string CenterLayout = "center";
        /// <summary>
        /// 缩进布局.
        /// </summary>
        public const string IndentLayout = "indent";
        /// <summary>
        /// 预览
        /// </summary>
        private string _snippet;
        /// <summary>
        /// 预览.
        /// </summary>
        [SQLite.Ignore]
        public string Snippet =>
            _snippet ?? (_snippet = Content.Split('。')[0].Replace("\r\n"," "));
    }
}
