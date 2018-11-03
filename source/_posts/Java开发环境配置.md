---
title: Java开发环境配置
date: 2018-11-03 07:48:03
categories:
- Java
tags:
- Java基础
top:
---

# JDK安装和配置

## Windows环境

1. 从Java官网下载Windows版本的JDK ( Java SE Development Kit 8u161): <http://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html>

2. 点击安装包进行安装，记住安装路径

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103095231.png)图片

3. 设置Windows环境变量Java_Home：

   ```
   C:\Program Files\Java\jdk-1.8.0_161
   ```

4. 设置Windows环境变量Path，在原有内容之后添加:

   ```
   ;%Java_Home%\bin
   ```

5. 设置Windows环境变量CLASSPATH，在原有内容之后添加:

   ```
   ;%Java_Home%\lib\dt.jar;%Java_Home%\lib\tools.jar
   ```

   如果环境变量CLASSPATH不存在，则创建，设置其值为:

    ```
    %Java_Home%\lib\dt.jar;%Java_Home%\lib\tools.jar
    ```

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103095314.png)

6. 运行命令`java -version`和`javac -version`，测试安装是否成功

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103095841.png)

## MacOS环境

1. 从Java官方网站下载macOS版本的JDK (Java SE Development Kit 8u161)：<http://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html>

2. 一直点击下一步安装

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103094840)

3. **Java默认安装路径**: `/Library/Java/JavaVirtualMachines/jdk1.8.0_161.jdk/`
   JDK Home目录：`/Library/Java/JavaVirtualMachines/jdk1.8.0_161.jdk/Contents/Home` (配置IDE需要)

4. 配置环境变量，在.zshrc (根据使用shell对应更改):

   ```
   export JAVA_HOME=/Library/Java/JavaVirtualMachines/jdk1.8.0_161.jdk/Contents/Home 
   export PATH=$JAVA_HOME/bin:$PATH
   export CLASSPATH=JAVA_HOME/libh/tools.jar:$JAVA_HOME/lib/dt.jar
   ```

5. 运行命令 `source .zshrc`

6. 确认安装成功，运行命令 `java -version`, 正确的输出为:

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103094938.png)

# Maven安装和配置

## Windows 安装

### 下载和安装

1. 从[Maven官方](http://www-us.apache.org/dist/maven/maven-3/3.5.4/binaries/apache-maven-3.5.4-bin.zip)下载安装压缩包
2. 解压文件夹 `apache-maven-3.5.4` 到任意文件夹, 例如我解压到 `http://www-eu.apache.org/dist/maven/maven-3/3.5.4/source/apache-maven-3.5.4-src.zip`



![image.png](https://video.jirengu.com/xdml/image/11621da1-d0da-451b-aad4-07530e96963c/2018-9-7-23-22-10.png)image.png



### 设置环境变量

1. 设置环境变量 `M2_HOME` 为maven安装的路径, 例如:



![image.png](https://video.jirengu.com/xdml/image/11621da1-d0da-451b-aad4-07530e96963c/2018-9-7-23-29-40.png)image.png



1. 添加`%M2_HOME%\bin`到系统环境变量path中, 例如:



![image.png](https://video.jirengu.com/xdml/image/11621da1-d0da-451b-aad4-07530e96963c/2018-9-7-23-30-39.png)image.png



### 运行命令测试安装

运行命令: `mvn -version`:



![image.png](https://video.jirengu.com/xdml/image/11621da1-d0da-451b-aad4-07530e96963c/2018-9-7-23-31-34.png)



## macOS 安装

### 使用brew安装

运行命令: `brew update; brew install maven`

### 运行命令测试安装

运行命令: `mvn -version`

# IDE配置

## VSCode配置

### 	一.基础环境

1. 在插件市场中搜索“Java Extension Pack”并安装。 

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103101750.png)

2. 在磁盘中创建一个临时空目录，并创建`Main.java`。之后，使用VSCode打开该目录。 

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103102149.png)

3. 在`Main.java`文件中输入或粘贴要测试的代码 

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103101837.png)

4. 打开`调试`选项卡（或按下“Ctrl+Alt+D”），并选择`添加配置`。

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103102625.png)

5. 打开“launch.json”，并修改配置项“mainClass”为真实的主类名（本例中为“Main”）。

     ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103102700.png)

6. 在启动调试前，还需要设置“JAVA_HOME”的路径。打开“文件”--“首选项”--“设置”（或按下“Ctrl+,”），并修改“java.home”配置项为真实的“JAVA_HOME”路径。 

     ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103102824.png)

7. 至此，环境配置完毕，可直接按下“F5”键启动调试，并可正常下断点和监视变量。

     ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103103340.png)

### 二.Maven环境

1. 首先配置可以正常使用的Maven环境。

2. 新建一个临时空目录，在其中创建必须的几个文件和文件夹，包括`pom.xml`、`src/main/java`。

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103103832.png)

3. 修改VSCode中与Maven相关的配置，将Maven可执行文件（windows平台为`mvn.cmd`）的绝对路径填写在`maven.executable.path`配置项中，并将Maven配置文件`settings.xml`的绝对路径填写在`java.configuration.maven.userSettings`配置项中。

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103104037.png)

4. 保存配置并重启VSCode，之后在“pom.xml”文件上右击，并选择“Update project configration”。

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103104122.png)

5. 之后，VSCode的Java插件将会在工作区中生成相关的文件（“.classpath”、“target”等）。

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103104220.png)

6. 在工作区下方，即可选择Maven工程的常用命令。

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103104349.png)

7. 配置完成后，代码中可直接提示出“pom.xml”包含的包中的类。

   ![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181103104617.png)

## IntelliJ IDEA 安装配置

### 一. IntelliJ IDEA安装

在Intellij官方网站下载Community版

- Windows 下载地址: <https://www.jetbrains.com/idea/download/#section=windows>
- Mac 下载地址: <https://www.jetbrains.com/idea/download/#section=mac>
- Linux 下载地址: <https://www.jetbrains.com/idea/download/#section=linux>

直接双击安装包，开始安装

1. 选择安装JRE



![图片](https://video.jirengu.com/FnOcFeS_xnuns2FXpSWwE6TxmI3E)图片



1. 选择导入配置信息或者不导入配置信息



![图片](https://video.jirengu.com/Fh6y9o82Aa7SxvbQxoDSSqXXznHf)图片



### 二. IntelliJ IDEA 配置

如果你没有导入配置文件，第一次运行IDEA会看到一个设置导航

1. 选择喜欢的UI配色



![图片](https://video.jirengu.com/FqJZlN1LDg2ylN01GRTycBQnM542)图片



1. 选择安装推荐插件，这里保持默认就好，或者根据你的需要安装插件



![图片](https://video.jirengu.com/FncMdxs_TZSW5Z1_az8ENVK5W03x)图片



1. 配置build任务，保持默认就好



![图片](https://video.jirengu.com/FnJVuCH2HtnNxIBbXNkG-A38uQxJ)图片



### 三. 配置项目默认JDK

1. 在IDEA启动窗口，选择 *Configure* -> *Project Default* -> *Project Structure*, 打开 *Default Project Structure* 窗口



![图片](https://video.jirengu.com/FiXfBLnFy34c7yIH88OZ3dfq04ZU)图片



1. 在 *Project SDK* 标签下，选择一个 Java SDK或者点击 *New* -> *+ JDK* 选择新的Java SDK



![图片](https://video.jirengu.com/Fnb0dlwgcxlBahtx7IJa3MO1wRQa)图片



1. 点击右下角 *确认*

### 四. 创建第一个Java项目

1. 点击Java启动窗口的 *Create New Project*



![图片](https://video.jirengu.com/FsvTaR5741SGm6Z_YdvwJyethcxK)图片



1. 左边标签栏选择 *Java*, 在Project设置窗口中还可以选择 *Project SDK*，不需要选择其他的库和框架，点击下一步



![图片](https://video.jirengu.com/FuD-u9FcqE5ScRTGhgQW0SHjy6PV)图片



1. 在 *New Project* 窗口下，选择 *Create Project from template (从模板创建项目)*，然后勾选 *Command Line App (命令行程序)*，点击下一步



![图片](https://video.jirengu.com/FsPlAXkGCT__7YOI44jNrKtkSeIT)图片



1. 设置 *Project Name*，选择 *Project location*，此处可以保持默认 *package*，课程中会再介绍 package 概念，然后点击下一步，完成项目创建。



![图片](https://video.jirengu.com/FqdotC7vXBF_bU7qdtUYeao__CvV)图片

### 五. IntelliJ IDEA 插件推荐

- **Material Theme UI**： 一个很好看的 UI 主题
- **IdeaVim**: 在IDEA里使用VIM编辑代码， Vim已死，Vim永存~
- **WakaTime**: 记录每天写代码的时间

# 参考

- [使用VSCode搭建简单Java开发环境（一、基础环境）](http://blog.hotsun168.com/index.php/archives/10/)
- [使用VSCode搭建简单Java开发环境（二、Maven环境）](http://blog.hotsun168.com/index.php/archives/16/)