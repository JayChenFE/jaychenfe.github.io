---
title: SpringBoot启动引导-原理概述及包扫描
date: 2019-12-31 16:42:43
categories:
tags:
top:
---

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20191231161238.png)
[toc]

## 1.新建入门程序

使用IDEA的SpringInitializer 创建一个 SpringBoot 应用

pom文件我只引入了 `spring-boot-starter-web`：

```xml
<dependency>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-web</artifactId>
</dependency>
```

启动类如下
```java
@SpringBootApplication
public class Application {

    public static void main(String[] args) {
        SpringApplication.run(Application.class, args);
    }

}
```
尝试注释掉`@SpringBootApplication`注解并启动
```shell
org.springframework.context.ApplicationContextException: Unable to start web server; nested exception is org.springframework.context.ApplicationContextException: Unable to start ServletWebServerApplicationContext due to missing ServletWebServerFactory bean.
	at org.springframework.boot.web.servlet.context.ServletWebServerApplicationContext.onRefresh(ServletWebServerApplicationContext.java:156) ~[spring-boot-2.2.2.RELEASE.jar:2.2.2.RELEASE]
	...
```
因为没有 `ServletWebServerFactory`，而导致无法启动IOC容器。
那`@SpringBootApplication`是做什么的呢?

## 2.@SpringBootApplication

还原`@SpringBootApplication`注解

```java
/**
 * Indicates a {@link Configuration configuration} class that declares one or more
 * {@link Bean @Bean} methods and also triggers {@link EnableAutoConfiguration
 * auto-configuration} and {@link ComponentScan component scanning}. This is a convenience
 * annotation that is equivalent to declaring {@code @Configuration},
 * {@code @EnableAutoConfiguration} and {@code @ComponentScan}.
 *
 * @author Phillip Webb
 * @author Stephane Nicoll
 * @author Andy Wilkinson
 * @since 1.2.0
 
 标识了一个配置类，这个配置类上声明了一个或多个 @Bean 的方法，并且它会触发自动配置和组件扫描。
 这是一个很方便的注解，它等价于同时标注 @Configuration + @EnableAutoConfiguration + @ComponentScan 。
 */
@Target(ElementType.TYPE)
@Retention(RetentionPolicy.RUNTIME)
@Documented
@Inherited
@SpringBootConfiguration
@EnableAutoConfiguration
@ComponentScan(excludeFilters = { @Filter(type = FilterType.CUSTOM, classes = TypeExcludeFilter.class),
		@Filter(type = FilterType.CUSTOM, classes = AutoConfigurationExcludeFilter.class) })
public @interface SpringBootApplication
```

从SpringBoot1.2.0开始出现的，在 SpringBoot1.1及以前的版本，在启动类上标注的注解应该是三个：`@Configuration` +` @EnableAutoConfiguration` + `@ComponentScan`，只不过从1.2以后 SpringBoot 帮我们整合起来了,即 `@SpringBootApplication` = (默认属性)`@Configuration` + `@EnableAutoConfiguration` + `@ComponentScan`

> 注意`@SpringBootConfiguration`点开查看发现里面还是应用了`@Configuration`

文档注释已经描述的很详细：它是一个**组合注解**，包括3个注解。标注它之后就会触发自动配置（`@EnableAutoConfiguration`）和组件扫描（`@ComponentScan`）。

那这三个注解有什么作用呢?

## 3. @SpringBootConfiguration

```java
/**
 * Indicates that a class provides Spring Boot application
 * {@link Configuration @Configuration}. Can be used as an alternative to the Spring's
 * standard {@code @Configuration} annotation so that configuration can be found
 * automatically (for example in tests).
 标识一个类作为 SpringBoot 的配置类，
 它可以是Spring原生的 @Configuration 的一种替换方案，目的是这个配置可以被自动发现。
 * <p>
 * Application should only ever include <em>one</em> {@code @SpringBootConfiguration} and
 * most idiomatic Spring Boot applications will inherit it from
 * {@code @SpringBootApplication}.
 应用应当只在主启动类上标注 @SpringBootConfiguration，
 大多数情况下都是直接使用 @SpringBootApplication。
 *
 * @author Phillip Webb
 * @author Andy Wilkinson
 * @since 1.4.0
 */
@Target(ElementType.TYPE)
@Retention(RetentionPolicy.RUNTIME)
@Documented
@Configuration
public @interface SpringBootConfiguration
```

从文档注释以及它的声明上可以看出，它被 `@Configuration` 标注，说明它实际上是标注配置类的，而且是标注主启动类的。

### 3.1 @Configuration的作用

`@Configuration`是JavaConfig形式的Spring Ioc容器的配置类使用的那个`@Configuration`，SpringBoot社区推荐使用基于JavaConfig的配置形式，所以，这里的启动类标注了`@Configuration`之后，本身其实也是一个IoC容器的配置类。

例如:

#### 3.1.1表达形式层面

基于XML配置的方式是这样：

```xml
<?xml version="1.0" encoding="UTF-8"?>
<beans xmlns="http://www.springframework.org/schema/beans"
       xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
       xsi:schemaLocation="http://www.springframework.org/schema/beans http://www.springframework.org/schema/beans/spring-beans-3.0.xsd"
       default-lazy-init="true">
    <!--bean定义-->
</beans>
```

而基于JavaConfig的配置方式是这样：

```java
@Configuration
public class MockConfiguration{
    //bean定义
}
```

任何一个标注了@Configuration的Java类定义都是一个JavaConfig配置类。

#### 3.1.2注册bean定义层面

基于XML的配置形式是这样：

```xml
<bean id="mockService" class="..MockServiceImpl">
    ...
</bean>
```

而基于JavaConfig的配置形式是这样的：

```java
@Configuration
public class MockConfiguration{
    @Bean
    public MockService mockService(){
        return new MockServiceImpl();
    }
}
```

任何一个标注了@Bean的方法，其返回值将作为一个bean定义注册到Spring的IoC容器，方法名将默认成该bean定义的id。

#### 3.1.3表达依赖注入关系层面

为了表达bean与bean之间的依赖关系，在XML形式中一般是这样：

```xml
<bean id="mockService" class="..MockServiceImpl">
   <propery name ="dependencyService" ref="dependencyService" />
</bean>
<bean id="dependencyService" class="DependencyServiceImpl"></bean>
```

而基于JavaConfig的配置形式是这样的：

```java
@Configuration
public class MockConfiguration{
    @Bean
    public MockService mockService(){
        return new MockServiceImpl(dependencyService());
    }

    @Bean
    public DependencyService dependencyService(){
        return new DependencyServiceImpl();
    }
}
```

如果一个bean的定义依赖其他bean，则直接调用对应的JavaConfig类中依赖bean的创建方法就可以了。

@Configuration：提到@Configuration就要提到他的搭档@Bean。使用这两个注解就可以创建一个简单的spring配置类，可以用来替代相应的xml配置文件。

```xml
<beans>
    <bean id = "car" class="com.test.Car">
        <property name="wheel" ref = "wheel"></property>
    </bean>
    <bean id = "wheel" class="com.test.Wheel"></bean>
</beans>
```

相当于：

```java
@Configuration
public class Conf {
    @Bean
    public Car car() {
        Car car = new Car();
        car.setWheel(wheel());
        return car;
    }

    @Bean
    public Wheel wheel() {
        return new Wheel();
    }
}
```

#### 3.1.4验证

初始化一个IOC容器，并打印IOC容器中的所有bean的name：

```java
public class MainApp {
    public static void main(String[] args) throws Exception {
        AnnotationConfigApplicationContext ctx = new AnnotationConfigApplicationContext(ConfigurationDemo.class);
        String[] beanDefinitionNames = ctx.getBeanDefinitionNames();
        Stream.of(beanDefinitionNames).forEach(System.out::println);
    }
}
```

```java
org.springframework.context.annotation.internalConfigurationAnnotationProcessor
org.springframework.context.annotation.internalAutowiredAnnotationProcessor
org.springframework.context.annotation.internalCommonAnnotationProcessor
org.springframework.context.event.internalEventListenerProcessor
org.springframework.context.event.internalEventListenerFactory
Conf
wheel
car
```

可以发现组件，以及配置类本身被成功加载。

`@Configuration`的注解类标识这个类可以使用Spring IoC容器作为bean定义的来源。

`@Bean`注解告诉Spring，一个带有@Bean的注解方法将返回一个对象，该对象应该被注册为在Spring应用程序上下文中的bean。

### 3.2 @SpringBootConfiguration的附加作用

借助IDEA搜索 `@SpringBootConfiguration` 的出现位置，发现除了 `@SpringBootApplication` 外，只有一个位置使用了它：

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20200101151346.png)

发现是一个测试包中的usage（默认的 `SpringInitializer` 会把 `spring-boot-starter-test` 一起带进来，故可以搜到这个usage。如果小伙伴手动使用Maven创建 SpringBoot 应用且没有导入 `spring-boot-start-test` 依赖，则这个usage都不会搜到）。

翻看 SpringBoot 的官方文档，发现通篇只有两个位置提到了 `@SpringBootConfiguration`，还真有一个跟测试相关：

https://docs.spring.io/spring-boot/docs/2.2.2.RELEASE/reference/htmlsingle/#boot-features-testing-spring-boot-applications-detecting-config

第三段中有对 `@SpringBootConfiguration` 的描述：

>The search algorithm works up from the package that contains the test until it finds a class annotated with `@SpringBootApplication` or `@SpringBootConfiguration`. As long as you structured your code in a sensible way, your main configuration is usually found.
>
>搜索算法从包含测试的程序包开始工作，直到找到带有 `@SpringBootApplication` 或 `@SpringBootConfiguration` 标注的类。只要您以合理的方式对代码进行结构化，通常就可以找到您的主要配置。

这很明显是解释了 SpringBoot 主启动类与测试的关系，标注 `@SpringBootApplication` 或 `@SpringBootConfiguration` 的主启动类会被 Spring测试框架 的搜索算法找到。回过头看上面的截图，引用 `@SpringBootConfiguration` 的方法恰好叫 **getOrFindConfigurationClasses**，与文档一致。

## 4. @ComponentScan

`@ComponentScan`这个注解在Spring中很重要，它对应XML配置中的元素，`@ComponentScan`的功能其实就是自动扫描并加载符合条件的组件（比如`@Component`和@`Repository`等）或者bean定义，最终将这些bean定义加载到IoC容器中。

我们可以通过basePackages等属性来细粒度的定制`@ComponentScan`自动扫描的范围，如果不指定，则默认Spring框架实现会从声明`@ComponentScan`所在类的package进行扫描。

> 注：所以SpringBoot的启动类最好是放在root package下，因为默认不指定basePackages。

不过在上面的声明中有显式的指定了两个过滤条件：

```java
@ComponentScan(excludeFilters = { @Filter(type = FilterType.CUSTOM, classes = TypeExcludeFilter.class),
		@Filter(type = FilterType.CUSTOM, classes = AutoConfigurationExcludeFilter.class) })
```

这两个过滤器又是做什么的呢

### 4.1 TypeExcludeFilter

文档注释原文翻译：

> Provides exclusion TypeFilters that are loaded from the BeanFactory and automatically applied to SpringBootApplication scanning. Can also be used directly with @ComponentScan as follows:
>
> 提供从 BeanFactory 加载并自动应用于 `@SpringBootApplication` 扫描的排除 `TypeFilter` 。
>
> ```java
>  @ComponentScan(excludeFilters = @Filter(type = FilterType.CUSTOM, classes = TypeExcludeFilter.class))
> ```
>
> Implementations should provide a subclass registered with BeanFactory and override the match(MetadataReader, MetadataReaderFactory) method. They should also implement a valid hashCode and equals methods so that they can be used as part of Spring test's application context caches. Note that TypeExcludeFilters are initialized very early in the application lifecycle, they should generally not have dependencies on any other beans. They are primarily used internally to support spring-boot-test.
>
> 实现应提供一个向 BeanFactory 注册的子类，并重写 `match(MetadataReader, MetadataReaderFactory)` 方法。它们还应该实现一个有效的 `hashCode` 和 `equals` 方法，以便可以将它们用作Spring测试的应用程序上下文缓存的一部分。
>
> 注意，`TypeExcludeFilters` 在应用程序生命周期的很早就初始化了，它们通常不应该依赖于任何其他bean。它们主要在内部用于支持 `spring-boot-test` 。

从文档注释中大概能看出来，它是给了一种扩展机制，能让我们**向IOC容器中注册一些自定义的组件过滤器，以在包扫描的过程中过滤它们**。

这种Filter的核心方法是 `match` 方法，它实现了过滤的判断逻辑：

```java
@Override
public boolean match(MetadataReader metadataReader, MetadataReaderFactory metadataReaderFactory)
		throws IOException {
	if (this.beanFactory instanceof ListableBeanFactory && getClass() == TypeExcludeFilter.class) {
		for (TypeExcludeFilter delegate : getDelegates()) {
			if (delegate.match(metadataReader, metadataReaderFactory)) {
				return true;
			}
		}
	}
	return false;
}
private Collection<TypeExcludeFilter> getDelegates() {
	Collection<TypeExcludeFilter> delegates = this.delegates;
	if (delegates == null) {
		delegates = ((ListableBeanFactory) this.beanFactory).getBeansOfType(TypeExcludeFilter.class).values();
		this.delegates = delegates;
	}
	return delegates;
}
```

注意看if结构体中的第一句，它会从 `BeanFactory` （可以暂时理解成IOC容器）中获取所有类型为 `TypeExcludeFilter` 的组件，去执行自定义的过滤方法。

由此可见，`TypeExcludeFilter` 的作用是做**扩展的组件过滤**。

### 4.2 AutoConfigurationExcludeFilter

看这个类名，总感觉跟自动配置相关，还是看一眼它的源码：

```java
public boolean match(MetadataReader metadataReader, MetadataReaderFactory metadataReaderFactory)
        throws IOException {
    return isConfiguration(metadataReader) && isAutoConfiguration(metadataReader);
}

private boolean isConfiguration(MetadataReader metadataReader) {
    return metadataReader.getAnnotationMetadata().isAnnotated(Configuration.class.getName());
}

private boolean isAutoConfiguration(MetadataReader metadataReader) {
    return getAutoConfigurations().contains(metadataReader.getClassMetadata().getClassName());
}

protected List<String> getAutoConfigurations() {
    if (this.autoConfigurations == null) {
        this.autoConfigurations = SpringFactoriesLoader.loadFactoryNames(EnableAutoConfiguration.class,
                this.beanClassLoader);
    }
    return this.autoConfigurations;
}
```

它的 `match` 方法要判断两个部分：**是否是一个配置类，是否是一个自动配置类**。其实光从方法名上也就看出来了，下面的方法是其调用实现，里面有一个很关键的机制：`SpringFactoriesLoader.loadFactoryNames`，我们留到下面解释。

## 小结

1. `@SpringBootApplication` 是组合注解。
2. `@ComponentScan` 中的 `exclude` 属性会将主启动类、自动配置类屏蔽掉。
3. `@Configuration` 可标注配置类，`@SpringBootConfiguration` 并没有对其做实质性扩展。

`@EnableAutoConfiguration` 的作用篇幅较长，单独成篇。