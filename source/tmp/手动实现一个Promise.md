# Promise/A+规范

## 什么是Promise ?

> Promise是JS异步编程中的重要概念，异步抽象处理对象，是目前比较流行Javascript异步编程解决方案之一

## Promises/A+ 规范

> 为实现者提供一个健全的、可互操作的 JavaScript promise 的开放标准。

### 术语

- **解决 (fulfill)** : 指一个 promise 成功时进行的一系列操作，如状态的改变、回调的执行。虽然规范中用 fulfill 来表示解决，但在后世的 promise 实现多以 resolve 来指代之。
- **拒绝（reject)** : 指一个 promise 失败时进行的一系列操作。
- **拒因 (reason)** : 也就是拒绝原因，指在 promise 被拒绝时传递给拒绝回调的值。
- **终值（eventual value）** : 所谓终值，指的是 promise 被解决时传递给解决回调的值，由于 promise 有一次性的特征，因此当这个值被传递时，标志着 promise 等待态的结束，故称之终值，有时也直接简称为值（value）。
- **Promise** : promise 是一个拥有 then 方法的对象或函数，其行为符合本规范。
- **thenable** : 是一个定义了 then 方法的对象或函数，文中译作“拥有 then 方法”。
- **异常（exception）** : 是使用 throw 语句抛出的一个值。

### 基本要求

> 下面我们先来讲述Promise/A+ 规范的几个基本要求。

#### Promise 的状态

一个 Promise 的当前状态必须为以下三种状态中的一种：**等待态（Pending）**、**执行态（Fulfilled）**和**拒绝态（Rejected）**。

##### 等待态（Pending）

处于等待态时，promise 需满足以下条件：

- 可以迁移至执行态或拒绝态

##### 执行态（Fulfilled）

处于执行态时，promise 需满足以下条件：

- 不能迁移至其他任何状态
- 必须拥有一个**不可变**的终值

##### 拒绝态（Rejected）

处于拒绝态时，promise 需满足以下条件：

- 不能迁移至其他任何状态
- 必须拥有一个**不可变**的据因

这里的不可变指的是恒等（即可用 `===` 判断相等），而不是意味着更深层次的不可变（**译者注：**盖指当 value 或 reason 不是基本值时，只要求其引用地址相等，但属性值可被更改）。

#### 2. Then 方法

一个 promise 必须提供一个 then 方法以访问其当前值、终值和据因。

promise 的 then 方法接受两个参数：

```js
promise.then(onFulfilled, onRejected)
```

**参数可选**

onFulfilled 和 onRejected 都是可选参数。

- 如果 onFulfilled 不是函数，其必须被忽略
- 如果 onRejected 不是函数，其必须被忽略

**onFulfilled 特性**

如果 onFulfilled 是函数：

- 当 promise 执行结束后其必须被调用，其第一个参数为 promise 的终值
- 在 promise 执行结束前其不可被调用
- 其调用次数不可超过一次

**onRejected 特性**

如果 onRejected 是函数：

- 当 promise 被拒绝执行后其必须被调用，其第一个参数为 promise 的据因
- 在 promise 被拒绝执行前其不可被调用
- 其调用次数不可超过一次

**调用时机**

onFulfilled 和 onRejected 只有在执行环境堆栈仅包含平台代码时才可被调用 **注1**

**注1** 这里的平台代码指的是引擎、环境以及 promise 的实施代码。实践中要确保 onFulfilled 和 onRejected 方法异步执行，且应该在 then 方法被调用的那一轮事件循环之后的新执行栈中执行。

这个事件队列可以采用“宏任务（macro - task）”机制或者“微任务（micro - task）”机制来实现。

由于 promise 的实施代码本身就是平台代码（译者注：即都是 JavaScript），故代码自身在处理在处理程序时可能已经包含一个任务调度队列。

**调用要求**

onFulfilled 和 onRejected 必须被作为函数调用（即没有 this 值）

**多次调用**

then 方法可以被同一个 promise 调用多次

- 当 promise 成功执行时，所有 onFulfilled 需按照其注册顺序依次回调
- 当 promise 被拒绝执行时，所有的 onRejected 需按照其注册顺序依次回调

# 一步一步实现一个Promise





# 参考

[Promises/A+](https://promisesaplus.com/)

[【翻译】Promises/A+规范](http://www.ituring.com.cn/article/66566)