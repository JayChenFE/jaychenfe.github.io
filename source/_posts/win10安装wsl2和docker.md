---
title: win10安装wsl2和docker
date: 2019-08-28 21:43:29
categories:
- 工具建设
tags:
top:
---

[TOC]

# 参考

https://zhuanlan.zhihu.com/p/69121280

# 踩过的坑

- WSL2需要Windows build 18917 更新,WSL不需要.但是最好**首先**将操作系统更新到需要的版本,**而不是**先安装WSL再更新
- **不要**安装不在windows商店的Linux发行版(包括github上的各种centos以及centos官方docker镜像,centos目前不是windows商店的Linux发行版之一)

# 安装过程

以下默认使用管理员权限打开powershell

## 1.确认操作系统版本

开始菜单(右键)>设置>系统>关于>windows规格

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190824083248.png)

如果版本号(小数点之前的数字)小于18917需要更新,否则进入步骤3安装wsl 





## 2.升级window版本

### 2.1开启快速通道(Insider Preview)

开始菜单(右键)>设置>更新和安全>windows预览体验计划

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190828223853.png)
<br/>

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190829074144.png)

<br/>

### 2.2更新

开始菜单(右键)>设置>更新和安全>windows更新

开始更新,**可能需要重启,时间比较长**

更新完成后如下:

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190829075855.png)

操作系统版本大于18917即可

## 3.安装wsl

### 3.1 开启wsl

在powershell命令行中输入如下命令开启wsl

```powershell
Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux  
```

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190822214926.png)
可能需要重启

### 3.2 在windows商店中选择合适的linux发行版

这里我选择了Ubuntu,安装

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190829080630.png)

### 3.3测试wsl

命令行中输入`wsl -l -v 查看`,这里版本应该是1

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190829081316.png)

## 4.升级到wsl2

### 4.1开启VirtualMachinePlatform

```powershell
Enable-WindowsOptionalFeature -Online -FeatureName VirtualMachinePlatform
```

### 4.2将发行版的wsl版本设置为2

命令为`wsl --set-version <Distro> 2`,例如`wsl --set-version Ubuntu 2`

如果想使wsl2 成为默认架构，可以使用以下命令执行此操作:

```powershell
wsl --set-default-version 2
```

这将使你安装的任何新发行版初始化为 wsl2 发行版。

### 4.3测试wsl2

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190829082356.png)

## 5.安装docker

### 5.1进入wsl2

输入`wsl -u root`进入wsl2,并切换到根目录

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190829082930.png)

### 5.2快速安装docker

```shell
$ curl -fsSL https://get.docker.com -o get-docker.sh
$ sudo sh get-docker.sh
$ sudo service docker start
```

测试一下,运行`docker run hello-world`

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190829083356.png)

### 5.3在docker中安装mysql

在https://hub.docker.com/_/mysql找到合适的mysql镜像版本,这里使用mysql5.7.27

运行如下命令

```powershell
docker run --name mysql -p3306:3306 -e MYSQL_ROOT_PASSWORD=123qweasd -d mysql:5.7.27
```

这里启动了一个mysql5.7.27的docker镜像,密码为`123qweasd`,并将docker的3306端口(mysql默认端口)映射到了wsl2的3306端口

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190828215546.png)

在wsl2中使用`ifconfig`获取局域网ip记住

## 6.在windows中测试

在Win10中打开IDEA旗舰版,用自带的数据库连接工具连接成功

host和mysql密码和上一步相同

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190828215721.png)

至此,在windows中连接wsl2中的docker中的mysl成功