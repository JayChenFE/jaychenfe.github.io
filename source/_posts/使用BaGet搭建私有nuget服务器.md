---
title: 使用BaGet搭建私有nuget服务器
date: 2019-08-15 22:18:30
categories: 
- 工具建设
tags:
top:
---

[TOC]

# 参考文章

https://medium.com/@onurvatan/net-core-custom-nuget-server-baget-on-docker-b763a3c7a276

# 缘起

在微服务中,有公用的业务类库,如果用共享代码的方式引入,每个使用的人会带来版本维护上的诸多麻烦.如果发到官方`nuget`上,有两个问题

- 代码太菜T_T
- 公司内部业务私密问题

为此我们需要搭建一个私有的`nuget`服务器. 虽然微软官方有[nuget-server](https://docs.microsoft.com/zh-cn/nuget/hosting-packages/nuget-server)这种实现方式,但是还是有些麻烦.在查看了[官方推荐的第三方实现列表](https://docs.microsoft.com/zh-cn/nuget/hosting-packages/overview)后,决定使用[BaGet](https://github.com/loic-sharma/BaGet)

# 步骤

公司服务器的环境是CentOS 7,已安装了docker

## 配置环境

在任务目录中新建`baget_nuget`文件夹,用来存放相关内容,并在其中建立子文件夹`baget-data`和`baget.env`文件,文件内容为

```txt
ApiKey=你的ApiKey
Storage__Type=FileSystem
Storage__Path=/var/baget/packages
Database__Type=Sqlite
Database__ConnectionString=Data Source=/var/baget/baget.db
Search__Type=Database
AllowPackageOverwrites=True
```

`BaGet`的后端使用.net core,前端使用react.`baget.env`文件存储了`BaGet`配置所需的环境变量,其中`ApiKey`是发布类库时需要用到的key

## 在docker中运行

切换到上一步中新建的`baget_nuget`文件夹下,运行

```she
docker run --rm --name nuget-server -p 5555:80 --env-file baget.env -v "$(pwd)/baget-data:/var/baget" loicsharma/baget:latest
```

此时docker的80端口就运行起了`Baget`的`nuget-server`并映射到了服务器本机的5555端口

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190815231318.png)

如果有图形界面,应该就可以看见如下的内容了

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190815232246.png)

如果没有图形界面在命令行中用`curl http://localhost:5555`测试一下,出现html内容文本即可

测试ok后进入下一步

##  配置nginx映射

如果服务器的5555端口可以暴露出去,就可以省略这一步直接到下一步.

因为公司只有一个入口所以需要配置nginx映射

公司访问的域名是`www.xxxx.com`,我们需要事先在云的CDN在配置好二级域名,这里我们使用`nuget.xxx.com`

nginx配置为

```c
server {
    listen 80;
    server_name nuget.xxx.com;

    location / {
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header Host $http_host;
        proxy_set_header X-NginX-Proxy true;

        proxy_pass http://localhost:5555/;
    }
}
server {
    listen       80 default_server;
   // ....其他配置
}
```

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190815232937.png)

这样我们就把对nuge.xxx.com的请求转发到了5555端口

## 发布库并测试

- 在本地新建一个TestLib随意输入一些方法

  ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190816073646.png)

- 切换到TestLib目录使用`dotnet pack`命令打包

  ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190816073951.png)

  生成了`nupkg`打包文件

- 切换到`bin\Debug`目录并使用发布

  执行`dotnet nuget push -s http://nuget.xxx.com/v3/index.json -k 你的ApiKey TestLib.1.0.0.nupkg`

  ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190816082127.png)

  发布成功

  ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190816082317.png)

- 使用类库

  1. 新建一个控制台程序CallTest
     ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190816082929.png)
  
  2.  在 `工具/选项/Nuget包管理器/程序包源`  中增加私有nuget服务地址
  
     名称随意 这里我使用了nuget.xxx.com,源填写`http://nuget.xxx.com/v3/index.json`
  
     ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190816083449.png)
  
     添加成功
  
     ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190816083854.png)
  
  3. 引入TestLib
  
     ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190816084040.png)
  
     选择私有地址,可以找到我们刚刚上传的库,安装
  
     ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190816084227.png)
  
     这样就可以在程序中使用了
  
  4. 更新类库
  
     待更新.....