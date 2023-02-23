# 学习资源

*  MSDN - Xamarin官方文档

> 学会配置 Xamarin开发环境,
>
> 安卓开发好久没用 ,生疏许多

* 油管跟进学习 如何开始 开发和 调试 



---

## **第四版 ,讨论数据**

小型开发中,常用的数据类型分类 

一个是 数量少,数据没有共同特征

一个是 数量大 有共同特征;

本地存储,联网存储

少量本地数据 _ 键值存储

大量本地存储 _ 数据库/文件存储

远程数据 无论数量多少,统一使用Web服务进行管理;





| 存储位置 | 数据数量 |             |
| -------- | -------- | ----------- |
|          | 少量零星 | 大量成批    |
| 本地存储 | 键值存储 | 数据库/文件 |
| 远程存储 | Web服务  | Web服务     |



1. 主项目 Demo中, 新建项目 ,选择Xamarin-ContentPage, 设为PreferencePage,启动页的意思
   让它作为启动页的话,就需要在`App.xaml.cs` 中

```c#
public App()
{
	InitializeComponent();
    
    MainPage = new MainPage(); //此处修改, MainPage = new PreferencePage();
}
```

**数据库SQLite**

> 首先安装  一个工具 `DB Browser for SQLite`
>
> 然后 还是新建一个内容页,  DataBase.xaml
>
> 同理,要是运行该 数据库内容页,需要手动在App.xaml中修改 
>
> 2. 安装NutGet包 ,在主项目,右键选择 NutGet,添加 SQLite-net-pcl ,不要勾选预发行版本;
>
> 3. 使用SQLite进行 创建数据库, 使用注解 

> 不同的平台,有不同的文件路径,如何保障自己的数据库文件,有效保存和查找呢?
> 抽象出来的文件保存路径 :

**Web连接**

最简单的Token连接

> 1. 学会使用 引用包,将其转化为token;
> 2. json2CSharp 网站,自动生成类



# 第五章 MVVM

下载 mvvmlightlibs 包引用,辅助完成MVVM架构

> MVVM
>
> Mode-View-ViewModel
>
> 模型,视图,视图模型
>
> Model ,就是MVC中的包装数据的 ; 
>
> View 主要指的是XAML(XAML和同分类的cs文件) MVVM效果是 零 C#代码
>
> ViewMode 业务问题,专门给View提供数据

> 在ViewModels文件夹中添加一个 class  `MainPageViewModel.cs`
>
> 默认创建的 class类没有修饰符,手动添加 public



==重点讲述==

MVVM 核心在于 V 和VM

V 要显示数据 和触发功能; 

VM 是 提供数据 和功能  ,属性来显示数据, 方法来触发功能

> 表面上看,ViewModel为View服务,
> 但这套机制是 View要找 ViewModel,View把自己的控件绑定到ViewModel上面;
>
> 特征就是,在ViewModel中不存在任何控件的名称引用

> 常规的属性赋值,事件触发,更多的是 ,控件本身来,主动参与,
>
> 而VM ,更多的是 控件主动将数据的处理权限交给他人,他人处理完,将结果复制过来 
>
> 业务都在在ViewModel中发生,数据和功能都在里面; 完全不知道外面发生了什么
>
> View单方面的调用, ==ViewModel完全不知道View的存在==
>
> ViewModel的本质就是一个带有INotifyPropertyChanged的对象而已

![image-20230221145639107](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230221145639107-2023-2-2114:56:40.png)



就是朋友圈的推送机制;

每当  ViewModel中的属性发生变化,由属性自身决定是否向外推送,自动调用  PropertyChanged 来通知监听该事件的对象

View 仅仅是关注了 这个监听事件而已

------

# 1,2 3 讲述 控件,主要了解 常用控件的 属性设置就可以 

---

略

# 4  Managing Data ,管理数据

这部分 ,对于数据 按照  键值, 数据库, 网络 三种进行处理  

## 键值存储



---

略







## 数据库 

----

```xaml
<!--DataBase.xaml-->
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             mc:Ignorable="d"
             x:Class="Demo.Views.DataBase">
    <ContentPage.Content>
        <Grid>
            <Grid.RowDefinitions>
                <RowDefinition Height="*" />
                <RowDefinition Height="Auto" />
            </Grid.RowDefinitions>

            <ScrollView Grid.Row="0" >
                <ListView x:Name="ResultListView" >
                    <ListView.ItemTemplate>
                        <DataTemplate>
                            <!-- 此时编写 绑定的数据源有蓝色波浪线,去修改-->
                            <TextCell Text="{Binding Id}"
                                      Detail="{Binding ISFavorite}" />
                        </DataTemplate>
                    </ListView.ItemTemplate>
                </ListView>
            </ScrollView>

            <StackLayout    Grid.Row="1">
                <Button Text="creat database"
                        x:Name="CreateDatabaseButton"
                        Clicked="CreateDataBaseButton_OnClicked"/>
                <Button Text="insert data"
                        x:Name="InsertDataButton" 
                        Clicked="InsertDataButton_OnClicked"/>
                <Button Text="read data"
                        x:Name="ReadDataButton" 
                        Clicked="ReadDataButton_OnClicked"/>
                <Label x:Name="pathShow"></Label>

            </StackLayout>
        </Grid>
    </ContentPage.Content>
</ContentPage>
```

```c#
//DataBase.xaml.cs文件
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataBase : ContentPage
    {
        private SQLiteAsyncConnection connection = null; // 类成员变量,因为经常用
        public DataBase()
        {
            InitializeComponent();
        }
        //数据库还是要存放到数据库文件中的,
        private async void CreateDataBaseButton_OnClicked(object sender, EventArgs e
        )
        {
            //这是获取文件夹,但是我是要存文件,PC和Android的文件路径不一样
           pathShow.Text=
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
        [PrimaryKey,AutoIncrement ]  
        public int Id { get; set; }
        public bool IsFavorite{ get; set; }
    }
}
```

## Web网络

----



略

















# 5MVVM 

重点讲解  MVVM 的概念 

同时 在 V  和VM之间如何进行交互,以一个Demo进行阐述 











-----

# 6 MVVM+IService

<iframe src="//player.bilibili.com/player.html?aid=802860623&bvid=BV1Sy4y1s7VE&cid=329310785&page=1" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> 该视频 ,将原来的 DataBase.xaml中 视图 和 逻辑混淆的 ,进行拆分开 
> ==将其变为可循环利用==

[DataBase.xaml原本内容](#数据库)

> **改造内容**
>
> 原本是xaml,和对应的xaml.cs文件实际是一体的, 页面和逻辑混淆在一起

> **步骤**
>
> 1. Favorite类 ==承载数据== 没有业务和功能,只有携带数据的作用 **==>** :pushpin: 存放在数据管理的Model文件夹中  
> 2. 在ViewModels文件夹中 ,新建 DataBasePageViewModel.cs类文件,编写业务逻辑 ,3个功能编写完成  :pushpin: 只负责处理数据 , 连接数据库 还需他人完成
> 3. Services文件夹中 , 新建 `Code`,选择 接口`interface` , 命名 `IFavoriteStorage.cs`
> 4. Services文件夹中, 新建 `FavoriteStorage.cs`文件 实现上述接口
> 5. `DatabasePageViewModel.cs` 调用 `FavoriteStorage.cs`   **步骤4的功能 辅助完成步骤2**
> 6. Views文件夹内,创建内容页`Content Page`   `DataBasePage.xaml`

## 详细补充

**上述步骤 记录补充** 

1. 新建 Favorite类 存放在 Model中 

   * SQLiter-net-pcl nutget包 保证是要有的,引用其中`SQLite` 
   * public 进行修饰 class权限
   * ViewMode是业务逻辑,是做事的,View是供显示使用
   * 这个没有争论的或者 看个人开发习惯;
   * 实例已经实现了 界面和逻辑,是改造,功能明确,就直接从功能入手
   * **可以删除 Database原有的 类了**

2. 新建DataBasePageViewModel 

   * View 和ViewModel的设计上 何为主次的问题

     <iframe src="//player.bilibili.com/player.html?aid=802860623&bvid=BV1Sy4y1s7VE&cid=329310996&page=2" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

   * 首先 将 public 修饰class 

   * 继承 ViewModelBase  ==自动引用GalaSoft.MvvmLight==

   * > 三个最基础的 Button ,创建连接,插入数据,读取数据,==对应三个command==

     > ==ReSharper创建代码模板== 
     >
     > 1. **搜索框搜索 Live template**
     >
     > 2. 选择 `Create Live Template from Selection` 进行创建模板
     >
     > 3. 进入模板编辑界面 `Template <No Name>`
     > 4. 修改变动的部分,以 `$command$` 为例,并设置 快捷短语`shortcut` 和 描述`Description` ,保存即可 
     >
     > 我自己创建没法使用,预计是 自己 `ReSharper`没有配置好的缘故

   * **此时,删除原有的三个RelayCommand方法没有用的,还需要进行编写**

   * > 此时 ViewModel是为View准备数据, 分层是为了 屏蔽 view的数据是怎么来的,
     > 所以 把 数据 剥离到 ViewModel中来, **但是怎么连接数据库呢?**
     >
     > 类有单一职责的原则 ,`DataBasePageViewModel`类是 为View准备数据,**而不是连接数据库**
     >
     > 这里就是涉及到 了 MVVM + IService, 有人连接数据库,将连接数据库 事情 扔到 Services文件夹中

3. 接口 设为  public 

   	<iframe src="//player.bilibili.com/player.html?aid=802860623&bvid=BV1Sy4y1s7VE&cid=329311116&page=3" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

   *  接口只 `规定` 功能, 不关心 如何实现 
   *  规定 连接,插入,读取三个方法声明,导入的是Model文件夹的 Favorite;

4. 实现上述接口

<iframe src="//player.bilibili.com/player.html?aid=802860623&bvid=BV1Sy4y1s7VE&cid=329311335&page=4" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> 该视频 讲解 创建连接方法中的注意点: 实现方法,实现接口与接口的冲突如何解决 ,重构

* 实现接口,`小灯泡` 快速导入
* 在 原本Database.xaml.cs文件中 各种button点击事件中的 ,转移过去

==C#约定,使用await,就必须在类声明表示是`async`,同时该方法 后缀必须是`Async`表明是 Async方法==

> 但是,修改实现的方法是 接口中规定的方法; 有冲突 
>
> 1. 设计接口 ,不考虑实现; 但是设计的接口,不一定能实现 
> 2. async 方法的返回值是Task,返回值是void还不行
>
> ==接口的设计 不符合 实现的规则==
>
> 设计接口 不应该考虑实现,受实现困扰,但 这样的接口设计 ,没有实现的可能
>
> **故, 设计接口 ,一定要考虑 实现,是 不完全考虑,实现的时候,可以修改**
>
> ==是系统成熟,对接口开发,对实现封闭==   **==> 需要重构**

```c#
public async void CreateDatabase(){
...
}
```

> **重构步骤**
>
> 1. 光标移动到 方法名,不选中,右键`Refactor` --> `Change Signature` 
> 2. 修改 方法名为 CreateDatabaseAsync; 返回值为Task(System.Threading.Tasks.Task)

<iframe src="//player.bilibili.com/player.html?aid=802860623&bvid=BV1Sy4y1s7VE&cid=329311537&page=5" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> 修改 插入方法

```c#
public async Task InsertDataAsync(Favorite favorite)
            => await connection.InsertAllAsync((System.Collections.IEnumerable)favorite);
```

> 修改读取
>
> * 还是 await, async 关键字进行修饰
>
> * 重构,先 复制 返回值类型  `IList<Favorite>` ,重构 ,方法名,添加Async, 返回值选择 Task重构版本,复制进入
>
> * 此处需要有返回值, 和插入不同,返回时为空 ,此时添加return 或者 变为表达式体方法 
>
>   ==程序写短,是为了读的更懂==  ==算法才是最优美的==
>
>   ==先成为工程师, 开发者== 最基本的素质 是写的代码让人舒服,才是==xx工程师==



5. 返回 ViewModel

<iframe src="//player.bilibili.com/player.html?aid=802860623&bvid=BV1Sy4y1s7VE&cid=329311672&page=6" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> 视频讲解, 如何 在 ViewModel文件中调用 数据库连接  `直接调用 FavoriteStorage`  ==想办法调用到它==   
>
> **依赖是 接口,而非实现**
>
> * 首先,声明一个 IFavoriteStorage接口,    :pushpin: 智能感知系统, 写变量首字母 就能感知出来  **这个一个接口实例**

将 ViewModel 3个 方法 异常修改为业务逻辑 , 强调 ,单一职责原则 

**Read方法修改**

<iframe src="//player.bilibili.com/player.html?aid=802860623&bvid=BV1Sy4y1s7VE&cid=329311855&page=7" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> 视频  讲述 ,read方法的修改

```c#
  public RelayCommand ReadDatabaseCommand =>
            _readDatabaseCommand ?? (_readDatabaseCommand =
                new RelayCommand(async() => {
                   var  results = await _favoriteStorage.ReadDataAsync();
                       }));

```

> * 将返回值存在 变量 `results`中 ,需要将其变量显示在前端,  使用可绑定属性  显示一条数据 可以使使用可绑定数据
>   但是 ,要显示一组数据 ,需要一个外部的 **主项目**下导入 包 `NuGet` 搜索 `mvvmhelpers` ,找到`Refractored.MvvmHelpers`   作者 `James Montemagno` 在Xamarin项目中层级很高
> * 包中 提供特殊的集合,

 **在类中声明一个 类型是xxx的 属性, 只有 读取功能,初始化时传入Favorite类型的实例**

```C#
public ObservableRangeCollection<Favorite> Favorite {get;} =
	new ObservableRangeCollection<Favorite>();    
```

```c#
public ObservableRangeCollection<Favorite> Favorites  =
            new ObservableRangeCollection<Favorite>();
```

> ==二者不同含义,前者初始化之后,只读取属性值,后者,每次查看该属性都初始化一次==

之后 使用该属性,在读取方法中 ,传入进入

6. 创建 DataBasePage 这个是View

<iframe src="//player.bilibili.com/player.html?aid=802860623&bvid=BV1Sy4y1s7VE&cid=329311995&page=8" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> 该视频讲解创建View 
>
> **步骤**
>
> * 有View ,就要关联 ViewModel  
>
>   * > 就是在ViewModelLocator进行注册
>     > 在 SimpleIoc 注册(构造方法中编写) ,并声明一个相应的属性
>
> * 在DataBasePage.xaml中 绑定 
>
>   * >  标签  `<ContentPage>`中 进行 编写 
>     >
>     > ```html
>     > < BindingContext="{Binding DataBasePageViewModel, Source={StaticResource ViewModelLocator}}" >
>     > ```
>     >
>     > ==花时间 ,将VS 和ReSharper的智能感知调通,是很值得的事情==
>
> * Button按钮 方法 Command到 ViewModel中,  **和原本的比较就是 ,控件事件方法 改为 绑定 ViewModel中的方法**
>
> * ListView中 数据展示,数据源 ItemSource ,绑定 为`Favorite` , 和原本比较就是在 对应的cs文件中 ,手动编写数据源绑定
>
> * 手动测试 ,需要在`App.xaml` 文件中,修改 MainPage的实例对象

  

:warning: 此时 ,视频中运行报错  02:05

<iframe src="//player.bilibili.com/player.html?aid=802860623&bvid=BV1Sy4y1s7VE&cid=329312205&page=9" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> `DataBasePageViewModel`类中 字段 `_FavoriteStorage` 没有赋值
>
> :pushpin: 直接 new ,有很多弊病,最好使用依赖注入
>
> 通过构造函数,进行依赖注入
>
> **既然 必须传入 Favorite,才能构造函数,使用 _favorite, 那谁去给呢?**
>
> ==有SimpleIoc来完成== 
>
> 在 ViewModelLocator构造方法中 ,注册 ,说明谁才是 Favorite
>
> IFavoriteStorage 是需要的,但它是接口,无法实例化,需要说明它的实现
>
> ```
> SimpleIoc.Default.Register<IFavoriteStorage, FavoriteStorage>();
> ```
>
> > **讲解:**
> >
> > 当实例化 DataBasePageViewModel以便调用属性时,发现 需要传入参数 IFavoriteStorage,
> >
> > 就去寻找 ,在注册中发现,该接口的声明是 FavoriteStorage,然后组合 进行依赖注入 ;
> >
> > ==SimpleIoc,基于构造函数进行判断==, 特点,非常明确
> >
> > ==SpringIoC非常隐晦,到处都是@==  但是 ,SimpleIoc 只能做轻量级的小东西
> >
> > 既不要底层手动实现,也不要企业级全自动实现,还是坚持 轻量级半自动化实现;
> > 充分掌控整个依赖注入过程,同时也屏蔽足够的细节

## 总结

<iframe src="//player.bilibili.com/player.html?aid=802860623&bvid=BV1Sy4y1s7VE&cid=329312360&page=10" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> 最后一个视频,讲解MVVM + IServices 之间 如何工作和互动
> ==复杂,但是不难== ==工作量是有,思考量不多== 

![image-20230222142147619](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230222142147619-2023-2-2214:21:48.png)

> ViewModel 执行业务,但是什么活也不干
> 只做 最基本,符合业务本身的事情,其他的就找人去做,委托给接口,但接口没法干活,还是给 实现类
> 所以说,实现类是 ViewModel的工具人
> View主动找,去联系,才能找到ViewModel,
> 但是 ,ViewModel 只发布,办事的实现类,接口都是有ViewLocator进行 维护和寻找,,也是一个工具人



---

![image-20230222143003827](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230222143003827-2023-2-2214:30:05.png)

> 描述 ,View工作,如何去联系 ViewModel 



# 7 Database

---

> **合集概述:**



<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329312439&page=1" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P1 ,46集;
> 创建一个主从模板,也就是我这个项目的 最开始的模板
> "浮出"的菜单是从,主页面的子项目 点击进入也是从



<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329312559&page=2" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P2 47 , 主要介绍  示例数据库的特点
>
> 将DB Brower 启动, --> `Open Database`  项目路径 选择 `poetrydb.sqlite3`文件

![image-20230223151038663](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230223151038663-2023-2-2315:10:40.png)

![image-20230223151054486](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230223151054486-2023-2-2315:10:55.png)

 id从1000开始,且不连续,最后是layout属性

```sqlite
select distinct layout from works
```

查看layout属性就 indent,center两种值,
描述布局方式,有的居中 ,居左

---

<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329312719&page=3" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P3, 48 正式创建数据库
>
> 1. 数据表读到内存,或者说,**创建类对象,将其映射起来**



> 从代码角度看 ,**数据库 就是 Model**

示例中的库是 今日诗词中的库,第三方的,它的数据库就是SQLite

1. 创建类,映射
   * 在Model文件夹中,新建类`class` 类名`Poetry.cs`
   
   * 先public ,
     * 借助 sqlite-net-pcl ,进行映射; 
       * :pushpin:上次做项目, 直接建立映射 ,用`Favorite`类,直接映射 `Favorite`表
       * `CreateTableAsync`函数来建立
       
       ---
       
     * > 类是`Poetry` 表是`works`  二者名称不一致,如何对应
       > ==使用特殊标记 [SQLite.Table("works")]==
       > **在所有的ORM中都这么做**
       >
       > 注释,表明这个是诗词类  _ 写好注释很重要 
     
     * > 按照 表中的属性,来编写类的属性
       > 表中的id是小写, 类中一般都是大写; 
       >  输入 `prop` tab进行输出模板;
       >
       > 因为大小写无法映射, 添加标记 `[SQLite.Column("id")]`
     
     将所有的 属性进行 配置  08:15





<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329312839&page=4" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P4 49 
>
> 处理特殊的属性
>
> * 以Layout属性 讲解 字符串容易出错,且系统无法辅助检查
>   ==如何解决这个问题?==   **最基本的要求就是 尽量不要写字符串**
> * 写死字符串, 写成该类常量   => 脚本语言 编译不给你提示,运行报错才给提示
>   编译器 和 类型 本身就是一种文档





<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329312839&page=4" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>



> 
