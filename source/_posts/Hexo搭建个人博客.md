---
title: Hexo搭建个人博客
date: 2018-10-21 17:54:09
categories: 
- 工具建设
tags: 
- hexo
---

[TOC]



本文介绍如何使用`Hexo`搭建个人博客并托管到`GithubPages`的过程,使用`Next(6.x版本)的Gemini`主题,持续集成工具为`travis ci`

需要提前安装好git和node.js,并熟悉github的基本操作,访问部分工具需要科学上网

为方便说明 **假设你的github名为jay,实际操作时请`替换`成自己的用户名**

# 新建Github仓库

仓库名为jay.github.io

新建完成后

```bash
git clone git@github.com:jay/jay.github.io.git
cd jay.github.io
git checkout -b blog
```

这里把仓库clone到了本地,新建并切换到了blog分支

下面的操作都将在blog分支根目录进行(以下简称为根目录),blog分支用于保存源文件,master分支用于发布.**不要手动操作master分支**

> 也可以使用两个repo,分别作为源码版本管理的repo和发布的repo,不过一个已经够用了

# 安装Hexo

hexo详细教程请参考[hexo官网](https://hexo.io/zh-cn/),此处列出一些简要步骤

- 全局安装hexo脚手架

  ```bash
  npm install hexo-cli -g
  ```

-  在根目录生成hexo网站模板到temp文件夹

   ```bash
   hexo init temp
   ```

-  复制 temp文件夹中的内容到根目录,并`\themes`文件夹下的默认`landscape`主题,然后删除temp文件夹

约定 根目录下的`_config.yml`文件为`站点配置文件`

# 安装Next主题

```bash
git clone https://github.com/theme-next/hexo-theme-next themes/next
```

将next主题拷贝至 themes/next文件夹下,删除next文件夹下多余的文件,除了下图红框中包含的文件和文件夹之外全部删除,尤其注意要删除next下隐藏的**.git文件夹**

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181021193233.png)

约定 next文件夹下的`_config.yml`文件为`主题配置文件`

在站点配置文件中启用next主题

```yml
theme: next
```

在主题配置文件中启用`Gemini`分主题,去掉#注释

```yml
# Schemes
# scheme: Muse
#scheme: Mist
#scheme: Pisces
scheme: Gemini
```

此时可以在本地调试看一下效果

```bash
hexo s
```

`hexo s`是 `hexo server`的缩写,启动后可以复制地址到浏览器中查看,默认为`http://localhost:4000`

# 调整Next样式

## 基本配置

### 修改语言,标题等信息

修改站点配置文件

```yml
# Site
# 语言，原来是zh-Hans
language: zh-CN
# 统计插件
# hexo-symbols-count-time
symbols_count_time:
  symbols: true
  time: true
  total_symbols: false
  total_time: false
```

顺便在`# Site`处修改站点标题,作者等信息

### 修改头像

修改主题配置文件

```yml
Avatar
avatar:
  # in theme directory(source/images): /images/avatar.gif
  # in site  directory(source/uploads): /uploads/avatar.gif
  # You can also use other linking images.
  #/images/avatar.gif
  # 你的头像地址
  url:
 
  # If true, the avatar would be dispalyed in circle.
  rounded: true
  # The value of opacity should be choose from 0 to 1 to set the opacity of the avatar.
  opacity: 1
  # If true, the avatar would be rotated with the cursor.
  rotated: true
```

- url  头像图片网址,也可以使用`/images`目录下的文件
- rounded 是否为圆形
- rotated 鼠标hover时是否有旋转效果

### 修改社交信息

修改主题配置文件

```yml
social:
  GitHub: https://github.com/jay || github
  # E-Mail:
  #Google: https://plus.google.com/yourname || google
  #Twitter: https://twitter.com/yourname || twitter
  #FB Page: https://www.facebook.com/yourname || facebook
  #VK Group: https://vk.com/yourname || vk
  #StackOverflow: https://stackoverflow.com/yourname || stack-overflow
  #YouTube: https://youtube.com/yourname || youtube
  #Instagram: https://instagram.com/yourname || instagram
  #Skype: skype:yourname?call|chat || skype
```

hexo采用[fontawesome](https://fontawesome.com/)字体图标,每个||之前的内容代表文件路径,||之后的内容代表字体图标class,如github的字体图标渲染时class为`fa-github`

## 主要样式

`\themes\next\source\css\_custom\custom.styl`中加入以下代码

```stylus
body {
    font-family: 'Microsoft YaHei', sans-serif;
}

/* 上方留白 */
.header-inner, .content {
    margin-top: 20px;
}

/* 背景设置 */
[class='container sidebar-position-left '], .main, [class$='page-home'] #footer, [class$='page-post-detail'] #footer, .page-archive {
    background: rgba(255, 255, 255, 0.83);
}

[class='container sidebar-position-left '] .main, [class$='sidebar-position-left'] #footer, .page-archive .main {
    background: transparent;
}

.pagination, .post-block, .comments, .container .header-inner, .sidebar-inner {
    background: rgba(255, 255, 255, 0.45);
}

// 归档
#sidebar {
    background: rgba(255, 255, 255, 0);
}

.menu .menu-item a:hover {
    border-bottom-color: transparent;
    background: #2222223b;
}

.menu-item-active a {
    border-bottom-color: transparent;
    background: #fbcb3fbd;
}

.tabs ul.nav-tabs li.tab.active, .tabs .tab-content {
    background-color: rgba(255, 255, 255, 0.62) !important;
}

.gitment-editor-body textarea {
    background-color: #ffffff9e !important;
}

.gitment-editor-preview-field {
    background-color: #ffffff40 !important;
}

.gitment-comment-header {
    background-color: transparent !important;
}

#gitment-container a {
    border-bottom: none;
}

/* 侧边栏 */
.post-toc ol a {
    border-bottom-color: transparent;

    &:hover {
        border-bottom-color: #666;
    }
}

// active状态下的超链接样式
.post-toc .nav li[class$='active']>.nav-link {
    border-bottom-color: transparent;

    &:hover {
        border-bottom-color: #fc6423;
    }
}

// 当前正在访问的超链接样式
.post-toc .nav li[class$='active-current']>.nav-link {
    display: block;
    padding: 5px 7px;
    border-bottom-color: transparent;
    border-radius: 3.6px;
    background: #337ab7;
    color: white;

    &:hover {
        background: orange;
        color: black;
    }
}

.site-title {
    font-weight: bold;
}

.site-state-item {
    border-left: 1px solid #b1b1b1;
}

// 各级目录的显示宽度 - 主要是为了不让边框超出侧边栏
.post-toc .nav .active-current.nav-level-2 a {
    width: 303px;
}

.post-toc .nav .active-current.nav-level-3 a {
    width: 296px;
}

.post-toc .nav .active-current.nav-level-4 a {
    width: 286px;
}

.post-toc .nav .active-current.nav-level-5 a {
    width: 276px;
}

.post-toc .nav .active-current.nav-level-6 a {
    width: 266px;
}

.post-toc {
    width: calc(100% + 17px);
}

/* 标题 */
.posts-expand article:only-of-type h1 {
    font-weight: bold;
    font-size: 37px;
}

// 一级标题(正常显示器分辨率)
// 其实要在左下角和右下角加上一个折角、翻折的效果（一个三角形）
.post-body h1 {
    position: relative;
    margin: 20px -42.3px 15px -48px;
    padding-left: 1.7%;
    height: 60px;
    border: 1px solid #ddd;
    border-radius: 2px;
    background-color: #eaf1f7;
    box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.12), 0 3px 1px -2px rgba(0, 0, 0, 0.06), 0 1px 5px 0 rgba(0, 0, 0, 0.12);
    font-weight: bold;
    line-height: 60px;
}

// 二级标题(移动终端分辨率)
@media (min-width: 768px) and (max-width: 991px) {
    .main .post-body h2 {
        margin: 20px -23px 15px -28px;
    }
}

@media (max-width: 767px) {
    .main .post-body h2 {
        margin: 20px -15px 15px -18px;
    }
}

// 三级标题
.post-body h3 {
    padding-bottom: 0.3em;
    border-bottom: 1px solid #cfd8dc !important;
}

/* 文章页面内的各种元素 */
.post-body .note {
    border-color: #ddd;
    background-color: #f9f9f980;
}

// 文章内的普通超链接
.post-body a {
    border-bottom: none;
    color: #0593d3;

    &:hover {
        color: #FF4500;
    }
}

// 代码块内的超链接
.post-body a code {
    border-bottom: none;
    color: #0593d3;

    &:hover {
        color: #FF4500;
    }
}

// 分页导航（上/下一篇文章的超链接）
.post-nav-item a {
    border-bottom: none;
    color: #0593d3;

    &:hover {
        color: #FF4500;
    }
}

// 阅读全文按钮的超链接
.post-button .btn {
    border-bottom: 2px solid #555;
}

// 无序列表
.posts-expand .post-body ul li {
    list-style: disc;
}

// 有序列表
.posts-expand .post-body ol {
    counter-reset: counter;
}

.posts-expand .post-body ol>li {
    position: relative;
    list-style-type: none;
}

.posts-expand .post-body ol>li:before {
    position: absolute;
    top: 5px;
    left: -27px;
    padding: 3px;
    width: 13px;
    height: 13px;
    border-radius: 50%;
    background: #49b1f5; // #929ca2;
    color: #fff;
    content: counter(counter);
    counter-increment: counter;
    text-align: center;
    font-weight: 500;
    font-size: 12px;
    line-height: 1;
}

// fancybox图片
.posts-expand .post-body .fancybox img {
    display: block !important;
    margin-left: 0;
    cursor: pointer;
    cursor: zoom-in;
    cursor: -webkit-zoom-in;
}

// 图片底下的描述性文字
.post-body .image-caption, .post-body .figure .caption {
    text-align: left;
}

// label块
.post-body .label {
    margin: 0 3px;
    padding: 3px 4px;
    border-radius: 0.25em;
    font-weight: bold;
}

.post-body .label.primary {
    background-color: #ecd6ff;
}

.post-body .label.info {
    background-color: #c3e8f9;
}

.post-body .label.success {
    background-color: #b2eabb;
}

.post-body .label.default {
    background-color: #dcdcdc;
}

.post-body .label.warning {
    background-color: #ffe89a;
}

.post-body .label.danger {
    background-color: #f9c1ca;
}

// 代码区
.highlight .code pre {
    background-color: transparent;
}

.highlight {
    border: 1px solid #ddd;
    border-radius: 2px;
}

// 原样输出的代码块
pre {
    border: 1px solid #ddd;
    border-radius: 2px;
    background: #f9f9f980;
}

pre code {
    border: transparent;
}

pre, .highlight {
    // background-color: #1d1f21;
    // color: #c5c8c6;
    // font-size: 13px;
}

.highlight .gutter pre {
    // background-color: #000;
    // color: #888f96;
}

// 块引用
blockquote {
    border-left-color: #6b8afb;
    background-color: #f9f9f980;
}

// 表格奇数行
.highlight table>tbody>tr {
    &:nth-of-type(odd) {
        background-color: transparent;
    }
}

// 表格每一行鼠标经过时的颜色
table>tbody>tr {
    &:hover {
        background-color: #FFFFF0;
    }
}

// 表格的单元格
table td, table th {
    border-right: 1px solid #dcdcdc;
}

// 侧边栏头部
.site-meta {
    background: #337ab7;
}

// 文章底部的标签
.posts-expand .post-tags a {
    padding: 3px 4px;
    border-bottom: none;
    border-radius: 3px;
    background: #e6e6e6;
    color: #2ca6cb;

    &:hover {
        background: #2ca6cb;
        color: white;
    }
}

// 分割线
hr {
    margin: 0;
    height: 1px;
    border: none;
    background-image: none;
}

// code代码块
code {
    margin: 0 2px;
    background: #dcdcdc;
}

// 选项卡被选中的那一项
.tabs ul.nav-tabs li.tab.active a {
    font-size: 19px;
}

// 浏览器滚动条
::-webkit-scrollbar {
    width: 8px;
    height: 8px;
}

::-webkit-scrollbar-thumb {
    background: #49b1f5;
}

::-webkit-scrollbar-track {
    background-color: transparent;
}

.page-archive #footer {
    position: inherit;
    padding-bottom: 1.9%;
    height: 1px;
}

.feed-link {
    display: none;
}
```

## 显示宽度

在`\themes\next\source\css\_variables\custom.styl`增加以下配置信息

```stylus
$content-desktop = 75%;
$content-desktop-large = 75%;
$content-desktop-largest = 75%;
```

# 增加其他功能

## 增加标签和分类

1. 根目录下输入命令,会在根目录的source文件夹下生成categories文件夹和tags文件夹

   ```bash
    hexo new page categories   
    hexo new page tags   
   ```

2. 编辑新建的页面`source/categories/index.md`

   ```markdown
   ---
   title: 分类
   date: 2018-10-21 20:34:08
   type: "categories"
   ---
   ```

3. 编辑新建的页面`source/tags/index.md`

   ```markdown
   ---
   title: 标签
   date: 2018-10-21 20:34:08
   type: "tags"
   ---
   ```

4. 编辑文章模板`scaffolds\post.md`

   ```markdown
   ---
   title: {{ title }}
   date: {{ date }}
   categories:
   tags:
   ---
   ```

5. 修改 主题配置文件,增加菜单栏

   ```yml
   menu:
     home: / || home
     #about: /about/ || user
     tags: /tags/ || tags
     categories: /categories/ || th
     archives: /archives/ || archive
   ```


可以使用`hexo new "文章名"`生成新的文章查看效果,文章的标签和分类实例如下

```markd
---
title: Hexo搭建个人博客
date: 2018-10-21 17:54:09
categories: 
- 工具建设
tags: 
- hexo
- 教程
---
```

一篇文章只能有**一个分类**,可以有**一个或多个标签**

### 增加站内搜索

1. 安装站内搜索插件

   ```bash
   npm install hexo-generator-searchdb --save
   ```

2. 修改 站点配置 文件

   ```yml
   search:
     path: search.xml
     field: post
     format: html
     limit: 10000
   ```

3. 修改 主题配置 文件

   ```yml
   local_search:
       enable: true
   ```

本地网站重启后可以查看效果

## 增加RSS订阅

1. 安装rss插件

   ```bash
   npm install hexo-generator-feed
   ```

2. 修改站点配置文件,在底部增加

   ```yml
   # Extensions
   plugins:
       hexo-generator-feed
   #Feed Atom
   feed:
       type: atom
       path: atom.xml
       limit: 20
   ```

3. 修改`themes\next\languages\zh-CN.yml`,新增`rss: RSS订阅`

   ```yml
   menu:
     home: 首页
     archives: 归档
     categories: 分类
     tags: 标签
     rss: RSS订阅
     about: 关于
     search: 搜索
   ```

4. 修改主题配置文件,在menu中增加rss菜单

   ```yml
   menu:
     home: / || home
     #about: /about/ || user
     tags: /tags/ || tags
     categories: /categories/ || th
     archives: /archives/ || archive
     rss: /atom.xml || rss
   ```

使用`hexo g`生成`atom.xml`后重启网站查看效果,`hexo g`是`hexo generate`的缩写

## 增加评论区

hexo支持多款评论插件,经过对比之后使用`gitalk`

1. 在GitHub上注册新应用,[注册地址](https://github.com/settings/applications/new)

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181021214830.png)

   参数说明

   - Application name： # 应用名称，随意
   - Homepage URL： # 网站URL，如`https://jay.github.io`
   - Application description # 描述，随意
   - Authorization callback URL：# 网站URL，`https://jay.github.io`

   点击注册后，页面跳转如下，其中`Client ID`和`Client Secret`在后面的配置中需要用到，到时复制粘贴即可

2. 根目录下安装`gitalk`

   ```bash
   npm i --save gitalk
   ```

3. 修改主题配置文件,增加

   ```yml
   # Support for LiveRe comments system.
   # You can get your uid from https://livere.com/insight/myCode (General web site)
   #livere_uid: your uid
   enable: true
   githubID: jay    
   repo: 你的github上任意一个公开的repo
   ClientID: 步骤1中的ClientID
   ClientSecret: 步骤1中的ClientSecret
   adminUser: jay
   distractionFreeMode: true
   ```

   实际的评论会生成为repo中的issue

4. 新建`themes\next\layout\_third-party\comments\gitalk.swig`文件，并添加内容：

   ```js
   {% if page.comments && theme.gitalk.enable %}
     <link rel="stylesheet" href="https://unpkg.com/gitalk/dist/gitalk.css">
     <script src="https://unpkg.com/gitalk/dist/gitalk.min.js"></script>
     <script src="https://cdn.bootcss.com/blueimp-md5/2.10.0/js/md5.min.js"></script>
      <script type="text/javascript">
           var gitalk = new Gitalk({
             clientID: '{{ theme.gitalk.ClientID }}',
             clientSecret: '{{ theme.gitalk.ClientSecret }}',
             repo: '{{ theme.gitalk.repo }}',
             owner: '{{ theme.gitalk.githubID }}',
             admin: ['{{ theme.gitalk.adminUser }}'],
             id: md5(window.location.pathname),
             distractionFreeMode: '{{ theme.gitalk.distractionFreeMode }}'
           })
           gitalk.render('gitalk-container')
          </script>
   {% endif %}
   ```

   这里用了一个md5，同时采用了 `windows.location.pathname`来区分不同的文章,md5算法来自`https://cdn.bootcss.com/blueimp-md5/2.10.0/js/md5.min.js`

   **解决了那个issue label长度不能超过50的问题**

5. 引入`gitalk.swig`

   修改`themes\next\layout\_third-party\comments\index.swig`,添加内容

   ```js
   {% include 'gitalk.swig' %}
   ```

6. 修改`themes\next\layout\_partials\comments.swig`,添加内容(注释的为需要添加的内容,注意取消注释)

   ```js
    {% elseif theme.valine.enable and theme.valine.appid and theme.valine.appkey %}
       <div class="comments" id="comments">
       </div>
     //{% elseif theme.gitalk.enable %}
     //<div id="gitalk-container"></div>
     {% endif %}
   {% endif %}
   ```

7. 新建`themes\next\source\css\_common\components\third-party\gitalk.styl`

   ```stylus
   .gt-header a, .gt-comments a, .gt-popup a {
       border-bottom: none;
   }
   .gt-container .gt-popup .gt-action.is--active:before {
       top: 0.7em;
   }
   ```

8. 修改`themes\next\source\css\_common\components\third-party\third-party.styl`,添加内容

   ```stylus
   @import 'gitalk' if (hexo-config('gitalk.enable'));
   ```

   评论区的正式效果需要发布后才能查看

# 发布

1. 安装 [hexo-deployer-git](https://github.com/hexojs/hexo-deployer-git)。

   ```bash
   npm i hexo-deployer-git --save
   ```

2. 修改站点配置文件,添加内容

   ```yml
   deploy:
     type: git
     repo: git@github.com:jay/jay.github.io.git
     branch: master
   ```

   将发布到master分支

3. 使用`hexo generate --deploy`发布,可以简写成`hexo g -d`

至此,我们完成了博客的搭建,但是每次有新的文章都要发布,我们可以使用持续集自动化这个过程

# 持续集成

使用`Travis CI`进行持续集成

1. 生成github token

   在github点击头像 - setting - Developer settings- Personal access tokens  
   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181020203431.png)
   点击底部的生成,token只会出现一次,保存生成的token备用

2. 使用Github账号登录[Travis CI官网](https://travis-ci.org/)

3. `https://travis-ci.org/account/repositories`下选择博客所在的仓库
   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181020204823.png)

4. 进入项目，在`More options`中点击setting

   添加步骤1中生成的token,key值为`GH_TOKEN`
   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181020212821.png)

5. 在根目录下新建`.travis.yml`文件,添加内容:

   ```yml
   language: node_js
   node_js: stable
   # S: Build Lifecycle
   install:
     - npm install
   #before_script:
    # - npm install -g gulp
   script:
     - hexo g
   after_script:
     - cd ./public
     - git init
     - git config user.name "这里填你的github用户名,这里假定为jay"
     - git config user.email "这里填你的github邮箱"
     - git add . -f
     - git commit -m "Update docs"
     - git push --force --quiet "https://${GH_TOKEN}@${GH_REF}" master:master
   branches:
     only:
       - blog
   env:
    global:
      - GH_REF: github.com/jay/jay.github.io.git
   ```

   注意`GH_REF`填写**实际的项目地址,是https://后面的内容,不是ssh的地址**

现在我们对blog分支推送的任何更新都会由`Travis CI`将构建后的内容推送到master分支,可以在`Travis CI`查看构建的过程

可以删除原来手动发布所需的相关依赖和配置,当然保留也没关系

# 参考7

参考