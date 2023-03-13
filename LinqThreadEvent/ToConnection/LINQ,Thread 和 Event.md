## P1:viewmodel

---

显示所有收藏的 ViewModel

* 所有收藏列出来
* 点击一首收藏的时候 ,,该诗词显示出来;

新建 viewmodel ,`FavoritePageViewModel`

涉及到 两个服务`Service` ,收藏 和导航 

收藏数据  和 诗词数据 是 独立;

 只知道收藏该诗词,但不知道诗词的内容,显示诗词内容就需要 ;

所以 还需要第三个服务,诗词服务;



view在眼前摆着, 不知道 如何 添加和使用服务才能实现view的功能;

问题  和问题的实现 ,没有建立起来;  

缺乏工程思维; 做的太少了;

---

显示所有的收藏;

要显示的是 诗词,不是收藏;

favorite里面啥也没有,;UI中 是诗词;

---

引入 `NuGet包` MvvmHelps  `Refractored.MvvmHelpers 1.6.2`



**无限滚动的初始化**

* 在构造函数里面做
* 数据添加在回调函数里面进行

```
public ResultPageViewModel(IPoetryStorage poetryStorage, IContentNavigationService contentNavigationService,
            IFavoriteStorage favoriteStorage)
        {
            //TODO 供演示使用的诗词存储,未来应该删除.
            _poetryStorage = poetryStorage;
            //这个不需要删除
            _contentNavigationService = contentNavigationService;
            //TODO 供演示使用的收藏存储, 未来应该删除
            _favoriteStorage = favoriteStorage;


            PoetryCollection = new InfiniteScrollCollection<Poetry>
            {
                OnCanLoadMore = () => _canLoadMore,
                OnLoadMore = async () =>
                {
                    Status = Loading;
                    var poetries = await poetryStorage.GetPoetriesAsync(Where, PoetryCollection.Count, PageSize);
                    Status = "";
                    if (poetries.Count < PageSize)
                    {
                        _canLoadMore = false;
                        Status = NoMoreResult;
                    }

                    if (poetries.Count == 0 && PoetryCollection.Count == 0)
                    {
                        Status = NoResult;
                    }

                    return poetries;
                }
            };
        }
```

**ObservableRangeCollection**

* 看起来不错,底层有Bug
  搭配的控件有bug, 在实现 该接口 有问题;

  和特定控件搭配有问题,需要注意;

* 不在 构造函数中使用; 初始化额外进行

* 集合没有 无限滚动功能,需要手动实现;

----

q!lhHf5jpUXS7nn

## P2 ling查询

**收藏读取出来,转化成诗词;**

* 传统版本的 写法;

```c#
var favoriteList = await _favoriteStorage.GetFavoritesAsync();
            var poetryList = new List<Poetry>();
            foreach (var favorite in favoriteList)
            {
                await _poetryStorage.GetPoetryAsync(favorite.PoetryId);
            }
            PoetryCollection.AddRange(poetryList);
```

* 高级写法,使用LINQ去写;

  > await 关键字 和 async的原理:
  >
  > Task 是 一个待执行的 任务, 是没有执行的任务, 返回值是Poetry, 需要 await; 就是执行的意思
  >
  > 加上这个关键字,task才会真正执行并取出结果	

  ![image-20230313233020543](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230313233020543-2023-3-1323:30:22.png)

![image-20230313233429488](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230313233429488-2023-3-1323:34:30.png)

![image-20230313233447378](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230313233447378-2023-3-1323:34:48.png)

![image-20230313234142318](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230313234142318-2023-3-1323:42:22.png)

![image-20230313234208498](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230313234208498-2023-3-1323:43:22.png)

> 问题: select这个task 全部执行完的结果 再转为 列表,前一个Task执行完才能作为下一个 task的输入;
>
> 现在 得到是一组 task的 poetry,而我要是是 list 的poetry;
>
> 将所有的task的poetry都执行
>
> **Task的WhenAll函数就是用来执行一组Task任务的,返回值是 执行结果**



## P3 新頁面 FavoritePage

---

数据绑定 ,不然程序不好书写;

1. 现在 `ViewModelLocator` 中注册; 并包装成属性
2. 名称控件 导入 locator 和behavior;
3. 导入 b组件调用pageappearingcommand方法
4. listview控件绑定 属性

---

* 在`首页`中注册  `mainPage`  ,将启动页换为 `FavitePage` ,根导航还没有制作,只能手动切换

## P3导航 到详情页;

---

command 接受点击事件处理函数,

从收藏页导航到诗词详情页; 

从`ResultPageViewModel中` 粘贴  点击命令;

view层 ,绑定点击事件处理函数; ,传递command	



view,直接照着 赋值粘贴就可以;

此时运行 发现出问题:

> 每次 PageAppearingCommand的时候, 都要加载一次数据 ;
>
> :key: 1. 每次 都清空;  2. PageAppearingCommand 只执行一次;

第一种清空

* 好处: 最新的数据
* 返回上级界面,也是最新的数据;
* 坏处: 加载时间长,用户体验一般

```c#
public async Task PageAppearingCommandFunction()
        {
            // PoetryCollection.Clear(); //每次导航前进行清空
```

第二种  不刷新,但是 数据不同步;

问题是 5秒内 切换到 该页面,假设能切换出3次该页面,那3次页面的数据同时加载进来; 一般这个速度是很短的;,但也是个bug

```c#
 if (_isLoaded)
            {
                return;
            }
 await Task.Delay(5000); // 5秒延时查看变化状态
            _isLoaded = true;

```

第三种: 使用线程锁 来解决 

线程二次加锁;

多个线程 去读取写入 `_isLoaded` 的值

线程锁原理展示 但是 ,若是在时限内,两个线程同时操作,就都不会执行;	

```
 // var canRun = false;  //线程锁原理展示 
            // if (!_isLoaded) canRun = true;
            // if (!canRun) return;
            // _isLoaded = true;
```

## P6 二次加锁法;

 读,锁 读 改;跳出 



```c#
    var canRun = false;

            if (!_isLoaded)
            {
                lock (_isLoadedLock)
                {
                    if (!_isLoaded)
                    {
                        _isLoaded = true;
                        canRun = true;
                    }
                }
            }

            if (!canRun)
            {
                return;
            }
```

把`isLoaded` 锁上,别人就一定能读到嘛?

别人下次读到的就一定是true了嘛?

此时也会出问题

cpu 和编译器 乱序执行; 多线程等原因;

如何保证呢? 保证每个线程 都能读到这唯一 一个变量的真正值呢?

内存中就这一个变量,但以上原因 导致 读取不一致,

添加一个关键字;,使用 volatile 进行 禁用 一切优化; 让所有线程 都读同一个 isloaded的值;

所哟编译平台下都有

服务器端 多出现该问题;

## P7  同步数据



此时 ,返回,还是在 收藏界面,没哟被删除

A页面修改数据, B页面怎么知道;  跨页面通信 

* 简单的数据 ,使用数据绑定解决;  A,B两个页面绑定到同一个对象上,

![image-20230314014123498](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230314014123498-2023-3-1401:41:24.png)

> 跨页面同步数据; 
>
> 很多中方法,  
>
> 最基本的思路;

![image-20230314014639407](https://gitee.com/songhoujin/pictures-to-typora-by-utools/raw/master/image-20230314014639407-2023-3-1401:47:22.png)

![image-20230314014659764](C:/Users/hp/AppData/Roaming/Typora/typora-user-images/image-20230314014659764.png)
