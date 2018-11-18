---
title: Java IO
date: 2018-11-03 14:10:32
categories:[Java,Java基础]
tags:

top:
---

# 封装(Encapsulation )

隐藏内部细节

## 例子1:

`students.java`

```java
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

```

测试:

```java
package com.javatpoint;

class Test {
    public static void main(String[] args) {
        Student s = new Student();
        s.setName("vijay");
        System.out.println(s.getName());
    }
}

```

```bash
Compile By: javac -d . Test.java
Run By: java com.javatpoint.Test

Output: vijay
```

## 例子2:

`Account.java`

```java
class Account {
    //private data members  
    private long acc_no;
    private String name;
    private String email;
    private float amount;

    //public getter and setter methods  
    public long getAcc_no() {
        return acc_no;
    }

    public void setAcc_no(long acc_no) {
        this.acc_no = acc_no;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public float getAmount() {
        return amount;
    }

    public void setAmount(float amount) {
        this.amount = amount;
    }
}

```

`TestAccount.java`

```java
//A Java class to test the encapsulated class Account.  
public class TestEncapsulation {
    public static void main(String[] args) {
        //creating instance of Account class  
        Account acc = new Account();
        //setting values through setter methods  
        acc.setAcc_no(7560504000L);
        acc.setName("Sonoo Jaiswal");
        acc.setEmail("sonoojaiswal@javatpoint.com");
        acc.setAmount(500000f);
        //getting values through getter methods  
        System.out.println(acc.getAcc_no() + " " + acc.getName() + " " +
            acc.getEmail() + " " + acc.getAmount());
    }
}

```

`output`

```java
7560504000 Sonoo Jaiswal sonoojaiswal@javatpoint.com 500000.0
```



# 参考

https://stackify.com/oops-concepts-in-java/