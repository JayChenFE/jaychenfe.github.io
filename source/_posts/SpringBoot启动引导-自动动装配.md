---
title: SpringBoot启动引导-自动动装配
date: 2020-01-02 17:34:53
categories:
tags:
top:
---

[toc]

## 6. SpringBoot的自动装配

SpringBoot的自动配置完全由 `@EnableAutoConfiguration` 开启。

`@EnableAutoConfiguration` 的内容：

```java
@Target(ElementType.TYPE)
@Retention(RetentionPolicy.RUNTIME)
@Documented
@Inherited
@AutoConfigurationPackage
@Import(AutoConfigurationImportSelector.class)
public @interface EnableAutoConfiguration 
```

文档注释原文翻译：

> Enable auto-configuration of the Spring Application Context, attempting to guess and configure beans that you are likely to need. Auto-configuration classes are usually applied based on your classpath and what beans you have defined. For example, if you have `tomcat-embedded.jar` on your classpath you are likely to want a `TomcatServletWebServerFactory` (unless you have defined your own `ServletWebServerFactory` bean).
>
> 启用Spring-ApplicationContext的自动配置，并且会尝试猜测和配置您可能需要的Bean。通常根据您的类路径和定义的Bean来应用自动配置类。例如，如果您的类路径上有 `tomcat-embedded.jar`，则可能需要 `TomcatServletWebServerFactory` （除非自己已经定义了 `ServletWebServerFactory` 的Bean）。
>
> When using `SpringBootApplication`, the auto-configuration of the context is automatically enabled and adding this annotation has therefore no additional effect.
>
> 使用 `@SpringBootApplication` 时，将自动启用上下文的自动配置，因此再添加该注解不会产生任何其他影响。
>
> Auto-configuration tries to be as intelligent as possible and will back-away as you define more of your own configuration. You can always manually `exclude()` any configuration that you never want to apply (use `excludeName()` if you don't have access to them). You can also exclude them via the `spring.autoconfigure.exclude` property. Auto-configuration is always applied after user-defined beans have been registered.
>
> 自动配置会尝试尽可能地智能化，并且在您定义更多自定义配置时会自动退出（被覆盖）。您始终可以手动排除掉任何您不想应用的配置（如果您无法访问它们，请使用 `excludeName()` 方法），您也可以通过 `spring.autoconfigure.exclude` 属性排除它们。自动配置始终在注册用户自定义的Bean之后应用。
>
> The package of the class that is annotated with `@EnableAutoConfiguration`, usually via `@SpringBootApplication`, has specific significance and is often used as a 'default'. For example, it will be used when scanning for `@Entity` classes. It is generally recommended that you place `@EnableAutoConfiguration` (if you're not using `@SpringBootApplication`) in a root package so that all sub-packages and classes can be searched.
>
> 通常被 `@EnableAutoConfiguration` 标注的类（如 `@SpringBootApplication`）的包具有特定的意义，通常被用作“默认值”。例如，在扫描@Entity类时将使用它。通常建议您将 `@EnableAutoConfiguration`（如果您未使用 `@SpringBootApplication`）放在根包中，以便可以搜索所有包及子包下的类。
>
> Auto-configuration classes are regular Spring `Configuration` beans. They are located using the `SpringFactoriesLoader` mechanism (keyed against this class). Generally auto-configuration beans are `@Conditional` beans (most often using `@ConditionalOnClass` and `@ConditionalOnMissingBean` annotations).
>
> 自动配置类也是常规的Spring配置类。它们使用 `SpringFactoriesLoader` 机制定位（针对此类）。通常自动配置类也是 `@Conditional` Bean（最经常的情况下是使用 `@ConditionalOnClass` 和 `@ConditionalOnMissingBean` 标注）。

`@EnableAutoConfiguration` 也是一个组合注解,分开来看

### 6.1 @AutoConfigurationPackage

```java
/**
 * Indicates that the package containing the annotated class should be registered with
 * {@link AutoConfigurationPackages}.
 *
 表示包含该注解的类所在的包应该在 AutoConfigurationPackages 中注册。
 * @author Phillip Webb
 * @since 1.3.0
 * @see AutoConfigurationPackages
 */
@Target(ElementType.TYPE)
@Retention(RetentionPolicy.RUNTIME)
@Documented
@Inherited
@Import(AutoConfigurationPackages.Registrar.class)
public @interface AutoConfigurationPackage 
```

它的实现原理是在注解上标注了 `@Import`，导入了一个 `AutoConfigurationPackages.Registrar` 。

#### 6.1.1 AutoConfigurationPackages.Registrar

```java
/**
 * {@link ImportBeanDefinitionRegistrar} to store the base package from the importing
 ImportBeanDefinitionRegistrar 用于保存导入的配置类所在的根包
 * configuration.
 */
static class Registrar implements ImportBeanDefinitionRegistrar, DeterminableImports {
	@Override
	public void registerBeanDefinitions(AnnotationMetadata metadata, BeanDefinitionRegistry registry) {
		register(registry, new PackageImport(metadata).getPackageName());
	}
	@Override
	public Set<Object> determineImports(AnnotationMetadata metadata) {
		return Collections.singleton(new PackageImport(metadata));
	}
}
```

它就是实现把主配置所在根包保存起来以便后期扫描用的。分析源码：

`Registrar` 实现了 `ImportBeanDefinitionRegistrar` 接口，它向IOC容器中要手动注册组件。

在重写的 `registerBeanDefinitions` 方法中，它要调用外部类 `AutoConfigurationPackages` 的register方法。

看传入的参数：**new PackageImport(metadata).getPackageName()**,它实例化的 `PackageImport` 对象的构造方法：

```java
PackageImport(AnnotationMetadata metadata) {
    this.packageName = ClassUtils.getPackageName(metadata.getClassName());
}
```

它取了一个 metadata 的所在包名。那 metadata 又是什么呢？

翻看 `ImportBeanDefinitionRegistrar`的文档注释：

```java
public interface ImportBeanDefinitionRegistrar {
    /**
     * ......
     * @param importingClassMetadata annotation metadata of the importing class
     * @param registry current bean definition registry
     */
    void registerBeanDefinitions(AnnotationMetadata importingClassMetadata, BeanDefinitionRegistry registry);
}
```

注意 **importingClassMetadata** 的参数说明：**导入类的注解元数据**。

它实际代表的是被 `@Import` 标记的类的信息。

那在 SpringBoot 的主启动类中，被标记的肯定就是最开始案例里的 `DemoApplication`。

也就是说它是 `DemoApplication` 的类信息，那获取它的包名就是获取主启动类的所在包。

拿到这个包有什么意义呢？不清楚，那就回到那个 `Registrar` 中，看它调用的 register 方法都干了什么：

#### 6.1.2 register方法

```java
private static final String BEAN = AutoConfigurationPackages.class.getName();

public static void register(BeanDefinitionRegistry registry, String... packageNames) {
    // 判断 BeanFactory 中是否包含 AutoConfigurationPackages
    if (registry.containsBeanDefinition(BEAN)) {
        BeanDefinition beanDefinition = registry.getBeanDefinition(BEAN);
        ConstructorArgumentValues constructorArguments = beanDefinition.getConstructorArgumentValues();
        // addBasePackages：添加根包扫描包
        constructorArguments.addIndexedArgumentValue(0, addBasePackages(constructorArguments, packageNames));
    }
    else {
        GenericBeanDefinition beanDefinition = new GenericBeanDefinition();
        beanDefinition.setBeanClass(BasePackages.class);
        beanDefinition.getConstructorArgumentValues().addIndexedArgumentValue(0, packageNames);
        beanDefinition.setRole(BeanDefinition.ROLE_INFRASTRUCTURE);
        registry.registerBeanDefinition(BEAN, beanDefinition);
    }
}
```

划重点：它要判断当前IOC容器中是否包含 `AutoConfigurationPackages` 。如果有，就会拿到刚才传入的包名，设置到一个 **basePackage** 里面！basePackage 的意义很明显是根包。

换句话说，它要**取主启动类所在包及子包下的组件**。

到这里，就呼应了文档注释中的描述，也解释了为什么 **SpringBoot 的启动器一定要在所有类的最外层**。