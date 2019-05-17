---
title: [译]Distributed systems for fun and profit_3时间和顺序
date: 2019-04-23 17:32:04
categories: [微服务理论文章阅读学习]
tags:
top:
---

# 三.Time and order 时间和顺序

What is order and why is it important?

What do you mean "what is order"?

I mean, why are we so obsessed with order in the first place? Why do we care whether A happened before B? Why don't we care about some other property, like "color"?

Well, my crazy friend, let's go back to the definition of distributed systems to answer that.

As you may remember, I described distributed programming as the art of solving the same problem that you can solve on a single computer using multiple computers.

This is, in fact, at the core of the obsession with order. Any system that can only do one thing at a time will create a total order of operations. Like people passing through a single door, every operation will have a well-defined predecessor and successor. That's basically the programming model that we've worked very hard to preserve.

分布式系统的目标是能够让多台机器像单机一样处理问题。在这里面需要关注的核心是**顺序**。任何系统在同一时刻都只能做一件事情，这样就会产生一系列有顺序的操作。

The traditional model is: a single program, one process, one memory space running on one CPU. The operating system abstracts away the fact that there might be multiple CPUs and multiple programs, and that the memory on the computer is actually shared among many programs. I'm not saying that threaded programming and event-oriented programming don't exist; it's just that they are special abstractions on top of the "one/one/one" model. Programs are written to be executed in an ordered fashion: you start from the top, and then go down towards the bottom.

传统的模型是：单一程序利用一个CPU运行在一个进程和一个内存空间中。而分布式模型通常处理多个CPU和多个程序以及程序之间的内存通常需要共享。这个时候即使程序运行在多台机器之间，我们希望它也能像单机一样，同样顺序执行程序

Order as a property has received so much attention because the easiest way to define "correctness" is to say "it works like it would on a single machine". And that usually means that a) we run the same operations and b) that we run them in the same order - even if there are multiple machines.

The nice thing about distributed systems that preserve order (as defined for a single system) is that they are generic. You don't need to care about what the operations are, because they will be executed exactly like on a single machine. This is great because you know that you can use the same system no matter what the operations are.

In reality, a distributed program runs on multiple nodes; with multiple CPUs and multiple streams of operations coming in. You can still assign a total order, but it requires either accurate clocks or some form of communication. You could timestamp each operation using a completely accurate clock then use that to figure out the total order. Or you might have some kind of communication system that makes it possible to assign sequential numbers as in a total order.

分布式系统的好处在于一个定义好的顺序是通用的。你不需要担心它是如何操作的，无论你进行什么样的操作，你都可以使用这个系统。

分布式系统程序运行在多个节点之间。你需要分配一个全局顺序，需要精确的时钟与一些通信方式。你可以对每一个操作贴上一个时间戳，来保证它们全局有序，或者也可以采用某种通信方式（例如计数器），来确保它们全局有序

操作顺序包括：全局有序、偏序

## Total and partial order  全局有序和偏序

The natural state in a distributed system is [partial order](http://en.wikipedia.org/wiki/Partially_ordered_set). Neither the network nor independent nodes make any guarantees about relative order; but at each node, you can observe a local order.

A [total order](http://en.wikipedia.org/wiki/Total_order) is a binary relation that defines an order for every element in some set.

- **全局有序：数据集中每一个元素顺序的一种二进制关系**
- **偏序：分布式系统中最自然的状态就是偏序。独立节点与网络不能保证相关顺序，但在每个节点中，自身有本地顺序**

Two distinct elements are **comparable** when one of them is greater than the other. In a partially ordered set, some pairs of elements are not comparable and hence a partial order doesn't specify the exact order of every item.

两个明确的元素之间能够进行相互大小比较，但是在一个分区有序数据集中，不同区的元素之间是没法进行比较的，因为它们只在自己的分区中有序

Both total order and partial order are [transitive](http://en.wikipedia.org/wiki/Transitive_relation) and [antisymmetric](http://en.wikipedia.org/wiki/Antisymmetric_relation). The following statements hold in both a total order and a partial order for all a, b and c in X:

无论是全局有序还是偏序，都遵从传递性（transitive）和反对称性（antisymmetric）。下面的描述表达了顺序具有的性质：

```
If a ≤ b and b ≤ a then a = b (antisymmetry);
If a ≤ b and b ≤ c then a ≤ c (transitivity);
```

However, a total order is [total](http://en.wikipedia.org/wiki/Total_relation):

```
a ≤ b or b ≤ a (totality) for all a, b in X
```

while a partial order is only [reflexive](http://en.wikipedia.org/wiki/Reflexive_relation):

```
a ≤ a (reflexivity) for all a in X
```

Note that totality implies reflexivity; so a partial order is a weaker variant of total order.
For some elements in a partial order, the totality property does not hold - in other words, some of the elements are not comparable.

偏序（分区有序？可以这样理解吗）的性质比全局顺序的性质要弱，因为有些元素他们是没法进行比较的

Git branches are an example of a partial order. As you probably know, the git revision control system allows you to create multiple branches from a single base branch - e.g. from a master branch. Each branch represents a history of source code changes derived based on a common ancestor:

**Git分支就是偏序的一个实例**。我们都知道git的版本控制能够让你从一个单一的基本分支中创造出多个分支。例如，从主分支中进行后续创建。每一个分支代表原始代码从最初到后面的变化历程：

```
[ branch A (1,2,0)]  [ master (3,0,0) ]  [ branch B (1,0,2) ]
[ branch A (1,1,0)]  [ master (2,0,0) ]  [ branch B (1,0,1) ]
                  \  [ master (1,0,0) ]  /
```

The branches A and B were derived from a common ancestor, but there is no definite order between them: they represent different histories and cannot be reduced to a single linear history without additional work (merging). You could, of course, put all the commits in some arbitrary order (say, sorting them first by ancestry and then breaking ties by sorting A before B or B before A) - but that would lose information by forcing a total order where none existed.

这里分支A和B都来自于同一个祖先，但是它们两者之间的顺序是没有定义的：两者表示的是不同的历史版本，并且如若不经过其它额外操作，类似于merging，是无法将它们两者归到同一线性改变的版本中。当然你可以自己进行一些操作，定义提交的顺序，但如果自定义AB之间的顺序，会强制出现一个本来就不存在的total order

In a system consisting of one node, a total order emerges by necessity: instructions are executed and messages are processed in a specific, observable order in a single program. We've come to rely on this total order - it makes executions of programs predictable. This order can be maintained on a distributed system, but at a cost: communication is expensive, and time synchronization is difficult and fragile.

在一个有单一节点构成的系统中，全局有序是必要的，这使得程序的执行结果具有可预测性。这样的顺序也能在分布式系统中维持，但昂贵的通信成本以及时间同步的困难和脆弱性使得其代价十分昂贵

# What is time? 时间

Time is a source of order - it allows us to define the order of operations - which coincidentally also has an interpretation that people can understand (a second, a minute, a day and so on).

没有时间就没有顺序。时间能让我们确定操作的顺序，同时也能被更好的理解

In some sense, time is just like any other integer counter. It just happens to be important enough that most computers have a dedicated time sensor, also known as a clock. It's so important that we've figured out how to synthesize an approximation of the same counter using some imperfect physical system (from wax candles to cesium atoms). By "synthesize", I mean that we can approximate the value of the integer counter in physically distant places via some physical property without communicating it directly.

从某种角度而言，时间像计数器一样，能够让大多数的运算有自己专用的时间传感器。对于同步系统而言，我们能有一个不用互相通信就存在的精确的计数器

Timestamps really are a shorthand value for representing the state of the world from the start of the universe to the current moment - if something occurred at a particular timestamp, then it was potentially influenced by everything that happened before it. This idea can be generalized into a causal clock that explicitly tracks causes (dependencies) rather than simply assuming that everything that preceded a timestamp was relevant. Of course, the usual assumption is that we should only worry about the state of the specific system rather than the whole world.

时间戳是表达事物所处状态的一个简单的速记值。如果一件事发生在某一时刻，那么它受到发生在它之前的事件的影响。这个想法可以概括为一个因果时钟，它明确地跟踪原因（依赖性），而不是简单地假设时间戳之前的所有内容都是相关的。当然，通常的假设是，我们只应该担心特定系统的状态，而不是整个世界。

Assuming that time progresses at the same rate everywhere - and that is a big assumption which I'll return to in a moment - time and timestamps have several useful interpretations when used in a program. The three interpretations are:

- Order
- Duration
- Interpretation

假设时间在任何地方都以相同的速度进行——这是一个很大的假设，我稍后将回到这个假设——时间和时间戳在程序中使用时有几个有用的解释。这三种解释是：

- **顺序**
- **持续时间**
- **表现形式**

*Order*. When I say that time is a source of order, what I mean is that:

- we can attach timestamps to unordered events to order them
- we can use timestamps to enforce a specific ordering of operations or the delivery of messages (for example, by delaying an operation if it arrives out of order)
- we can use the value of a timestamp to determine whether something happened chronologically before something else

**顺序**：一系列事件发生的顺序

- 我们能通过事件发生的时间戳来确定事件发生的顺序
- 我们能够利用时间戳来定义一系列操作的顺序，或者传递信息（例如，如果一个操作发生故障，则延迟它）
- 通过时间戳能够确定一件事是否发生在另一件事前

*Interpretation* - time as a universally comparable value. The absolute value of a timestamp can be interpreted as a date, which is useful for people. Given a timestamp of when a downtime started from a log file, you can tell that it was last Saturday, when there was a [thunderstorm](https://twitter.com/AWSFail/statuses/218915147060752384).

**表现形式**：时间是一个可以进行全局比较的值。时间的表现形式可以有多种，比如日期、星期几等等

*Duration* - durations measured in time have some relation to the real world. Algorithms generally don't care about the absolute value of a clock or its interpretation as a date, but they might use durations to make some judgment calls. In particular, the amount of time spent waiting can provide clues about whether a system is partitioned or merely experiencing high latency.

**持续时间**：通过时间段长短可以来判断系统是发生分区还是发生了高延迟

By their nature, the components of distributed systems do not behave in a predictable manner. They do not guarantee any specific order, rate of advance, or lack of delay. Each node does have some local order - as execution is (roughly) sequential - but these local orders are independent of each other.

Imposing (or assuming) order is one way to reduce the space of possible executions and possible occurrences. Humans have a hard time reasoning about things when things can happen in any order - there just are too many permutations to consider.

强制（或假定）顺序是减少可能执行和发生的空间的一种方法。当事情可以以任何顺序发生时，有太多的排列需要考虑，因而很难对事情进行推理。

## Does time progress at the same rate everywhere?各个分布式节点中时间相同吗？

We all have an intuitive concept of time based on our own experience as individuals. Unfortunately, that intuitive notion of time makes it easier to picture total order rather than partial order. It's easier to picture a sequence in which things happen one after another, rather than concurrently. It is easier to reason about a single order of messages than to reason about messages arriving in different orders and with different delays.

依据个人经验，我们都有一个直观的时间概念。但直观的时间概念使我们更容易描绘出总顺序而不是部分顺序。更容易想象事情发生的顺序，一个接一个，而不是同时发生。对一个消息顺序进行推理要比对以不同顺序和不同延迟到达的消息进行推理容易得多。（**通常一串连续发生的事件比同时发生要更容易理解**）

However, when implementing distributing systems we want to avoid making strong assumptions about time and order, because the stronger the assumptions, the more fragile a system is to issues with the "time sensor" - or the onboard clock. Furthermore, imposing an order carries a cost. The more temporal nondeterminism that we can tolerate, the more we can take advantage of distributed computation.

然而，在实施分布式系统时，我们希望避免对时间和顺序做出强有力的假设，因为假设越强，系统就越容易受到“时间传感器”或时钟的问题的影响。此外，执行命令也会带来成本。我们越能容忍时间上的不确定性，就越能利用分布式计算。（**分布式系统中关于“时间传感器”的定义和假设相较而言没那么刻板。对时间的不确定性容忍度约稿，对于系统的分布式计算更有利**）

There are three common answers to the question "does time progress at the same rate everywhere?". These are:

- "Global clock": yes
- "Local clock": no, but
- "No clock": no!

对于分布式节点中的时间是否同步，使用不同的时钟假设是不同的：

- **全局时钟：相同 （同步系统模型）**
- **本地时钟：不同，但是本地有序 （部分同步系统模型）**
- **不使用时钟：不同 （异步系统模型，用逻辑时钟来确定顺序）**

These correspond roughly to the three timing assumptions that I mentioned in the second chapter: the synchronous system model has a global clock, the partially synchronous model has a local clock, and in the asynchronous system model one cannot use clocks at all. Let's look at these in more detail.

### Time with a "global-clock" assumption 全局时钟

The global clock assumption is that there is a global clock of perfect accuracy, and that everyone has access to that clock. This is the way we tend to think about time, because in human interactions small differences in time don't really matter.

全局时钟：有一个全局精确的时钟，并且每个节点都能接触到

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190425230612.png)

The global clock is basically a source of total order (exact order of every operation on all nodes even if those nodes have never communicated).

However, this is an idealized view of the world: in reality, clock synchronization is only possible to a limited degree of accuracy. This is limited by the lack of accuracy of clocks in commodity computers, by latency if a clock synchronization protocol such as [NTP](http://en.wikipedia.org/wiki/Network_Time_Protocol) is used and fundamentally by [the nature of spacetime](http://en.wikipedia.org/wiki/Time_dilation).

Assuming that clocks on distributed nodes are perfectly synchronized means assuming that clocks start at the same value and never drift apart. It's a nice assumption because you can use timestamps freely to determine a global total order - bound by clock drift rather than latency - but this is a [nontrivial](http://queue.acm.org/detail.cfm?id=1773943) operational challenge and a potential source of anomalies. There are many different scenarios where a simple failure - such as a user accidentally changing the local time on a machine, or an out-of-date machine joining a cluster, or synchronized clocks drifting at slightly different rates and so on that can cause hard-to-trace anomalies.

Nevertheless, there are some real-world systems that make this assumption. Facebook's [Cassandra](http://en.wikipedia.org/wiki/Apache_Cassandra) is an example of a system that assumes clocks are synchronized. It uses timestamps to resolve conflicts between writes - the write with the newer timestamp wins. This means that if clocks drift, new data may be ignored or overwritten by old data; again, this is an operational challenge (and from what I've heard, one that people are acutely aware of). Another interesting example is Google's [Spanner](http://research.google.com/archive/spanner.html): the paper describes their TrueTime API, which synchronizes time but also estimates worst-case clock drift.

全局时钟的存在使在任何节点上的操作都按照一定的顺序执行，即便这些节点之间不发生通信交互

但在现实世界中，时钟同步只能存在于能容忍一定程度上的不精确的系统中。因为由于空间分布的原因而存在的延迟。

假设时钟在分布式节点中完美同步的话，说明时间都是从同一个值开始计时，并且永远不会不一样。这样一来的话使用时间戳的话就能完美的保证全局顺序。但是通常会有异常现象存在，但针对这些异常，也会有相应的处理方案

然而，现实中有一些系统使用的是全局时钟假设。比如Facebook的Cassandra系统。它使用时间戳来解决写的冲突，最新的版本胜出。另一个例子Google的Spanner，它的时钟也是同步的，但同时考虑了最坏情况下的时钟漂移

### Time with a "Local-clock" assumption  本地时钟

The second, and perhaps more plausible assumption is that each machine has its own clock, but there is no global clock. It means that you cannot use the local clock in order to determine whether a remote timestamp occurred before or after a local timestamp; in other words, you cannot meaningfully compare timestamps from two different machines.

更合理的情况是，每一个机器上有自己的时钟，但是不存在全局时钟。即你能通过本地时间戳来确定本地事件发生顺序，但是不能对不同机器间的时间戳进行比较

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190425231027.png)

The local clock assumption corresponds more closely to the real world. It assigns a partial order: events on each system are ordered but events cannot be ordered across systems by only using a clock.

本地时钟的假设最符合真实世界的情况。它表明时间在每一个独立的分区上能够有序，但是在所有的系统中仅仅靠时钟来并不能保证事件的顺序

However, you can use timestamps to order events on a single machine; and you can use timeouts on a single machine as long as you are careful not to allow the clock to jump around. Of course, on a machine controlled by an end-user this is probably assuming too much: for example, a user might accidentally change their date to a different value while looking up a date using the operating system's date control.

### Time with a "No-clock" assumption 无时钟

Finally, there is the notion of logical time. Here, we don't use clocks at all and instead track causality in some other way. Remember, a timestamp is simply a shorthand for the state of the world up to that point - so we can use counters and communication to determine whether something happened before, after or concurrently with something else.

这里存在一个逻辑时间的概念。不在使用时钟来追寻时间发生的因果顺序，而是通过另一种其它的方式-**计数器和通信**

This way, we can determine the order of events between different machines, but cannot say anything about intervals and cannot use timeouts (since we assume that there is no "time sensor"). This is a partial order: events can be ordered on a single system using a counter and no communication, but ordering events across systems requires a message exchange.

通过这种方式，我们能对不同机器上发生的事件的顺序进行比较，但是没法使用关于时间间隔以及超时设置的变量了（即不存在时间传感器了）。这从某种角度而言也是一种局部有序的情况：时间在单系统中能够使用计数器并且不用进行通信来保证事件顺序，但是在多系统之间就需要进行信息交换

One of the most cited papers in distributed systems is Lamport's paper on [time, clocks and the ordering of events](http://research.microsoft.com/users/lamport/pubs/time-clocks.pdf). Vector clocks, a generalization of that concept (which I will cover in more detail), are a way to track causality without using clocks. Cassandra's cousins Riak (Basho) and Voldemort (Linkedin) use vector clocks rather than assuming that nodes have access to a global clock of perfect accuracy. This allows those systems to avoid the clock accuracy issues mentioned earlier.

When clocks are not used, the maximum precision at which events can be ordered across distant machines is bound by communication latency.

**矢量钟就是一种不利用真正的时间来进行时间顺序因果追寻的方式**。后面会进行详细介绍

## How is time used in a distributed system?分布式系统中时间的作用

What is the benefit of time?

1. Time can define order across a system (without communication)
2. Time can define boundary conditions for algorithms

**时间的作用：**

- **能够定义系统中事件的顺序（不需要通信）**
- **能够定义算法的边界条件（故障检测器）**

The order of events is important in distributed systems, because many properties of distributed systems are defined in terms of the order of operations/events:

- where correctness depends on (agreement on) correct event ordering, for example serializability in a distributed database
- order can be used as a tie breaker when resource contention occurs, for example if there are two orders for a widget, fulfill the first and cancel the second one

在分布式系统中，确定事件发生的顺序是很重要的，因为许多操作是需要顺序进行的

- **系统的正确性取决于正确的时间顺序**
- **当资源发生竞争时，顺序能够作为评判标准**

A global clock would allow operations on two different machines to be ordered without the two machines communicating directly. Without a global clock, we need to communicate in order to determine order.

一个全局时钟能够允许两个机器上的操作有序进行并且不需要通信。但一旦没有全局时钟，我们就需要进行通信来确定不同机器上操作的顺序

Time can also be used to define boundary conditions for algorithms - specifically, to distinguish between "high latency" and "server or network link is down". This is a very important use case; in most real-world systems timeouts are used to determine whether a remote machine has failed, or whether it is simply experiencing high network latency. Algorithms that make this determination are called failure detectors; and I will discuss them fairly soon.

时间也可以用来定义算法的边界条件，比如，判定系统到底是发生了“高延迟”还是出现了“服务或者网络的宕机”。用来做这个判断的算法被称为是故障检测器，后面会进行讨论

## Vector clocks (time for causal order) 矢量时钟

Earlier, we discussed the different assumptions about the rate of progress of time across a distributed system. Assuming that we cannot achieve accurate clock synchronization - or starting with the goal that our system should not be sensitive to issues with time synchronization, how can we order things?

前面我们讨论了分布式系统中不同的时间假设。如果我们没法获得精确的时钟同步的话，那么如何保证分布式系统中事件有序呢？

Lamport clocks and vector clocks are replacements for physical clocks which rely on counters and communication to determine the order of events across a distributed system. These clocks provide a counter that is comparable across different nodes.

**Lamport clocks**和**矢量时钟**能够代替物理时钟来进行保证系统有序。通过**计数器**+**通信**来决定事件顺序

*A Lamport clock* is simple. Each process maintains a counter using the following rules:

- Whenever a process does work, increment the counter
- Whenever a process sends a message, include the counter
- When a message is received, set the counter to `max(local_counter, received_counter) + 1`

#### Lamport clock

Lamport clock很简单，每一个进程都有一个计数器，服从下面的规则：

- **一旦一个进程开始工作，计数器递增**
- **任何进程发送的消息中，包含计数器的值**
- **当一个消息被接收时，更新计数器的值为max（本地，接收）+1**

Expressed as code: 用代码表示的话:

```js
function LamportClock() {
  this.value = 1;
}

LamportClock.prototype.get = function() {
  return this.value;
}

LamportClock.prototype.increment = function() {
  this.value++;
}

LamportClock.prototype.merge = function(other) {
  this.value = Math.max(this.value, other.value) + 1;
}
```

A [Lamport clock](http://en.wikipedia.org/wiki/Lamport_timestamps) allows counters to be compared across systems, with a caveat: Lamport clocks define a partial order. If `timestamp(a) < timestamp(b)`:

- `a` may have happened before `b` or
- `a` may be incomparable with `b`

Lamport clock允许使用计数器来比较事件发生的顺序，比如如果 `timestamp(a) < timestamp(b)`：

- a可能在b之前发生
- a可能b无法和b比较

This is known as clock consistency condition: if one event comes before another, then that event's logical clock comes before the others. If `a` and `b` are from the same causal history, e.g. either both timestamp values were produced on the same process; or `b` is a response to the message sent in `a` then we know that `a` happened before `b`.

这和时钟的一致性一样，如果一件事发生在另一件事之前，那么它的逻辑时钟也发生在这件事之前。如果事件a和事件b都是从同一个历史中演化而来的，那么如果a<b，a一定发生在b之前

Intuitively, this is because a Lamport clock can only carry information about one timeline / history; hence, comparing Lamport timestamps from systems that never communicate with each other may cause concurrent events to appear to be ordered when they are not.

使用Lamport时钟也有一个缺点，因为它只包含了一个时间线的计数信息，那么同步发生的事情在这个时钟下仍然可比，即表现出有序性，但本质上，他们是同步的，不应该表现出有序性

Imagine a system that after an initial period divides into two independent subsystems which never communicate with each other.

For all events in each independent system, if a happened before b, then `ts(a) < ts(b)`; but if you take two events from the different independent systems (e.g. events that are not causally related) then you cannot say anything meaningful about their relative order.  While each part of the system has assigned timestamps to events, those timestamps have no relation to each other. Two events may appear to be ordered even though they are unrelated.

However - and this is still a useful property - from the perspective of a single machine, any message sent with `ts(a)` will receive a response with `ts(b)` which is `> ts(a)`.

*A vector clock* is an extension of Lamport clock, which maintains an array `[ t1, t2, ... ]` of N logical clocks - one per each node. Rather than incrementing a common counter, each node increments its own logical clock in the vector by one on each internal event. Hence the update rules are:

- Whenever a process does work, increment the logical clock value of the node in the vector
- Whenever a process sends a message, include the full vector of logical clocks
- When a message is received:
  - update each element in the vector to be `max(local, received)`
  - increment the logical clock value representing the current node in the vector

#### vector clock矢量时钟

矢量时钟是Lamport clock的一个衍生，它包含了一个含有N个节点计数器值的计数器列表 [t1,t2,...]，每一个节点递增他们自己的逻辑时钟（计数器的值），规则如下：

- **一旦一个进程开始工作，矢量钟中的关于该进程节点上的计数器值递增**
- **任何进程发送的消息中，包含矢量计数器列表**
- **当一个消息被接收时：**
  - 更新矢量
  - 递增矢量中代表当前节点的逻辑时钟的计数器值

Again, expressed as code:

```js
function VectorClock(value) {
  // expressed as a hash keyed by node id: e.g. { node1: 1, node2: 3 }
  this.value = value || {};
}

VectorClock.prototype.get = function() {
  return this.value;
};

VectorClock.prototype.increment = function(nodeId) {
  if(typeof this.value[nodeId] == 'undefined') {
    this.value[nodeId] = 1;
  } else {
    this.value[nodeId]++;
  }
};

VectorClock.prototype.merge = function(other) {
  var result = {}, last,
      a = this.value,
      b = other.value;
  // This filters out duplicate keys in the hash
  (Object.keys(a)
    .concat(b))
    .sort()
    .filter(function(key) {
      var isDuplicate = (key == last);
      last = key;
      return !isDuplicate;
    }).forEach(function(key) {
      result[key] = Math.max(a[key] || 0, b[key] || 0);
    });
  this.value = result;
};
```

This illustration ([source](http://en.wikipedia.org/wiki/Vector_clock)) shows a vector clock : 下图也能表示矢量钟：

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190429224307.png)

Each of the three nodes (A, B, C) keeps track of the vector clock. As events occur, they are timestamped with the current value of the vector clock. Examining a vector clock such as `{ A: 2, B: 4, C: 1 }` lets us accurately identify the messages that (potentially) influenced that event.

The issue with vector clocks is mainly that they require one entry per node, which means that they can potentially become very large for large systems. A variety of techniques have been applied to reduce the size of vector clocks (either by performing periodic garbage collection, or by reducing accuracy by limiting the size).

We've looked at how order and causality can be tracked without physical clocks. Now, let's look at how time durations can be used for cutoff.

上图对ABC三个节点的矢量钟进行了一个追踪。可以发现当一个事件发生后，矢量钟对每个节点目前的情况打上了一个时间戳的标记，随着事件发生，计数器的值进行改变。从矢量值中可以对事件发生的顺序进行判断 

**总结：**

- **Lamport clock每个节点上维护一个计数器值，每次通信对这个值进行更新**
- **vector clock每个节点上维护一个计数器列表，每次通信对这个列表进行更新**

## Failure detectors (time for cutoff) 故障检测器

As I stated earlier, the amount of time spent waiting can provide clues about whether a system is partitioned or merely experiencing high latency. In this case, we don't need to assume a global clock of perfect accuracy - it is simply enough that there is a reliable-enough local clock.

前面说过，等待花费的时长能够用来作为一个判断系统到底是发生了分区故障，还是高延迟的一种线索。在这里，我们不需要假设有一个精确的全局时钟，仅仅有一个可信赖的本地时钟就足够了

Given a program running on one node, how can it tell that a remote node has failed? In the absence of accurate information, we can infer that an unresponsive remote node has failed after some reasonable amount of time has passed.

But what is a "reasonable amount"? This depends on the latency between the local and remote nodes. Rather than explicitly specifying algorithms with specific values (which would inevitably be wrong in some cases), it would be nicer to deal with a suitable abstraction.

一个节点上运行一个程序，如果运行的信息延迟的时间到达一定的时长的话，我们就认为这个节点发生了故障。但是这个时长改如何定义呢？

A failure detector is a way to abstract away the exact timing assumptions. Failure detectors are implemented using heartbeat messages and timers. Processes exchange heartbeat messages. If a message response is not received before the timeout occurs, then the process suspects the other process.

A failure detector based on a timeout will carry the risk of being either overly aggressive (declaring a node to have failed) or being overly conservative (taking a long time to detect a crash). How accurate do failure detectors need to be for them to be usable?

**故障检测器是一种抽象的方法：心跳信息+定时器**

进程间交换心跳信息，如果一个进程在超时前没有收到响应信息，那么这个进程会怀疑其它进程

一个基于超时而言的故障检测器通常要么会过度检测（轻易断言一个节点发生故障），要么检测会过于保守（花费很长的等待时间来判断故障）。那么一个如何使用故障检测器使得它发挥出好的作用呢？

[Chandra et al.](http://www.google.com/search?q=Unreliable%20Failure%20Detectors%20for%20Reliable%20Distributed%20Systems) (1996) discuss failure detectors in the context of solving consensus - a problem that is particularly relevant since it underlies most replication problems where the replicas need to agree in environments with latency and network partitions.

They characterize failure detectors using two properties, completeness and accuracy:

>**Strong completeness.**
>Every crashed process is eventually suspected by every correct process.
>
>**Weak completeness.**
>Every crashed process is eventually suspected by some correct process.
>
>**Strong accuracy.**
>No correct process is suspected ever.
>
>**Weak accuracy.**
>Some correct process is never suspected.

有人用两个属性（完整性、精确性）将故障检测器进行了描述：

**强完整性**：每个故障的进程都会被任何正确的进程怀疑
**弱完整性**：每个故障的进程会被一部分正确的进程怀疑
**强精确性**：没有正确的进程会被怀疑
**弱精确性**：一些正确的进程也会被怀疑

Completeness is easier to achieve than accuracy; indeed, all failure detectors of importance achieve it - all you need to do is not to wait forever to suspect someone. Chandra et al. note that a failure detector with weak completeness can be transformed to one with strong completeness (by broadcasting information about suspected processes), allowing us to concentrate on the spectrum of accuracy properties.

完整性比精确性更容易实现。并且一个弱完整性的故障检测器能够转换成强完整性的故障检测器（通过广播被怀疑的进程的消息）

Avoiding incorrectly suspecting non-faulty processes is hard unless you are able to assume that there is a hard maximum on the message delay. That assumption can be made in a synchronous system model - and hence failure detectors can be strongly accurate in such a system. Under system models that do not impose hard bounds on message delay, failure detection can at best be eventually accurate.

通常对一个没有发生故障的进程进行错误的怀疑是无法避免的，因为你不知道消息延迟的上限是多少。但是在同步系统中，这个消息延迟的上限是固定的，因此在同步系统中使用故障检测器是非常精确的。

Chandra et al. show that even a very weak failure detector - the eventually weak failure detector ⋄W (eventually weak accuracy + weak completeness) - can be used to solve the consensus problem. The diagram below (from the paper) illustrates the relationship between system models and problem solvability:

研究表明即使是一个弱故障检测器（最终故障检测器），也能用来解决一致性问题。下图阐述了系统模型和问题可解性之间的关系：

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190429225320.png)

As you can see above, certain problems are not solvable without a failure detector in asynchronous systems. This is because without a failure detector (or strong assumptions about time bounds e.g. the synchronous system model), it is not possible to tell whether a remote node has crashed, or is simply experiencing high latency. That distinction is important for any system that aims for single-copy consistency: failed nodes can be ignored because they cannot cause divergence, but partitioned nodes cannot be safely ignored.

从上图可以看到，异步系统中，不使用故障检测器是无法对明确的问题进行解决的。因为，如果没有故障检测器，你无法得知一个远方的节点是否发生故障，或是因为高延迟的存在。这个判断对于单拷贝一致性的系统来说非常重要：故障节点会被忽略，因为它们不会带来分歧，但是分区节点不能被忽略，因为可能会造成数据分歧

How can one implement a failure detector? Conceptually, there isn't much to a simple failure detector, which simply detects failure when a timeout expires. The most interesting part relates to how the judgments are made about whether a remote node has failed.

怎样实施一个故障检测器呢？事实上，不存在一个很简单的故障检测器，因为判断一个节点是否发生故障时很难的

Ideally, we'd prefer the failure detector to be able to adjust to changing network conditions and to avoid hardcoding timeout values into it. For example, Cassandra uses an [accrual failure detector](https://www.google.com/search?q=The+Phi+accrual+failure+detector), which is a failure detector that outputs a suspicion level (a value between 0 and 1) rather than a binary "up" or "down" judgment. This allows the application using the failure detector to make its own decisions about the tradeoff between accurate detection and early detection.

依赖超时设置的值来判断是否发生故障。Cassandra它使用的是一个精确的故障检测器，它给出的故障判断是一个猜测值（0-1间，概率值）而不是一个二进制的数（0、1），这样一来，系统应用能够根据自己的定义来判断节点是否发生故障，进行一个无误检测和超前检测的权衡

## Time, order and performance 时间、顺序和性能

Earlier, I alluded to having to pay the cost for order. What did I mean?

If you're writing a distributed system, you presumably own more than one computer. The natural (and realistic) view of the world is a partial order, not a total order. You can transform a partial order into a total order, but this requires communication, waiting and imposes restrictions that limit how many computers can do work at any particular point in time.

如果你在设计一个分布式系统，你肯定拥有不止一台的计算机。那么从直观的角度来看，顺序是分区有序的而并非全局有序。你能够通过通信的方式，使得分区有序转变成全局有序，但是这通常还需要等待以及受到任意同一时刻能够有多少节点进行同时工作的限制

All clocks are mere approximations bound by either network latency (logical time) or by physics. Even keeping a simple integer counter in sync across multiple nodes is a challenge.

就最简单的保持一个简单的整数计数器在分布节点上同步都很有挑战性

While time and order are often discussed together, time itself is not such a useful property. Algorithms don't really care about time as much as they care about more abstract properties:

- the causal ordering of events
- failure detection (e.g. approximations of upper bounds on message delivery)
- consistent snapshots (e.g. the ability to examine the state of a system at some point in time; not discussed here)

事实上，算法通常不在乎时间，而是在乎顺序：

- 事件发生的原因顺序
- 故障检测器
- 一致快照

Imposing a total order is possible, but expensive. It requires you to proceed at the common (lowest) speed. Often the easiest way to ensure that events are delivered in some defined order is to nominate a single (bottleneck) node through which all operations are passed.

全局一致是可以实现的，但是代价很大，它要求所有的处理在相同的速度条件下。一个最简单的方法是：投票，选出一个经过了所有操作的节点出来

Is time / order / synchronicity really necessary? It depends. In some use cases, we want each intermediate operation to move the system from one consistent state to another. For example, in many cases we want the responses from a database to represent all of the available information, and we want to avoid dealing with the issues that might occur if the system could return an inconsistent result.

时间/顺序/同步真的有必要吗？这取决于你的案例。比如在一些用户案例中，我们想要每一次的操作都能让系统从一个一致性的转态转到另一个一致性的状态。举个例子：在数据库中，我们想要从数据库中找到能代表所有可用的信息，同时我们想避免处理系统返回不一致的结果所带来的问题。

But in other cases, we might not need that much time / order / synchronization. For example, if you are running a long running computation, and don't really care about what the system does until the very end - then you don't really need much synchronization as long as you can guarantee that the answer is correct.

但是在其他的一些例子中，我们可能不需要时间/顺序/同步。比如，我们想进行一个很长的计算操作，只要能保证最后的结果是正确的，我们并不关心这些运算是否同步发生

Synchronization is often applied as a blunt tool across all operations, when only a subset of cases actually matter for the final outcome. When is order needed to guarantee correctness? The CALM theorem - which I will discuss in the last chapter - provides one answer.

同步性通常是用来做为操作的限制工具的，仅仅当我们的结果只是收到一部分数据集的影响的时候才需要。顺序什么时候保证系统的可用性-这涉及到我们之后讨论的CALM理论

In other cases, it is acceptable to give an answer that only represents the best known estimate - that is, is based on only a subset of the total information contained in the system. In particular, during a network partition one may need to answer queries with only a part of the system being accessible. In other use cases, the end user cannot really distinguish between a relatively recent answer that can be obtained cheaply and one that is guaranteed to be correct and is expensive to calculate. For example, is the Twitter follower count for some user X, or X+1? Or are movies A, B and C the absolutely best answers for some query? Doing a cheaper, mostly correct "best effort" can be acceptable.

In the next two chapters we'll examine replication for fault-tolerant strongly consistent systems - systems which provide strong guarantees while being increasingly resilient to failures. These systems provide solutions for the first case: when you need to guarantee correctness and are willing to pay for it. Then, we'll discuss systems with weak consistency guarantees, which can remain available in the face of partitions, but that can only give you a "best effort" answer.

还有另一些例子中，我们能够接受那些尽力而为的答案作为我们系统的最后结果。这会涉及到一致性问题

- **强一致性：保证准确性但付出可用性低的代价**
- **弱一致性：保证系统可用性，但要只能告诉你“best effort”(尽力了)**

------

## Further reading

### Lamport clocks, vector clocks

- [Time, Clocks and Ordering of Events in a Distributed System](http://research.microsoft.com/users/lamport/pubs/time-clocks.pdf) - Leslie Lamport, 1978

### Failure detection

- [Unreliable failure detectors and reliable distributed systems](http://scholar.google.com/scholar?q=Unreliable+Failure+Detectors+for+Reliable+Distributed+Systems) - Chandra and Toueg
- [Latency- and Bandwidth-Minimizing Optimal Failure Detectors](http://www.cs.cornell.edu/people/egs/sqrt-s/doc/TR2006-2025.pdf) - So & Sirer, 2007
- [The failure detector abstraction](http://scholar.google.com/scholar?q=The+failure+detector+abstraction), Freiling, Guerraoui & Kuznetsov, 2011

### Snapshots

- [Consistent global states of distributed systems: Fundamental concepts and mechanisms](http://scholar.google.com/scholar?q=Consistent+global+states+of+distributed+systems%3A+Fundamental+concepts+and+mechanisms), Ozalp Babaogly and Keith Marzullo, 1993
- [Distributed snapshots: Determining global states of distributed systems](http://scholar.google.com/scholar?q=Distributed+snapshots%3A+Determining+global+states+of+distributed+systems), K. Mani Chandy and Leslie Lamport, 1985

### Causality

- [Detecting Causal Relationships in Distributed Computations: In Search of the Holy Grail](http://www.vs.inf.ethz.ch/publ/papers/holygrail.pdf) - Schwarz & Mattern, 1994
- [Understanding the Limitations of Causally and Totally Ordered Communication](http://scholar.google.com/scholar?q=Understanding+the+limitations+of+causally+and+totally+ordered+communication) - Cheriton & Skeen, 1993