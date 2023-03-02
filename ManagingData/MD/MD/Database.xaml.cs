using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Database : ContentPage
    {
        private SQLiteAsyncConnection connection = null; // 类成员变量,因为经常用
        public Database()
        {
            InitializeComponent();
        }
        //数据库还是要存放到数据库文件中的,
        private async void CreateDataBaseButton_OnClicked(object sender, EventArgs e
        )
        {
            //这是获取文件夹,但是我是要存文件,PC和Android的文件路径不一样
            pathShow.Text =
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var databasePath = Path.Combine(pathShow.Text, "db.db");
            connection = new SQLiteAsyncConnection(databasePath); // 数据库建立连接 
            // 创建数据库,仅只有Android平台下无法发现时候建立成功
            await connection.CreateTableAsync<Favorite>();
            // UWP,按照上面显示的路径,copy到 DB Browser里面 
        }

        private async void InsertDataButton_OnClicked(object sender, EventArgs e)
        {
            var favorite = new Favorite { IsFavorite = true }; // 光标回到new前 ,Alt+ Enter,Enter
            if (favorite != null) await connection.InsertAllAsync((System.Collections.IEnumerable)favorite);
        }

        private async void ReadDataButton_OnClicked(object sender, EventArgs e)
        {
            ResultListView.ItemsSource =
                await connection.Table<Favorite>().ToListAsync();
        }
    }

    public class Favorite
    {
        // Attribute
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool IsFavorite { get; set; }
    }
}
