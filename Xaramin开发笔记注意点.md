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

 ==id从1000开始,且不连续==,最后是layout属性

```sqlite
select distinct layout from works
```

查看layout属性就 indent,center两种值,
描述布局方式,有的居中 ,居左
属性很多,没有全部使用

* 从评论区下载到示例数据库,存放到文件夹`DataBases`中

---

<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329312719&page=3" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P3, 48 正式创建数据库 进行开发 
>
> 1. 数据表读到内存,或者说,**创建类对象,将其映射起来**



> 从代码角度看 ,**数据库 就是 Model**

示例中的库是 今日诗词中的库,第三方的,它的数据库就是SQLite

1. 创建类,进行映射
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
>   编译器 和 类型 本身就是一种文档   **==>** ==永远不要手写代码string,== 使用复制





<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329312839&page=4" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>



>  以列表的形式 查看收藏的诗词,   :warning: 了解到 `领歌` 这个 看板网站, 可以体验体验
> 要显示一个预览,preview ==> 属性没有priview; 要么自己写一个属性,要么在显示的时候生成一个priview 
>
> ==做法:由Model替我们生成一个priview==	

> Priview如何生成?
> 因为 数据库没有 该属性,所以它在 类中,以属性的形式进行计算出来 
> 其次, 需要将该属性给忽略掉 `Ingore` 这样 `Sqlite-net` 就不会处理这个属性了  使用特殊标注进行完成 

<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329313119&page=6" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P6 51 设计 提供 相关的服务接口
>
> :star: 重点在于 设计的动态查询 接口



> **步骤:**
>
> 1. 在`Services` 文件夹中 添加新建项 `Code` ->`Interface` ==> `IPoetryStorage.cs`
>    * 接口 内实现  初始化数据库 ==> 将已有的数据库 重新部署  :warning: 部署操作,是文件,就一定是 Async
>    * 增删改查 操作



1. 初始化数据库

> 不能每一次 启动都初始化, 而是要 判断 ,再决定是否要初始化	Task 进行异步操作
>
> * 动态查询
>
> > 这个不写死,限制查询
> >
> > ==查询就要涉及到条件,那么,条件如何传递==
> >
> > 动态查询条件 ==> Expression,导入的 using System.Linq.Expressions;
> >
> > 表达式本身是一个函数,,接收一个Poetry的参数,返回一个bool类型的值, 
> >
> > 翻页功能,跳过多少,剩下多少 , skip,take; 返回 20~30的结果,跳过skip20 ,返回take10



<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329313369&page=7" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P7 52  去实现该接口; 但主讲代码规范问题

> **步骤:**
>
> 1. 在`Services`文件夹下 添加 `PoetryStorage.cs`类
>    *  继承该接口,点击`灯泡` 自动实现该接口方法 共计4个
>
> ==代码书写规范== 很重要 
>
> 1. 命名规范
>
>    * 怕死口 ,首字母大写,公开的成员,都是 首字母大写
>
>    * 类的 私有成员,都是下划线开头
>
>    * 胎膜儿,函数的成员,前面不加下划线
>
>    * 接口以I开头
>
> 2. 排版规范
>    * kotlin 和微软的规范,自己选一个
>    * 一行语句,只做一件事
>    * 每次缩进一个tab 
>    * ==大型的公司,代码检查,不规范,真的要扣工资==
> 3. 注释
> 4. 功能分区  使用 //*******  还有 regin标记来实现,可以写成模板

<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329313604&page=8" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P8 53 ,实现上述接口 



搞注释, 意义不明确 时候需要写返回值;

NET 实现接口不会自动将注释 copy过来,

规范的代码,能增加写代码的幸福感

从上到下实现

**排序顺序**  服务类的都是公开变量,私有变量 继承方法,公开方法,私有方法 ==按照这个方法去写服务类==

1. **初始化时诗词存储**

> 固定要做的事情
>
> 创建数据库文件,[文件名, 文件路径] 每次都要用,就在类中设为私有变量 
>
> 数据路连接,第一次是 讲数据库连接, 第二次是将MVVM ,连接自己新建的文件,这是第三次
>
> Combine(  GetFolderPath(Environment.SpecialFolder.LocalApplicationData) 确定路径,获取路径,拼接路径

> `const`  和  ` static readlyonly`  这个两个比较 
>
> ==C#语法讲明白的== 一个是 永远不变,一个是可升级的版本号 
>
> 常量 不可计算,是写死的  ; 是常量,又可以计算,那么就使用 `static  readonly`
>
> ==`static` 是属于类的,不再属于成员了==

**编译时常量,运行时常量 ** 那是真常量 ,

<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329313769&page=9" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P9 54  开始执行初始化操作,此时 还在为初始化进行前置编写
>
> 重点阐述 **如何打包资源到项目中**

>  :interrobang:     ==之前是 自己主动创建数据库文件,现在是预设数据库文件,如何部署到客户机器上呢?==\
>
> 按照 ,上面 获取 文件路径的方法,客户机器上面肯定是没有该文件的
>
> 因此 **如何将现有的数据库文件,部署到客户的计算机上**
>
> :key:
>
> * 选中文件,复制
> * 在解决方法的主项目上,点击右键,选择粘贴
> * 导入到项目之后,在该文件,点击属性,弹出属性窗口
> * `Build Action` 选择嵌入式资源 `Embedded resource` 

![image-20230224172330556](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230224172330556-2023-2-2417:23:32.png)

> **验证是否导入成功**
>
> 在解决方案的主项目,也就是被导入的项目,右键,选择 `Edit Project File`  ,在标签  `ItemGroup` 里面就有 描述
>
> ==这个不好调试,或者说 很难发现 ==    :warning:  视频66集 发现 自己没有这个东西,回头翻找

![image-20230226022505273](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230226022505273-2023-2-2602:25:06.png)



<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329314003&page=10" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P10 55  打开链接 操作数据库
> 			打包好的资源,部署到用户的机器上

**文件流** :warning: 用完必须关闭 

> 常见做法就是  dbFileStream.Close();
>
> 微软做法: 使用using(需要关闭的语句操作) ,Java中使用try操作

解读:

把目标文件打开成为文件流,把 来源文件打开, 将目标文件内容拷贝到来源文件中



<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329314003&page=10" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P11 56 



已经拷贝过去,,需要考虑一个问题,版本问题,以后若是升级了,如何确定 用户机器中的当前数据库是哪个版本的呢?

**答案** :key: 存储 ,   由  提供数据库版本号 ,在`IPoetryStorage` `文件`  里面 ,提供版本号 ==不在接口里面,是在文件里面提供==

​		同时 ,将版本信息 存放到 数据库文件也是一种解决方案

> 定义一个 新的公开静态类 `PoetryStorageConstant`  含义  是  与 诗词存储有关的常量,
>
> ==当一个类 被`static`所修饰,就无法 被 `new` 了,就没有实例了==;

> **重点:**

```c#
public const string VersionKey =
            nameof(PoetryStorageConstants) + "." + nameof(Version);
//不是计算得到的,是硬编码的,在编译阶段 变成字符串
```

重新更正接口文件, 返回 部署数据库时, 记录 版本号信息, 在偏好存储中介意 `Preference`存储

以 key文件,version为值,保存版本信息;

:pushpin:  有了版本号`version` 在 是否初始化判定就非常简单 

```c#
Preferences.Get(PoetryStorageConstants.VersionKey, PoetryStorageConstants.DefaultVersion) ==
            PoetryStorageConstants.Version;
```

<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329314506&page=12" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> p12 57

> **步驟：**
>
> 1. 在私有变量 代码段,添加 字段和属性来操作`数据库连接`

```c#
await Connection.Table<Poetry>()
                .FirstOrDefaultAsync(p => p.Id == id);
```

* 打开数据库表`Poetry`
* 根据设定的`条件`进行查询,若无返回 默认 null

> 和使用Springboot 差不多,但是不用 磨磨唧唧在那里写 SQL语句 ,能看到细节,可以学,但不至于 全部都手写

```c#
public Task<IList<Poetry>> GetPoetriesAsync(Expression<Func<Poetry, bool>> where, int skip, int take)
```

将`where`前面自动添加的`@` 进行刪除

------

## 合集总结

* 现代化软件开发都提供了 `资源嵌入`机制 到客户的机器上,并释放出来
* 基于版本号来记录 `版本`的方法 ,记录到`Preference`中
* 读取 数据的方法,里面存入 lambada表达式
  * 返回一组,其他平台 使用翻页工具来制作

---

> :interrobang: **如何判断自己编写的业务逻辑代码是否可以满足需求**
>
> 一般都是借助UI ,启动模拟器 来进行判定是否成功,费时费力,
>
> :key: **使用单元测试**

每写一个类,就把 这个类的单元测试给制作了,确保 每一个类都能顺利通过检查

# 9 Unit Testing Database

---

> 承接第7章,解决如何 进行单元测试问题

<iframe src="//player.bilibili.com/player.html?aid=715269291&bvid=BV1zQ4y1Z79j&cid=329315573&page=1" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> **步骤:**
>
> 1. 新建一个单元测试项目 `solution` 解决方案上面,右键点击添加,选择`New Project`, 搜索 `nunit` 选择 `C#的.NET Core的 NUnit Test Porject`
>    团队开发时,一定要在 windows端新建该项目,Mac有时会出现错误
>    命名`Demo.UnitTest`
> 2. 点击测试`Test` 选择 测试浏览器`Test Explorer` 
>    可能会弹到副屏,   窗口展开,看到里面具体的默认的Test1方法,右键点击运行就可以跑单元测试 `变绿` 就是说明 测试通过
>
> > 单元测试约定 :
> >
> > `待测试的类`所在文件路径 和  在`测试类`中是一样的;
> >
> > `待测的函数` 和`测试函数` 名称一样  `InitializeAsync` ==>`TestInitializeAsync`
> >
> > **步骤**
> >
> > 
> >
> > 即 在`Demo`项目中是`Services`文件下 `PoetryStorage.cs` 
> > 则 在`Demo.UnitTest`项目新建`Services`文件夹,新建`PoetryStorageTest.cs`
> >
> > 2. public 进行修饰 
> > 3. 查询和选择两个函数测试容易,重点在于2个部署函数的测试
> >    ==里面有一系列的连环坑,特别的多==
> >    * 添加 标记[Test] 选择==NUnit.Framework==的Test 
> >    * 在这个函数里面进行测试
> >    * 初始化数据库,说明这个数据库还不存在,需要==断定,这个数据库真的不存在==
> >      * 测试 常用的 ,使用`断言` 路径不存在,文件就不存在
> >      * :interrobang: 路径`PoetryDbPath` 设置为`Private`, 如何在 Test类中访问呢?
> >        :key: 暂时修改为 `public`进行修饰

<iframe src="//player.bilibili.com/player.html?aid=715269291&bvid=BV1zQ4y1Z79j&cid=329315791&page=2" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P2 65集

> **步骤:**
>
> 承接叙述 
>
> 1. 首先在`PoetryStorage.cs`文件中,将 `PoetryDbPath` 属性修改为公开`public`变量
> 2. 断定文件不存在,在`TestInitializeAsync` 方法内编写 
>    * 首先在`Demo.NUnitTest`项目找到 依赖 `dependencies`,选择`添加现有项目``Demo`
>    * 使用断言`Assert`  ,设为 `IsFalse`
> 3. 在 `Test Explorer` 窗口选择测试该方法,`绿色` 通过编译
>    若是将断言改为`IsTrue` 就会出错 

<iframe src="//player.bilibili.com/player.html?aid=715269291&bvid=BV1zQ4y1Z79j&cid=329316128&page=3" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P3 66 ,接着测试上面的方法 

> **步骤:**
>
> 1. await 初始化步骤 判定成功 `IsTure`	
>
>    ```c#
>     Assert.IsFalse(File.Exists(PoetryStorage.PoetryDbPath));
>                var poetryStorage = new PoetryStorage();
>                await poetryStorage.InitializeAsync();
>                Assert.IsTrue(File.Exists(PoetryStorage.PoetryDbPath));
>    ```
>
>    :warning: 此时 测试显示 `空指针异常`错误	  ==非常难找==,跳转异常语句进行查看
>
>    ​           
>
>    ```c#
>                    await dbAssertStream.CopyToAsync(dbFilesStream);// 将目标文件拷贝到来源文件
>    ```
>
>    一般只有 变量空指针,系统变量不会
>    :interrobang: 如何 查看上述2个变量,哪一个是空指针呢?
>
>    :key: 使用 `调试` 断点查看 
>
>    * 断点设置在该行,同时在测试窗口 不再选择`Run` 选择`Debug`
>
>      * 断点没有命中,又出错了 ==> 卡在 `IsFalse`上了!!
>
>      * 那就在 `IsFalse`上面进行 设置`断点`  
>
>      * 此时再次运行 debug,在 路径里面生成的数据库文件是 测试生成的 ,大小是`0k` 里面根本没有数据
>
>        > Xamarin应用程序的 `LocalApplicationData` 路径很深 ,走的是应用程序
>        >
>        > 单元测试的`LocalApplicationData` 路径浅,走的是.NET Core ,二者是不同的平台 ,
>        > ==.NET core是系统一级平台,和系统是同等权重的没有屏蔽和封装;,UWP是二等公民,做了很多屏蔽和分装==
>
>      * 视频演示 在UWP项目下,就把该单元测试生成的数据库文件删除,并移除此处断点, 重新debug,到 双变量语句地方
>
>      * 发现是 `dbAssertStream`是空异常 
>
>      :interrobang: 文件不存在? 但是上面程序能执行,说明是创建的啊 ,如何调试?
>
>      :key:  步骤如下
>
>      > 1. 在`Immediate Windows` 窗口中 ==> `调试` --> `窗口` --> `即时` ,快捷键 `Ctrl+D,I` 
>      >
>      > 2. 输入 `Assembly.GetExecutingAssembly().GetManifestResourceNames()`  查看发现 `Demo.poetydb.sqlite3`
>      >
>      >    * DbName叫做`PoetryDb.sqlite3`
>      >
>      >    * 说明,`.NET`在你不知情的情况下,添加了一个项目名
>      >      ==引入嵌入资源,需要资源名+项目名==  
>      >
>      >      :interrobang: 那如何解决呢?
>      >
>      >      :key: **稍微修改 Demo文件** 
>      >
>      >      > Demo项目本身 ,右键, 选择`编辑项目文件` 
>      >      >
>      >      > 在`EmbeddedResource`标签里面
>      >
>      >    今日编译失败  距离上次 真机编译过去很久, 这次 发现报错 
>      >
>      >    :interrobang:  刚刚 报错 `OpenSilver.MvvmlightLibs` 引用失败,,我寻思这样 会不会和 视频不一样,因为原来的`MvvmLight` 弃用不更新的缘故呢 ,没想到删除 using引用,再重新导入,就好了!!
>      >
>      >    > 接着调试, 在资源文件中修改 如下
>      >    >
>      >    > ```xaml
>      >    > <EmbeddedResource Include="poetrydb.sqlite3">
>      >    > 			<LogicalName>poetrydb.sqlite3</LogicalName>
>      >    > 		</EmbeddedResource>
>      >    > ```
>      >    >
>      >    > :interrobang: 再次进行单元测试 run ,还是报错 ==> 但是在路径中发现, 该文件 终于不是 空文件 
>      >    >
>      >    >  :warning: 说明数据库文件已经正常被部署过来
>
> 2. 

<iframe src="//player.bilibili.com/player.html?aid=715269291&bvid=BV1zQ4y1Z79j&cid=329316507&page=4" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

>  P4 67 如何 剥离 不可测试代码,变为 可以测试 ;



**我的Demo项目zhiyouAndroid平台** 运行报错是这个 

![image-20230226025430688](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230226025430688-2023-2-2602:54:31.png)

![image-20230226025617936](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230226025617936-2023-2-2602:56:18.png)



> 这个 跟着 视频流程走,应该是 Android的问题, 忽略 ;
>
> 回归到视频的问题 :

![image-20230226025811564](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230226025811564-2023-2-2602:58:13.png)

之前在 主项目, ios,android,uwp各个项目下调用都可以使用

能测的才能测,不能测试的 压根就测试不了 `Preferenc` 就无法测试  

:interrobang: 那如何测试? 如何剥离 出去 不能测试的代码呢?

04:31  

> 1. 新建一个接口,把`Preference` 功能复刻进去
>
>    1. interface 里面不用写 public
>    2. 接口的 set 和get方法 规定完成;
>    3. 该接口 和原来的`Preference` 是一模一样的
>
>    > 这样 ,就不再需要`Preference`类方法,而是使用`PreferenceStorage`类方法
>
>    :interrobang: 你需要一个`preferencestorage`这样的类型,如何说明呢?
>
>    :key: 在 构造函数里面说明
>
> 2. 在 `PoetryStorage`中 构造方法声明了`IPreferenceStorage` ,==将上面的 `Preference` 变成`PreferenceStorage`

```c#
    public async Task InitializeAsync()
        { // 打开文件,传递路径 将需要关闭的初始化 扔到using中,文件操作 必须要关闭,using就是该效果
            using (var dbFilesStream = 
                   new FileStream(PoetryDbPath,FileMode.Create))
                // dbAssertStream 数据资源流
            using (var dbAssertStream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream(DbName))
            {
                await dbAssertStream.CopyToAsync(dbFilesStream);// 将目标文件拷贝到来源文件
            }

            Preferences.Set(PoetryStorageConstants.VersionKey, PoetryStorageConstants.Version); 
        }

        /// <summary>
        /// 诗词存储是否已经初始化.
        /// </summary>
        /// <returns></returns>
        public bool IsInitiallized() =>
            Preferences.Get(PoetryStorageConstants.VersionKey, PoetryStorageConstants.DefaultVersion) ==
            PoetryStorageConstants.Version;
```

```c#
public async Task InitializeAsync()
        { // 打开文件,传递路径 将需要关闭的初始化 扔到using中,文件操作 必须要关闭,using就是该效果
            using (var dbFilesStream = 
                   new FileStream(PoetryDbPath,FileMode.Create))
                // dbAssertStream 数据资源流
            using (var dbAssertStream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream(DbName))
            {
                await dbAssertStream.CopyToAsync(dbFilesStream);// 将目标文件拷贝到来源文件
            }
//修改的地方
            _preferenceStorage.Set(PoetryStorageConstants.VersionKey, PoetryStorageConstants.Version); 
        }

        /// <summary>
        /// 诗词存储是否已经初始化.
        /// </summary>
        /// <returns></returns>
        public bool IsInitiallized() =>
            //修改的地方
            _preferenceStorage.Get(PoetryStorageConstants.VersionKey, PoetryStorageConstants.DefaultVersion) ==
            PoetryStorageConstants.Version;
```

<iframe src="//player.bilibili.com/player.html?aid=715269291&bvid=BV1zQ4y1Z79j&cid=329316738&page=5" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P5 68 

```c#
[Test]
        public async Task TestInitializeAsync()
        {
            Assert.IsFalse(File.Exists(PoetryStorage.PoetryDbPath));
            var poetryStorage = new PoetryStorage();  //这里 构造需要传入参数 Ipreferencestorage
            await poetryStorage.InitializeAsync();
            Assert.IsTrue(File.Exists(PoetryStorage.PoetryDbPath));
        }
```

> :warning: `IPreferencestroage`接口没有实现
>
> :interrobang:  如何 声明该接口呢?  
>
> :key: :star:  mock出来, 通过mock工具生成一个mock对象 ,借助这个mock对象来实例化接口
>
> ​		==不借助实现类的实例对象来实现接口==
>
> ```c#
> using Moq;
> var preferenceStorageaMock = new Mock<IPreferenceStorage>();
> var mockPreferenceStorage = preferenceStorageaMock.Object;
> ```
>
> > 再将 生成的sql文件删除,进行测试,就成功了
> >
> > :star: 而我将 其注释,我的也成功了,因为我是在Android平台下测试的 ` Assert.IsFalse(File.Exists(PoetryStorage.PoetryDbPath));
>
> **单元测试 能够自动化重复运行 ,不能每次都手动进行删除文件再进行测试吧 ** :interrobang: 如何解决?
>
> :key:  MOY7NS35PN4PP2IZ 

运行之前和运行之后 都执行一遍 打上2个标签,在任何一个单元测试之前和之后先运行 ;

**上述就是一个完整函数的完整测试周期就完成了**

待测函数  和测试函数 加起来 大概20行

![image-20230226170032863](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230226170032863-2023-2-2617:00:36.png)

![image-20230226170047609](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230226170047609-2023-2-2617:00:48.png)

> 1. 单元测试会有 副作用,第一遍通过,第二遍就不通过,清理副作用 进行自动化完成 
> 2. 不是什么程序 都能测试, 必须把不可测试代码进行剥离 ; ,剥离之后,使用`mock`工具进行代替
> 3. 实际上 和  理论上 还是很有区别的 ;



<iframe src="//player.bilibili.com/player.html?aid=715269291&bvid=BV1zQ4y1Z79j&cid=329316892&page=6" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P6 69  代码覆盖率 , 微软原生开发工具 做不来`代码覆盖率`  **必须依赖第三方工具**
>
> 提升测试 的 覆盖率





<iframe src="//player.bilibili.com/player.html?aid=715269291&bvid=BV1zQ4y1Z79j&cid=329316990&page=7" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> p7 70 验证保存的版本号是否正确
>
> :interrobang:  如何验证 它 是否执行  set函数 以保存 版本号呢? 
>
> :key:  mock工具 ,不仅可以凭空产生对象还可以验证你是否调用了该对象

```c#
  preferenceStorageaMock.Verify(
                p => p.Set(PoetryStorageConstants.VersionKey,
                    PoetryStorageConstants.Version), Times.Once);
```

> Mock工具确认, 是否调用set函数,传入指定参数,而且只执行了一次 

<iframe src="//player.bilibili.com/player.html?aid=715269291&bvid=BV1zQ4y1Z79j&cid=329317184&page=8" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

>P8 71 测试 `IsInitiallized`函数 



待测试的函数 就是 调用get方法比较版本信息, 就需要 给一个`preferencestorage`
Mock工具产生对象,也能mock函数,

```c#
/// <summary>
        /// 测试诗词存储是否已经初始化.
        /// </summary>
        public void TestIsInitialized()
        {
            var preferenceStorageMock = new Mock<IPreferenceStorage>();
            preferenceStorageMock.Setup(p =>
                    p.Get(PoetryStorageConstants.VersionKey, PoetryStorageConstants.DefaultVersion))
                .Returns(PoetryStorageConstants.Version);
            var mockPreferenceStorage = preferenceStorageMock.Object;
            var poetryStorage = new PoetryStorage(mockPreferenceStorage);
            Assert.IsTrue(poetryStorage.IsInitiallized());
        }
```

> 1. 建立对象mock
> 2. 设置对象mock的setup方法,调用里面的 方法,就返回值
> 3. 建立mock对象
> 4. 断言测试

<iframe src="//player.bilibili.com/player.html?aid=715269291&bvid=BV1zQ4y1Z79j&cid=329317511&page=9" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P9 72 测试查询

要测试查询,就得调用 `Initialized` 连接数据库 来迁移数据库,还得给构造函数提供参数 
==> 后面 测试查询一组 也得这样操作 ,那么 需要重复使用的代码,需要独立出来 ;

**步骤:**

1. 在Nunit项目新建文件夹`Helpers` 创建一个新的类`PoetryStroageHelper`
2. 在类中 :获得一个已经调用 连接数据库函数的诗词存储

> 将删除数据库等操作 ,也集中到 帮助类中进来 , 单一职责原则

<iframe src="//player.bilibili.com/player.html?aid=715269291&bvid=BV1zQ4y1Z79j&cid=329317784&page=10" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P10 73 



```c#
/// <summary>
        /// 测试获取一个诗词
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestGetPoetryAsync()
        {
            var poetryStorage =
                await PoetryStorageHelper.GetInitializedPoetryStorageAsync();
            var poetry = await poetryStorage.GetPoetryAsync(10001);
            Assert.AreEqual("临江仙 · 夜归临皋", poetry.Name);
        }
```

:warning: 报错 

> 消息: 
> TearDown : System.IO.IOException : The process cannot access the file 'C:\Users\hp\AppData\Local\poetrydb.sqlite3' because it is being used by another process.
>
>   堆栈跟踪: 
> --TearDown
> FileSystem.DeleteFile(String fullPath)
> File.Delete(String path)
> PoetryStorageHelper.RemoveDatabaseFile() 行 31
> PoetryStorageTest.RemoveDatabaseFile() 行 25
>
> **此时只有Windows电脑报错**  ==> 数据库 当前处于 被打开状态 ;  ==当文件被打开状态,无法删除== 
>
> ==存粹的工程技巧 和工程 知识==
>
> :interrobang: 如何解决呢? 
>
> :key: 提供一个函数进行解决
>
> > 定义在哪里?
> >
> > 不是业务层面上的功能 ,是测试上面的功能  ,业务层面 ,需要在接口 文件中书写; 测试 ,面向的是`PoetryStorage`类 

```c#
// <summary>
        /// 关闭诗词数据库.
        /// </summary>
        /// <returns></returns>
        public async Task CloseAsync() => await Connection.CloseAsync();
```



```c#
 await poetryStorage.CloseAsync();
```

<iframe src="//player.bilibili.com/player.html?aid=715269291&bvid=BV1zQ4y1Z79j&cid=329318070&page=11" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P11 74  测试查询一组诗词数据

**步骤:**

> 1. 在诗词帮助类中, 添加常量字段 ,记录诗词数量
> 2. 测试函数编写

```c#
/// <summary>
        /// 测试获取满足给定条件的诗词集合.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestGetPoetriesAsync()
        {
            var poetryStorage =
                await PoetryStorageHelper.GetInitializedPoetryStorageAsync();
            var where = Expression.Lambda<Func<Poetry, bool>>(
                Expression.Constant(true),
                Expression.Parameter(typeof(Poetry), "p"));
            var poetries =
                await poetryStorage.GetPoetriesAsync(where, 0, int.MaxValue);
            Assert.AreEqual(PoetryStorageHelper.NumberPoetry, poetries.Count);
            await poetryStorage.CloseAsync();
        }
```

> 最后,视频 测试所有的 代码覆盖率,是100% ==> 以后代码出错,绝对是 新写的代码问题,已经测试的是没有问题 
> 测试 ,有几种测试可能, 可能覆盖不全, 
> 单元测试 墨迹,花时间,但是 以后再也不需要考虑,直接拿来用就可以



# 10 View and ViewModel

<iframe src="//player.bilibili.com/player.html?aid=757837987&bvid=BV1i64y1U7gY&cid=329318198&page=1" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P1 75集 使用设计器,(作用不大)

**步骤:**

> 1. 在主项目Demo中, `Views`文件夹,添加新的内容页`Content Page` 命名为`Result.xaml`
> 2. 添加基础的 ListView

```xaml
<ListView ItemsSource="{Binding PoetryCollection}">
            <ListView.ItemsSource>
                <x:Array Type="{x:Type x:String}">
                    <x:String>Item 1</x:String>
                    <x:String>Item 2</x:String>
                </x:Array>
            </ListView.ItemsSource>
            <ListView.ItemTemplate>
                <DataTemplate>
                    <TextCell Text="{Binding Name}"
                              Detail="{Binding Snippet}" />
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>
```

**知识点**

> 1.  <ContentPage.Content> 默认自带的,可以删除 不写会默认添加
> 2. 预览效果,不一定很准

![image-20230226231147855](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230226231147855-2023-2-2623:11:48.png)

<iframe src="//player.bilibili.com/player.html?aid=757837987&bvid=BV1i64y1U7gY&cid=329318665&page=3" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P3 77 无限滚动效果

```
<ListView.Footer> 用来做注脚
```

==所有UI类做单元测试,非常困难==

<iframe src="//player.bilibili.com/player.html?aid=757837987&bvid=BV1i64y1U7gY&cid=329318665&page=3" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P4 78

1. 安装Mvvmlightlibs
2. 新建ViewModel  `ResultPageViewModel`

**知识点:**

> 1. ViewModel可以被单元测试,需要将方法或者属性进行公开

<iframe src="//player.bilibili.com/player.html?aid=757837987&bvid=BV1i64y1U7gY&cid=329319017&page=5" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P5 79 显示结果 Xamarin 并不支持 无穷滚动,但是 无穷滚动是国际标准,第三方支持;,是一个扩展功能

![image-20230226233452334](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230226233452334-2023-2-2623:34:52.png)

> 1. 安装上述 预览版的 包, 支持 无线滚动功能
> 2. Services 中代码块分布  和 ViewModel不一样 
>    * 构造函数,绑定属性 ,绑定命令,其他
> 3. 在构造函数初始化

**知识点:**

> 1. 舒服的开发顺序,先做 `Services` 再做`ViewModels` 最后再做 `View`

<iframe src="//player.bilibili.com/player.html?aid=757837987&bvid=BV1i64y1U7gY&cid=329319229&page=6" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P6 80	

```c#
public string Status
        {
            get => _status;
            set => Set(nameof(Status), ref _status, value);
        }
        private string _status;
```

> 1. 私有 字段,
> 2. 公有属性, get读取字段,set设置字段,调用Set方法,修改字段值,并触发statechange事件,将属性变化传播出去

有了上述的属性,就可以设置属性

<iframe src="//player.bilibili.com/player.html?aid=757837987&bvid=BV1i64y1U7gY&cid=329319567&page=7" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P7 81集 



* 搜索结果页,是显式查询结果,就需要调用查询方法,但是查询条件一定是 别人给的,不是自己生成的 ;
  :interrobang: 那如何解决?  他人 如何跨页 传参数 ;
  
  :key:  1. `Where参数 通过绑定属性进行传值
  
* `OnCanLoadMore` 和 `OnLoadMore`  这两个 是要有的,在文档中最小的例子都有 `OnLoadMore` 是用来加载数据的

* 声明Where属性, 重新填写 查询方法的参数 ,三个;

:interrobang: 判断  返回终止条件  : 诗词返回条数 小于 `PageSize`  此时需要 让`OnCanLoadMore` 设为 False

:key: 通过私有变量  _canLoadMore, 默认值是false

:interrobang:  谁 来将它 设置为true呢?
:key: Where属性来设置  ,<font color = Red size = 4> where属性变化,说明查询条件变化</font> ,此时就需要 将私有字段 _canLoadMore 设置为 true

<iframe src="//player.bilibili.com/player.html?aid=757837987&bvid=BV1i64y1U7gY&cid=329319677&page=8" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P8  82 



:interrobang: 什么情况下显示 没有任何结果呢? 

一开始没有,并且查也没有查到 

> 理清 该方法内容的思路:
>
> 1. 先取Page条结果,
>    * 如果取回的不足1页,,说明数据库没有更多的结果了
>    * 一条结果都没有,同时 一开始都没有任何结果



<iframe src="//player.bilibili.com/player.html?aid=757837987&bvid=BV1i64y1U7gY&cid=329320023&page=9" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P9 83 上述 已完成 加载数据的要求;  每次新查询,应该把 就查询给清空;在哪里操作?
>
> 触发新查询 在哪里操作 ,如何触发 重新载入  

==搜索页 和 收藏页==差不多

点击查询,立刻跳转 ,加载查询之后的结果,搜索页准备一个查询条件,传给搜索结果页;

再次搜索,先把原来的搜索结果删除,再把新的加载进来

> 在MVVM中,任何东西 都需要 `Command` 来执行  ==> 绑定命令,前面只有绑定属性

> 页面显示命令 `PageAppearingCommand`就是 页面显示时执行的命令  
>
> 新查询才执行

执行初始化操作之前, 将 newQuery 设置为false ,保证初始化操作 只执行1次,下一次执行 `PageAppearingCommand` 时

:interrobang:  不理解 但是 之后开发好好体会吧 

<iframe src="//player.bilibili.com/player.html?aid=757837987&bvid=BV1i64y1U7gY&cid=329320435&page=10" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>



> P10  84 进行单元测试 

> 1. 新建文件夹, 
> 2. 测试类



+= 空格 被挡住,没有弹出事件列表, 按esc ,再按空格

此处进行 单元测试 ,



<iframe src="//player.bilibili.com/player.html?aid=757837987&bvid=BV1i64y1U7gY&cid=329320702&page=11" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P10  85 

> 加载一次 就会返回20条结果

==Command()底层就是调用Execute函数== Execute必须传参数,若是没有参数,传`null`就可以

:interrobang:  此时  运行报错 ,预期20 结果0 ; 走调试, 报出 无法删除数据库的错误 ;

:warning: 我今天也是遇到这个问题,   运行和调试 出现的问题 不一致 ;

```c#
resultPageViewModel.PageAppearingCommand.Execute(null); // Execute 异步多线程执行
```

<font color = red size = 5> Execute函数执行 会自动开启一个新的线程 </font>

`PageAppearingCommand` 会在一个新的线程中 执行

<font color = blue > 此处 讲解为何 调试失败, 运行失败的推理</font>   详细阐述看

> ```c#
>    Assert.AreEqual(0, resultPageViewModel.PoetryCollection.Count);
>             resultPageViewModel.PageAppearingCommand.Execute(null);
>             Assert.AreEqual(20, resultPageViewModel.PoetryCollection.Count);
> ```
>
> 上述 三步 是单元测试 内执行的语句 , 单元测试是单线程执行的;
>
> 但是 第二语句  是 多线程执行 `没有等待第二句执行完` 就直接执行第三句
>
> 可第三句需要 第二句执行结果来做判定 ,因此 冲突 
>
> `Command `不可被测试 

<iframe src="//player.bilibili.com/player.html?aid=757837987&bvid=BV1i64y1U7gY&cid=329320877&page=12" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P11 86; 曲线救国 , ,之前不可被测试就剥离出去,以自定义的形式 进行测试 
>
> 现在 : **想测试的,不能被剥离 ,**  :interrobang:  如何解决?

:key:  将`Command`里面的内容变为一个函数  ==> 将不可测试的 进行拆分 成 待测函数和外壳

因此  测试`PageAppearingCommandFunction` 就可以了

==单元测试  不是万能的 ,有些只能人工审核== 



测试通过 ,验证数量, 验证内容 自己来完成 ;

还需要验证 `PageAppearingCommand`在Where没有发生变化,也就是where属性没有重新赋值的时候是不会触发 第二次加载的

resharper快捷键 `Shift + Ctrl + 空格`  触发智能提示;

测试 上述问题, 关注 collection是否改变的事件就可以 

<iframe src="//player.bilibili.com/player.html?aid=757837987&bvid=BV1i64y1U7gY&cid=329321381&page=14" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>



> P12 87  View  和 ViewModel连接起来 

**步骤:**

> 1. 使用`ViewModelLocator` 进行连接  ,构造器和 实例的构造 
>
> 2. 在 `App.xaml` 中 命名控件中 注册
>
>    ```xaml
>    xmlns:vm="clr-namespace:Demo.ViewModels;assembly=Demo" 
>    ```
>
>    并 填写 
>
>    ```c#
>    <vm:ViewModelLocator x:Key="ViewModelLocator"/>
>    ```
>
> 3. 在`ResultPage.xaml` 指定 `BindingContext` 
>
>    ```xaml
>     BindingContext="{Binding ResultPageViewModel, Source={StaticResource ViewModelLocator}}">
>    ```



<font color = red size = 5> 页面执行,调用`PageAppearingCommand`  </font> :interrobang:  ? ,听起来是这个词组

之前绑定都是用  button 的 command进行绑定

`ContentPage` 里面是没有 `Command` 的 ,但是有 `Appearing事件`  去 触发 . 有些 不优雅 

**步骤:**

> 1. 在 `ContextPage` 空间中填写并关联 `Appearing = "ResultPage_OnAppearing"
>
> 2. 重写 该事件
>
>    ```c#
>    private void ResultPage_OnAppearing(object sender, EventArgs e)
>    {
>        ((ResultPageViewModel)BindingContext).PageAppearingCommand.Execute(null);
>    } // 强制类型转换, 调用 该方法 
>                   
>    ```

:star: 问题如下

* 丑 ,代码有些难理解 
*  MVVM 模式要求, 不在`xaml`的 `xaml.cs文件中写代码 ,破坏该模式

:interrobang:  **如何触发`PageAppearingCommand`**方法而不调用该事件呢?

:key: :heavy_check_mark: 使用第三方机制  ==>第三方控件

==安装`Behavior.Forms`== ,作者是 `David Britch` 

利用该控件将 事件 ,关联

**步骤:**

1. `ContentPage` 导入

   ```xaml
   xmlns="http://xamarin.com/schemas/2014/forms" 
                xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                x:Class="Demo.Views.ResultPage"
   <!-- 新版本 -->
                xmlns:d ="http://xamarin.com/schemas/2014/forms/design"
                xmlns:mc ="http://schemas.openxmlformats.org/markup-compatibility/2006"
                mc:Ignorable ="d"
   <!-- 原有xaml设计器 -->
                xmlns:b ="clr-namespace:Behaviors;assembly=Behaviors"
   <!-- 导入B.F -->
                BindingContext="{Binding ResultPageViewModel, Source={StaticResource ViewModelLocator}}">
   <!-- 绑定ViewModel -->
   ```

2. 添加标签组

   ```xaml
       <ContentPage.Behaviors>
           <b:EventHandlerBehavior EventName="Appearing">
               <b:ActionCollection>
                   <b:InvokeCommandAction Command="{Binding PageAppearingCommand}"/>
               </b:ActionCollection>
           </b:EventHandlerBehavior>
       </ContentPage.Behaviors>
   ```

3. 在app,appshell中 注册;

4.  :warning: 没有 注册 `IPreferenceStorage` 

<iframe src="//player.bilibili.com/player.html?aid=757837987&bvid=BV1i64y1U7gY&cid=329322205&page=16" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P13 88 ,注册 `IPreferenceStorage` 

**步骤:**

1. 该类没有实现, 在`Services` 文件夹下新建一个类,命名 `PreferenceStorage` 	实现接口方法
2. 在Locator中注册

:interrobang: 测试 无法执行,打断点 看是否执行到`ResultPageViewModel里面 发现  `   `PageAppearingCommandFunctio` 中 `_isNewQuery` 是false

==一开始启动没有人 给它传 where条件, 当然不好使了==

添加测试使用的where条件

```c#
//TODO 供演示使用
            Where = Expression.Lambda<Func<Poetry, bool>>(
                Expression.Constant(true), Expression.Parameter(typeof(Poetry), "p"));
```

:warning:  非但没执行,反而崩溃了  ,调试发现不存在这张表;

单元测试 调用 迁移数据库,通过 `poetrystorage`	 进行迁移,调用其初始化数据库函数方法 

==构造函数不能是 async== 

**步骤:**

只能将 poetryStorage 变成一个本地成员变量在其他地方进行 调用初始化操作 



此时成功完成 

----

<iframe src="//player.bilibili.com/player.html?aid=757837987&bvid=BV1i64y1U7gY&cid=329322672&page=17" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P17 91  ,上面完成  成功完成数据的加载和显示 

:heavy_multiplication_x: 此时还不能无限滚动,还得设置 

之前注脚 完成,现在设置 无限滚动 

`ResultPage.xaml` 名称空间中,输入  下面语句, 注意 `Xamarin` 选择 ==Xamarin.Forms.Extended.InfiniteScrolling== 

```c#
xmlns:scroll ="clr-namespace:Xamarin.Forms.Extended;assembly=Xamarin.Forms.Extended.InfiniteScrolling"
```

扩充listView功能

```c#
  <ListView.Behaviors>
                <scroll:InfiniteScrollBehavior/>
            </ListView.Behaviors>
```

==扩展控件功能的就是 Behavior机制==

此时 , 实机运行成功, 单元测试不可以, 在`视图` 有`任务列表` 显示 所有的TODO `Ctrl + W,T` 

# 11 Navigation



<iframe src="//player.bilibili.com/player.html?aid=802862128&bvid=BV1Dy4y1s7RR&cid=329322938&page=1" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P1 92 ,上面搞定的是 诗词数据库的加载,现在是 诗词详情页的展开

> 诗词详情页和诗词搜索页相互关联,从结果页可以跳转到 详情页

**步骤**

> 1. `Views`文件夹新建内容页`DetailPage`
> 2. 添加导航服务接口不止一个  在`Services`文件夹中新建接口 `IContentNavigationService`

==通用做法基于页面的名字进行导航==

* 页面导航 需要传递实例 ,告诉他,导航到谁,传递的是Page实例页面实例

  * 这样造成类型依赖 ==> `Services`的层级非常高的

  * `services` 是绝不可以 依赖`View` 的 ;

    ==>剥离 出来  基于 字符串形成依赖,拒绝基于 类型形成依赖

![image-20230228233423849](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230228233423849-2023-2-2823:34:25.png)

<iframe src="//player.bilibili.com/player.html?aid=802862128&bvid=BV1Dy4y1s7RR&cid=329323125&page=2" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P2 93 讨论导航问题

导航有两种,,一种 浮出`Master-Detail` ,一种 页面里面`Tabbed`, 二者原理不一样, 

本视频主要讲解页面内的导航 

* 浮出 => 从 `浏览页` 切换到`关于页` 没有动画的 ,但往回且有动画,由listView带来的
  * 而 在浏览页  自带的 项目点击进入这个跳转 也有动画  
* 标签页切换 

二者的机制不同

本视频主要讲述 ,从页面内 ,跳转到详情页

<iframe src="//player.bilibili.com/player.html?aid=802862128&bvid=BV1Dy4y1s7RR&cid=329323448&page=3" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P3 94 

在 `Views`文件夹中的自带的`ItemsPage`文件中有 ==OnItemSelected== , 触发它,就能进入其他页面

查看 该方法 就知道  `new` 了一个Page

`Navagation` 是  `ItemPage`的一个属性 

<font color = red size = 5> 导航就在View层做,利用该属性</font>

根导航, 之后再讲



但我就想 将 导航操作就放在 Service中做 

View中的操作, 是在ViewModel中执行的, ViewModel 还得通过 Service执行;

但Service 又做不了导航,导航只能在 View中操作 

:interrobang:  如何 在Services中将view层的事情给做了

<iframe src="//player.bilibili.com/player.html?aid=802862128&bvid=BV1Dy4y1s7RR&cid=329324033&page=4" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P4 95 绕很大的圈 

> 新建类实现上述接口 `ContentNavigationService`

导航,归根结底是由View进行,要找到它 ;

在Xamarin中,找到当前显示的view 有一套方法, 要导航的东西一定是你当前显示的页面,
==获得当前正在显示的页面

**例如**

你看到是`MainPage`  关于,浏览两个页面 都是在`MainPage`中显示

==官方文档讲 覆盖一层上去,返回就是抽去上面一层==

`MainPage`是一个 `MasterDetailPage` ,是 官方提供的模板

<font color =red size =4> `App.xaml`中的MainPage是APP的属性, 由MainPage()赋值</font>

<iframe src="//player.bilibili.com/player.html?aid=802862128&bvid=BV1Dy4y1s7RR&cid=329324578&page=5" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P5 96   



两个模板中都有 `NavigationPage这个标签, 这个 是 模板能进行跳转的基础 
 方法走的是 ==Navigation.PushModalAsync== 进行跳转页面操作

`MasterDetailPage` 之所以能导航,是因为里面有  `Navigation` 这个标签;

```c#
//App.xaml 
MainPage = new navigationPage(new ItemPage());  // 这样才能导航
```



新建一个 `TestPage`,添加 按钮,及其方法 ,

```
private async Task Button_OnClicked(object sender, EventArgs e)
        {
            var cns = new ContentNavigationService();
            await cns.NavigateToAsync("");
        }
```

在 `MainPage.xaml`中的 `MasterDetailPage.Detail`中  修改  `<views:Page />`

<iframe src="//player.bilibili.com/player.html?aid=802862128&bvid=BV1Dy4y1s7RR&cid=329324755&page=6" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> P6 97 

![image-20230301152358290](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230301152358290-2023-3-115:23:59.png)

 MVVM + Service

![image-20230301152557249](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230301152557249-2023-3-115:25:58.png)

<iframe src="//player.bilibili.com/player.html?aid=802862128&bvid=BV1Dy4y1s7RR&cid=329325147&page=8" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>



> P8 99  
>
> 示例中 ,每次点击创建一个新的实例,造成内存泄漏
>
> 每个页面 只创建一份,那势必要缓存,  该如何解决? :interrobang: 

==将`new` Page的操作 给剥离出来==

在内存中 缓存一组对象最常用的方法就是使用字典

其他语言使用 Map ,C#语言使用

在缓存中 读取,读到就使用,没有 就新建

方法编写完成 , `ContentNavigation`(进行导航)依赖`ContentPageActivationService` (new页面)

以 私有变量的形式进行依赖



<iframe src="//player.bilibili.com/player.html?aid=802862128&bvid=BV1Dy4y1s7RR&cid=329326135&page=10" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> 10 101
>
> 公开方法获取私有变量	

> P11 102 和Spring IoC 非常像

上面的扩展性很差 , 

1. 需要在`IContentNavigationService`里面 添加一个 待加载页面的 字符常量

   ```
    /// <summary>
       /// 内容导航常量.
       /// </summary>
       public static class ContentNavigationConstants
       {
           /// <summary>
           /// 诗词详情页
           /// </summary>
           public const string DetailPage = nameof(Views.DetailPage);
       }
   ```

   

2. 在 `ContentPageActivationService`中 添加一个 if语句进行判断 是否是该字符常量

   ```c#
      public ContentPage Activate(string pageKey)
           {
               //完成页面的缓存  +  激活.
               if (cache.ContainsKey(pageKey))
               {
                   return cache[pageKey];
               }
   
               if (pageKey == ContentNavigationConstants.DetailPage)
               {
                   cache[pageKey] = new Views.DetailPage();
               }
   
               return cache[pageKey];
           }
   ```

<font color = red size = 5> 使用反射机制</font>  缓存机制;



根据类型 创建对象;

获得类型,:interrobang: 如何获得?

:key: 

准备一个类型字典 ;

```c#
/// <summary>
        /// 页面键 - 页面类型字典
        /// </summary>
        public static readonly Dictionary<string, Type> PageKeyTypeDictionary =
            new Dictionary<string, Type>{ { DetailPage, typeof(Views.DetailPage)  } };
```

根据类型 ,创建实例

```c#
  cache[pageKey]=
            (ContentPage) Activator.CreateInstance(ContentNavigationConstants.PageKeyTypeDictionary[pageKey]);
```

> 103

```C# 
public ContentPage Activate(string pageKey)
        {
            //完成页面的缓存  +  激活.
            if (cache.ContainsKey(pageKey))
            {
                return cache[pageKey];
            }

            /*if (pageKey == ContentNavigationConstants.DetailPage)
            {
                cache[pageKey] = new Views.DetailPage();
            }*/
            cache[pageKey]=
            (ContentPage) Activator.CreateInstance(ContentNavigationConstants.PageKeyTypeDictionary[pageKey]);
            return cache[pageKey];
        }
```

使用三元表达式 将 上述进行转换;

**赋值具有传递性 ,**返回 赋值给第一个

**总结:**

先编写 `IContentNavigationService`,有 `NavigateToAsync`函数 ,实现 传递页面键进行跳转;

编写 上述实现类 ,

大问题 :interrobang: 不能传参数 
