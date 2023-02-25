# BiShe-Xaramin-Forms
2023毕业设计项目 

首先 跟着B站 东北大学张引 老师进行视频学习,
了解Xamarin开发的流程和需要掌握的技术 ;
课程笔记 , Demo每次都上传;
整个流程学习完毕, 便开始上传 毕设 安卓开发部分 ;



[视频链接](https://space.bilibili.com/15135791/channel/seriesdetail?sid=1174637)

## 日志-打卡

:hourglass_flowing_sand: 2023年2月15日19:44:22

> * 配置好开发环境,
> * 模拟器 的Wifi无法连接,只能换用 真机 或者 wif调试, 后来尝试  真机成功
>   * google abd 驱动要安装, 真机本身的驱动也要安装 , 前者是 vs识别和转录代码,后者是电脑识别和转录代码
>   * abd 这个命令行有时还是很有用的   adb help 查看文档学习和使用
>   * 初步测试是成功 ;回去先把 文档文档学习好;
>
> **踩的坑**
>
> 1. 项目路径 不允许有 数字,空格和 中文, 只能说英文,;不允许长路径,建议放在二级路径就可以
> 2. 启用开发者模式,同时 VS要使用管理员模式启动
> 3. 工作负载 需要将UWP + 移动开发 两个 都安装



:hourglass_flowing_sand:

* B站视频学习,了解其他 跨平台,fullter,  Xamarin的好处在于 ,核心业务使用C#编写, 在不同的平台调用各平台的 UI就可以
* 微软 有 版本控制,这个需要注意 

**学习要点**

> 1. 项目模板用blank 空白模板可以学到一些东西, 用 现成的shell模板 给你打包好了,shell不能做uwp开发
> 2. 在主项目下面写程序, 自动会加载到不同的平台 ios, android, uwp
> 3. MainPage.xaml ,写UI的 ,之前是有 设计器/预览器, 之后被热重载替代了, 之前挺坑的,和网页设计差不多,
> 4. StackLayout ,显示内容 以 <Label  />为例, 标签前 和见过括号前后不可以有空格

**踩坑**

> 1. OPPO手机驱动老是异常这个需要在设备管理器中进行查看
> 2. 设置- 开发者模式 中开启,并打开 USB 设备发现



:hourglass: 2023年2月20日20:44:40

* B站视频学习 

学习要点:

> 1 图片资源存放问题,要在Vistual Studio 中的子项目,Android,ios,UWP中分别存放三份;
> Android 和ios 都在resources里的子文件粘贴,在UWP项目本身直接粘贴;不可以直接存放到路径中
>
> ==因为 在VS中粘贴后台会自行配置== 
>
> 1. 在资源管理器中,选择左上角的复制
> 2. 在未调试的状态下,drawable文件夹中粘贴就可以

> 2 线性布局 Stackout中 垂直布局时,宽好用,高不好用,水平布局,宽和高都好用
>
> UI设计上 固定好,不需要自己 深究,直接使用就行
>
> 3 滚动布局 ,插入图片,是 水平自适应高度,不需要滚动条,高度没有自适应,所以有滚动条

> 在 设置可以点击触发链接的按钮Label中
>
> 对于 Label本身的标签而言,简单的Margin属性可以设置简单的值,但是 设置复杂的链接属性就无法直接实现
>
> 就使用 GestureRecongizers来实现, 叫做Pproperty Element Syntax ==属性元素语法==
>
> 函数 异步 await, async  既是多线程,也不是多线程;

> ListView有个 ItemSource属性,在后台代码中 将其设为一个list
> ListView会将类名给toString,若查看对象内容,需要使用ItemTemplate,项目模板进行设置
>
> LiveView 和 一个List数据源绑定后,会将list中的每一项,作为DataTemplate,自动把每一个项 BindingContext给设置;
>
> 此时,一旦BingContext有了数据,就会自动的使用Binding关键字读取里面的数据
>
> **哪一项被选中了呢**
>
> ListView 本身是有一个点击事件
>
> e 就是被点击的poetry,调试,在控制台输入 e.Item就会返回该数据的值



:hourglass: 2023年2月22日14:46:17 

主要学习 第六章合集 , 全部学完,并整理出 和项目demo

我掌握的是 MVVM+IServices 流程如何运作 ;

View 界面 控件先基础布置好, 数据展示 和 方法调用 , 转移到 ViewModel中,

数据 承接 有 Model完成 ,但 ,ViewModel只做 创建连接这一件事  但 连接数据库 这件事 它不管,所以 我 需要引入 连接数据库的字段,放在Services中 , 实现这个接口, 由这个 实现类完成连接 

:hourglass: 2023年2月23日14:54:26

<iframe src="//player.bilibili.com/player.html?aid=845292633&bvid=BV1t54y1j76Y&cid=329312439&page=1" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true"> </iframe>

> 主要学习 7 Database 合集内容 到第49集

:hourglass: 2023年2月24日15:26:22 

> 预计能学习 1个半小时 
>
> 学习结束  2023年2月24日17:29:28   :hourglass: 2个小时 

:hourglass: 2023年2月25日22:10:12

> 预计能学习2个小时 ;
>
> 
