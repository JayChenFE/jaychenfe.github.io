---
title: SpringBoot启动引导-SpringFramework的手动装配
date: 2019-12-31 16:42:43
categories:
tags:
top:
---

[toc]

在了解 `@EnableAutoConfiguration` 之前，先了解 SpringFramework 的原生手动装配机制，这对后续阅读 `@EnableAutoConfiguration` 有很大帮助。

## 5.SpringFramework的手动装配

在原生的 SpringFramework 中，装配组件有三种方式：

- 使用模式注解 `@Component` 等（Spring2.5+）
- 使用配置类 `@Configuration` 与 `@Bean` （Spring3.0+）
- 使用模块装配 `@EnableXXX` 与 `@Import` （Spring3.1+）

其中使用 `@Component` 及衍生注解很常见

但`@Component`注解只能在自己编写的代码中标注，无法装配jar包中的组件。为此可以使用 `@Configuration` 与 `@Bean`，手动装配组件（如上面的 `@Configuration` 示例）。

但这种方式一旦注册过多，会导致编码成本高，维护不灵活等问题。

SpringFramework 提供了模块装配功能，通过给配置类标注 `@EnableXXX` 注解，再在注解上标注 `@Import` 注解，即可完成组件装配的效果。

下面介绍模块装配的使用方式。

### 5.1 @EnableXXX与@Import的使用

创建几个颜色的实体类，如Red，Yellow，Blue，Green，Black等。

新建 **@EnableColor** 注解，并声明 `@Import`。**（注意注解上有三个必须声明的元注解）**

```java
@Documented
@Retention(RetentionPolicy.RUNTIME)
@Target(ElementType.TYPE)
public @interface EnableColor {
    
}
```

`@Import` 可以传入四种类型：普通类、配置类、`ImportSelector` 的实现类，`ImportBeanDefinitionRegistrar` 的实现类。具体如文档注释中描述：

```java
public @interface Import {

	/**
	 * {@link Configuration @Configuration}, {@link ImportSelector},
	 * {@link ImportBeanDefinitionRegistrar}, or regular component classes to import.
	 */
	Class<?>[] value();

}
```

value中写的很明白了，可以导入**配置类**、**`ImportSelector` 的实现类**，**`ImportBeanDefinitionRegistrar` 的实现类**，或者**普通类**。

下面介绍 `@Import` 的用法。

#### 5.1.1 导入普通类

直接在 `@Import` 注解中标注Red类：

```java
@Import({Red.class})
public @interface EnableColor {
    
}
```

之后启动类标注 **@EnableColor**，引导启动IOC容器：

```java
@EnableColor
@Configuration
public class ColorConfiguration {
    
}

public class App {
    public static void main(String[] args) throws Exception {
        AnnotationConfigApplicationContext ctx = new AnnotationConfigApplicationContext(ColorConfiguration.class);
        String[] beanDefinitionNames = ctx.getBeanDefinitionNames();
        Stream.of(beanDefinitionNames).forEach(System.out::println);
    }
}
```

控制台打印：

```shell
org.springframework.context.annotation.internalConfigurationAnnotationProcessor
org.springframework.context.annotation.internalAutowiredAnnotationProcessor
org.springframework.context.annotation.internalCommonAnnotationProcessor
org.springframework.context.event.internalEventListenerProcessor
org.springframework.context.event.internalEventListenerFactory
colorConfiguration
com.example.demo.enablexxx.Red
```

可见Red类已经被注册。

#### 5.1.2 导入配置类

新建 **ColorRegistrarConfiguration**，并标注 `@Configuration` ：

```java
@Configuration
public class ColorRegistrarConfiguration {
    
    @Bean
    public Yellow yellow() {
        return new Yellow();
    }
    
}
```

之后在 **@EnableColor** 的 `@Import` 注解中加入 **ColorRegistrarConfiguration**：

```java
@Import({Red.class, ColorRegistrarConfiguration.class})
public @interface EnableColor {
    
}
```

重新启动IOC容器，打印结果：

```shell
org.springframework.context.annotation.internalConfigurationAnnotationProcessor
org.springframework.context.annotation.internalAutowiredAnnotationProcessor
org.springframework.context.annotation.internalCommonAnnotationProcessor
org.springframework.context.event.internalEventListenerProcessor
org.springframework.context.event.internalEventListenerFactory
colorConfiguration
com.example.demo.enablexxx.Red
com.example.demo.enablexxx.ColorRegistrarConfiguration
yellow
```

可见配置类 ColorRegistrarConfiguration 和 Yellow 都已注册到IOC容器中。

#### 5.1.3 导入ImportSelector

新建 **ColorImportSelector**，实现 `ImportSelector` 接口：

```java
public class ColorImportSelector implements ImportSelector {
    
    @Override
    public String[] selectImports(AnnotationMetadata importingClassMetadata) {
        return new String[] {Blue.class.getName(), Green.class.getName()};
    }
    
}
```

之后在 **@EnableColor** 的 `@Import` 注解中加入 **ColorImportSelector**：

```java
@Import({Red.class, ColorRegistrarConfiguration.class, ColorImportSelector.class})
public @interface EnableColor {
    
}
```

重新启动IOC容器，打印结果：

```shell
org.springframework.context.annotation.internalConfigurationAnnotationProcessor
org.springframework.context.annotation.internalAutowiredAnnotationProcessor
org.springframework.context.annotation.internalCommonAnnotationProcessor
org.springframework.context.event.internalEventListenerProcessor
org.springframework.context.event.internalEventListenerFactory
colorConfiguration
com.example.demo.enablexxx.Red
com.example.demo.enablexxx.ColorRegistrarConfiguration
yellow
com.example.demo.enablexxx.Blue
com.example.demo.enablexxx.Green
```

**ColorImportSelector** 没有注册到IOC容器中，两个新的颜色类被注册。

#### 5.1.4 导入ImportBeanDefinitionRegistrar

新建 **ColorImportBeanDefinitionRegistrar**，实现 `ImportBeanDefinitionRegistrar` 接口：

```java
public class ColorImportBeanDefinitionRegistrar implements ImportBeanDefinitionRegistrar {
    
    @Override
    public void registerBeanDefinitions(AnnotationMetadata importingClassMetadata, BeanDefinitionRegistry registry) {
        registry.registerBeanDefinition("black", new RootBeanDefinition(Black.class));
    }
    
}
```

之后在 **@EnableColor** 的 `@Import` 注解中加入 **ColorImportBeanDefinitionRegistrar**：

```java
@Import({Red.class, ColorRegistrarConfiguration.class, ColorImportSelector.class, ColorImportBeanDefinitionRegistrar.class})
public @interface EnableColor {
    
}
```

重新启动IOC容器，打印结果：

```shell
org.springframework.context.annotation.internalConfigurationAnnotationProcessor
org.springframework.context.annotation.internalAutowiredAnnotationProcessor
org.springframework.context.annotation.internalCommonAnnotationProcessor
org.springframework.context.event.internalEventListenerProcessor
org.springframework.context.event.internalEventListenerFactory
colorConfiguration
com.example.demo.enablexxx.Red
com.example.demo.enablexxx.ColorRegistrarConfiguration
yellow
com.example.demo.enablexxx.Blue
com.example.demo.enablexxx.Green
black
```

由于在注册Black的时候要指定Bean的id，而上面已经标明了使用 "black" 作为id，故打印的 beanDefinitionName 就是black。