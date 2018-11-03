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