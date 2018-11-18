---
title: Java OOP
date: 2018-11-17 08:16:59
categories:[Java,Java基础]
tags:
top:
---

# 封装(Encapsulation )

隐藏内部细节

```java
//save as Student.java
package com.javatpoint;

public class Student {
    private String name;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}


//save as Test.java
package com.javatpoint;

class Test {
    public static void main(String[] args) {
        Student s = new Student();
        s.setName("vijay");
        System.out.println(s.getName());
    }
}

```

```
Compile By: javac -d . Test.java
Run By: java com.javatpoint.Test

Output: vijay
```

# 继承(Inheritance )

```java
class Calculation {
    int z;

    public void addition(int x, int y) {
        z = x + y;
        System.out.println("The sum of the given numbers:" + z);
    }

    public void Subtraction(int x, int y) {
        z = x - y;
        System.out.println("The difference between the given numbers:" + z);
    }
}


public class My_Calculation extends Calculation {
    public void multiplication(int x, int y) {
        z = x * y;
        System.out.println("The product of the given numbers:" + z);
    }

    public static void main(String[] args) {
        int a = 20;
        int b = 10;
        My_Calculation demo = new My_Calculation();
        demo.addition(a, b);
        demo.Subtraction(a, b);
        demo.multiplication(a, b);
    }
}

```

`My_Calculation`使用了继承自`Calculation`的方法`addition`和`Subtraction`以及变量`z`,只有`multiplication`是`My_Calculation` 自身定义的方法

避免了重复

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181118072716.png)

# 多态(Polymorphism )

## 静态编译时多态

### 例1

方法`重载(overload)`在编译时确定

```java
class Adder {
    static int add(int a, int b) {
        return a + b;
    }

    static double add(double a, double b) {
        return a + b;
    }
}

public static void main(String args[])
{
	System.out.println(Adder.add(11,11));
	System.out.println(Adder.add(12.3,12.6));
}
```

### 例2

方法的参数在编译阶段常被静态地绑定

在whichFoo方法中，形式参数arg2的类型是Base, 因此不管arg2实际引用的是什么类型，arg1.foo(arg2)匹配的foo都将是foo(Base)

```java
public class Main {
    public static void main(String[] args) {
        Base b = new Base();
        Derived d = new Derived();
        whichFoo(b, b);
        whichFoo(b, d);
        whichFoo(d, b);
        whichFoo(d, d);
    }

    public static void whichFoo(Base arg1, Base arg2) {
        arg1.foo(arg2);
    }
}


class Base {
    public void foo(Base x) {
        System.out.println("Base.Base");
    }

    public void foo(Derived x) {
        System.out.println("Base.Derived");
    }
}


class Derived extends Base {
    public void foo(Base x) {
        System.out.println("Derived.Base");
    }

    public void foo(Derived x) {
        System.out.println("Derived.Derived");
    }
}

```



## 动态运行时多态

### 例1

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20181118082653.png)

方法`重写(override)`在运行时确定

```java
public Class BowlerClass{
	void bowlingMethod()
	{
		System.out.println(" bowler ");
	}
	public Class FastPacer{
		void bowlingMethod()
		{
			System.out.println(" fast bowler ");
		}
		Public static void main(String[] args)
		{
			FastPacer obj = new FastPacer();
			obj.bowlingMethod(); // fast bowler
		}
	}
```

### 例2

```java
public class Main {
    public static void main(String[] args) {
        Father father = new Son();
        System.out.println(father.age);
        father.name();
        father.age();
    }
}


class Father {
    public int age = 60;

    public static void name() {
        System.out.println("father name");
    }

    public void age() {
        System.out.println("father age:" + age);
    }
}


class Son extends Father {
    public int age = 25;

    public static void name() {
        System.out.println("son name");
    }

    public void age() {
        System.out.println("Son age:" + age);
    }
}

```

```
output:
60
father name
Son age:25
```

当执行 `Father father = new Son();`发生了向上转型，在编译期间 father就是个Father对象，系统不知道实际上它是一个 Son对象，这得在运行期间由JVM判断

在我们调用`father.age`的时候实际上，在处理java类中的成员变量时，并不是采用运行时绑定，而是一般意义上的静态绑定，即调用的是`Father类的age成员变量`

在调用`father.name()`的时候，注意这是个static方法，java当中的方法final，static，private和构造方法是前期绑定，因此调用的是`Father类中的name方法`

在调用`father.age()`的时候，需要采用动态绑定，此时father会被解析成它实际的对象，即Son对象，因此实际调用的是`Son.age()`

# 参考

https://stackify.com/oops-concepts-in-java/

https://www.edureka.co/blog/object-oriented-programming/

https://blog.csdn.net/zlp1992/article/details/52557238