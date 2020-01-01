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