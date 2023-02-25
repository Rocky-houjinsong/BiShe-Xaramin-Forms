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

>  P4 67 如何解决 



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
