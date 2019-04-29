---
title: '[译]Distributed systems for fun and profit_0引言&1基本概念'
date: 2019-04-23 17:32:04
categories:
- 微服务理论文章阅读学习
tags:
top:
---

# 零.Introduction 引言

I wanted a text that would bring together the ideas behind many of the more recent distributed systems - systems such as Amazon's Dynamo, Google's BigTable and MapReduce, Apache's Hadoop and so on.

我想写一篇能够说明目前一些分布式系统，例如亚马逊的Dynamo，谷歌的Big Table和Map Reduce，Apache的Hadoop背后的一些原理的文章

In this text I've tried to provide a more accessible introduction to distributed systems. To me, that means two things: introducing the key concepts that you will need in order to [have a good time](https://www.google.com/search?q=super+cool+ski+instructor) reading more serious texts, and providing a narrative that covers things in enough detail that you get a gist of what's going on without getting stuck on details. It's 2013, you've got the Internet, and you can selectively read more about the topics you find most interesting.

在这篇文章中我会尽力更简单易懂的介绍分布式系统，不关注概念背后的具体细节

In my view, much of distributed programming is about dealing with the implications of two consequences of distribution:

- that information travels at the speed of light
- that independent things fail independently*

在我看来，**多数的分布式程序都是为了解决分布式导致的两个问题：**

- **信息光速传播**
- **独立事务独立失败**

<font color=red >具体怎么理解呢？信息的传输速度是光速，虽然已经很快了，但是还是需要时间的，另一个系统要处理的错误不仅一类，而是会很多，而且这些错误之间没有联系，彼此独立。</font>

In other words, that the core of distributed programming is dealing with distance (duh!) and having more than one thing (duh!). These constraints define a space of possible system designs, and my hope is that after reading this you'll have a better sense of how distance, time and consistency models interact.

换句话而言，分布式程序的核心就是处理距离带来的问题和处理多个问题。这两方面的约束定义了一系列设计规则。希望通过这篇文章，你能更好的理解距离、时间和一致性模型是怎样相互影响的

This text is focused on distributed programming and systems concepts you'll need to understand commercial systems in the data center. It would be madness to attempt to cover everything. You'll learn many key protocols and algorithms (covering, for example, many of the most cited papers in the discipline), including some new exciting ways to look at eventual consistency that haven't still made it into college textbooks - such as CRDTs and the CALM theorem.

文章会介绍一些主要的协议和算法，包括一些新的怎么去保证最终一致性的方法，比如CRDTs和CALM理论

I hope you like it! If you want to say thanks, follow me on [Github](https://github.com/mixu/) (or [Twitter](http://twitter.com/mikitotakada)). And if you spot an error, [file a pull request on Github](https://github.com/mixu/distsysbook/issues).

---

## 1. Basics 基本概念

[The first chapter](http://book.mixu.net/distsys/single-page.html#intro) covers distributed systems at a high level by introducing a number of important terms and concepts. It covers high level goals, such as scalability, availability, performance, latency and fault tolerance; how those are hard to achieve, and how abstractions and models as well as partitioning and replication come into play.

第一节会介绍分布式系统的一些重要术语和概念。包括可扩展性、实用性、性能、延迟和容错性，及这些实施起来的难度，并引入抽象和模型比如分区和复制的设计规则

## 2. Up and down the level of abstraction

## 一系列的抽象来描述系统的特征

[The second chapter](http://book.mixu.net/distsys/single-page.html#abstractions) dives deeper into abstractions and impossibility results. It starts with a Nietzsche quote, and then introduces system models and the many assumptions that are made in a typical system model. It then discusses the CAP theorem and summarizes the FLP impossibility result. It then turns to the implications of the CAP theorem, one of which is that one ought to explore other consistency models. A number of consistency models are then discussed.

第二节会关注抽象和不可能的结果。首先会介绍尼采的引言，然后会从各种典型的系统模型的假设来介绍分布式系统模型，接着会讨论CAP原理以及FLP不可能结果。最后会介绍CAP原理怎样实施。还会讨论许多一致性的模型

## 3. Time and order 时间和顺序

A big part of understanding distributed systems is about understanding time and order.  To the extent that we fail to understand and model time, our systems will fail. [The third chapter](http://book.mixu.net/distsys/single-page.html#time) discusses time and order, and clocks as well as the various uses of time, order and clocks (such as vector clocks and failure detectors).

理解分布式系统的很重要的一点，需要了解时间和顺序。错误理解模型的时间，系统也不能很好的理解。第三节将讨论时间、顺序和时钟及他们的应用

## 4. Replication: preventing divergence 复制：强一致性

The [fourth chapter](http://book.mixu.net/distsys/single-page.html#replication) introduces the replication problem, and the two basic ways in which it can be performed. It turns out that most of the relevant characteristics can be discussed with just this simple characterization. Then, replication methods for maintaining single-copy consistency are discussed from the least fault tolerant (2PC) to Paxos.

第四节将讨论分布式系统中的复制问题，以及介绍两种主要的复制方式。将从最小容错到Paxos来论述保持单拷贝一致性的复制方法

## 5. Replication: accepting divergence  复制：弱一致性

The [fifth chapter](http://book.mixu.net/distsys/single-page.html#eventual) discussed replication with weak consistency guarantees. It introduces a basic reconciliation scenario, where partitioned replicas attempt to reach agreement. It then discusses Amazon's Dynamo as an example of a system design with weak consistency guarantees. Finally, two perspectives on disorderly programming are discussed: CRDTs and the CALM theorem.

第五节将讨论保持弱一致性的复制。首先介绍分散复制集如何达到最终一致，然后以亚马逊的Dynamo系统作为一个例子，来讨论怎样设计一个保证弱一致性的分布式系统。最后，介绍了CRDTs和CALM理论

## 6. Appendix 

[The appendix](http://book.mixu.net/distsys/appendix.html) covers recommendations for further reading.

---

<p class="footnote">*: This is a [lie](http://en.wikipedia.org/wiki/Statistical_independence). [This post by Jay Kreps elaborates](http://blog.empathybox.com/post/19574936361/getting-real-about-distributed-system-reliability).
</p>
# 一.Distributed systems at a high level  分布式系统概览

> Distributed programming is the art of solving the same problem that you can solve on a single computer using multiple computers.
> 分布式编程就是使用多台计算机解决单机问题

There are two basic tasks that any computer system needs to accomplish:

- storage and
- computation

**分布式系统主要是为了提高两个方面的能力**

- **存储能力**
- **计算能力**

Distributed programming is the art of solving the same problem that you can solve on a single computer using multiple computers - usually, because the problem no longer fits on a single computer.

分布式编程就是使用多台计算机解决单机问题-通常是因为问题已经不适合使用单机环境解决

Nothing really demands that you use distributed systems. Given infinite money and infinite R&D time, we wouldn't need distributed systems. All computation and storage could be done on a magic box - a single, incredibly fast and incredibly reliable system *that you pay someone else to design for you*.

如果有足够的钱和无穷的等待回复时间，那分布式系统就没有存在的必要。所有的计算和存储都能在一个单机上实现

However, few people have infinite resources. Hence, they have to find the right place on some real-world cost-benefit curve. At a small scale, upgrading hardware is a viable strategy. However, as problem sizes increase you will reach a point where either the hardware upgrade that allows you to solve the problem on a single node does not exist, or becomes cost-prohibitive. At that point, I welcome you to the world of distributed systems.

但实际上不会存在无尽的资源。因此，我们必须了解真实存在的花费-收益曲线。在小范围内，升级硬件资源能够带来更多的收益。但是随着规模的扩大，光靠升级一个单节点上的硬件来解决问题的成本花费是及其高的。基于此，分布式系统的存在就很有必要性

It is a current reality that the best value is in mid-range, commodity hardware - as long as the maintenance costs can be kept down through fault-tolerant software.

在现实情况中，最好的结果是拥有中等程度的硬件，同时维护成本能够通过具有容错性的软件来降低

Computations primarily benefit from high-end hardware to the extent to which they can replace slow network accesses with internal memory accesses. The performance advantage of high-end hardware is limited in tasks that require large amounts of communication between nodes.

高端硬件带来计算的主要好处在于，从内存直接访问数据速度会比网络获取快得多，能够降低访问速度。但是当节点之间需要进行大量的通信时，高端硬件的优点会受到限制

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190423153938.png)

As the figure above from [Barroso, Clidaras & Hölzle](http://www.morganclaypool.com/doi/abs/10.2200/S00516ED2V01Y201306CAC024) shows, the performance gap between high-end and commodity hardware decreases with cluster size assuming a uniform memory access pattern across all nodes.

图：不同粒度的集群下，高端硬件（128-core SMP）和低端硬件（4-core SMP）构建同样的处理核的性能对比

所有节点都采用统一的内存访问模式时，随着集群大小的增加，高端硬件和普通硬件之间的性能差距会减小。

Ideally, adding a new machine would increase the performance and capacity of the system linearly. But of course this is not possible, because there is some overhead that arises due to having separate computers. Data needs to be copied around, computation tasks have to be coordinated and so on. This is why it's worthwhile to study distributed algorithms - they provide efficient solutions to specific problems, as well as guidance about what is possible, what the minimum cost of a correct implementation is, and what is impossible.

理想情况下，增加一个新的机器会使得系统的性能线性增长。但是这显然是不现实的，因为由于计算机的分离会带来一些难以克服的问题：数据需要被复制到各个节点上，需要协作节点上的计算任务等。这也是为什么分布式算法值得花时间学习-分布式算法能为这些特定问题提供有效的解决方案，同时指引我们什么是可能的，正确的实现解决方案需要的最小代价是什么,以及什么是不可能的

The focus of this text is on distributed programming and systems in a mundane, but commercially relevant setting: the data center. For example, I will not discuss specialized problems that arise from having an exotic network configuration, or that arise in a shared-memory setting. Additionally, the focus is on exploring the system design space rather than on optimizing any specific design - the latter is a topic for a much more specialized text.

本文关注系统的设计，不进行细节优化的探讨

## What we want to achieve: Scalability and other good things<br/>我们的目标：扩展性和其他好的效果

The way I see it, everything starts with the need to deal with size.

在我看来,分布式系统的一切都是和系统规模做斗争

Most things are trivial at a small scale - and the same problem becomes much harder once you surpass a certain size, volume or other physically constrained thing. It's easy to lift a piece of chocolate, it's hard to lift a mountain. It's easy to count how many people are in a room, and hard to count how many people are in a country.

任何问题在小范围内都很容易,当超过了一定规模，问题将变得难以解决

So everything starts with size - scalability. Informally speaking, in a scalable system as we move from small to large, things should not get incrementally worse. Here's another definition:


>[Scalability](http://en.wikipedia.org/wiki/Scalability) is the ability of a system, network, or process, to handle a growing amount of work in a capable manner or its ability to be  enlarged to accommodate that growth.
>  扩展性：一个系统、网络或进程适应任务量增长的能力，增长后性能不会受很大的影响


What is it that is growing? Well, you can measure growth in almost any terms (number of people, electricity usage etc.). But there are three particularly interesting things to look at:

- Size scalability: adding more nodes should make the system linearly faster; growing the dataset should not increase latency
- Geographic scalability: it should be possible to use multiple data centers to reduce the time it takes to respond to user queries, while dealing with cross-data center latency in some sensible manner.
- Administrative scalability: adding more nodes should not increase the administrative costs of the system (e.g. the administrators-to-machines ratio).

**增长关注的三个维度：**

- **尺寸可扩展：**增加节点使系统线性增长，数据增大不会加大延迟
- **地理可扩展：**多数据中心能够被用来减少反馈用户查询命任务的响应时间，同时能够处理因多中心带来的延迟
- **管理可扩展：**增加节点的同时，不应该增加系统在管理节点上的开销

Of course, in a real system growth occurs on multiple different axes simultaneously; each metric captures just some aspect of growth.

在真实的系统中，增长往往同时发生在多个不同的维度上；每个度量标准仅仅表述的是某个方面的增长

A scalable system is one that continues to meet the needs of its users as scale increases. There are two particularly relevant aspects - performance and availability - which can be measured in various ways.

一个可扩展性的系统随着尺度的增长，仍然能够满足用户的需求。**性能和可用性**通常用来衡量系统是否能够满足用户需求。

## Performance (and latency) 	性能（和延迟）

> [Performance](http://en.wikipedia.org/wiki/Computer_performance) is characterized by the amount of useful work accomplished by a computer system compared to the time and resources used.
> 性能：任务所花费的时间和资源

Depending on the context, this may involve achieving one or more of the following:

- Short response time/low latency for a given piece of work
- High throughput (rate of processing work)
- Low utilization of computing resource(s)

**好性能的系统通常需要满足以下三点：**

- **低响应时间**
- **高吞吐量（生产力，即工作进程速率）**
- **低计算资源利用率**

There are tradeoffs involved in optimizing for any of these outcomes. For example, a system may achieve a higher throughput by processing larger batches of work thereby reducing operation overhead. The tradeoff would be longer response times for individual pieces of work due to batching.

想要优化这些结果，系统需要进行权衡和折中处理。比如：一个系统通过处理大批次的工作可能拥有很高的吞吐量，但是由于个体间的独立工作会导致响应时间变长。

I find that low latency - achieving a short response time - is the most interesting aspect of performance, because it has a strong connection with physical (rather than financial) limitations. It is harder to address latency using financial resources than the other aspects of performance.

同时，低延迟（短的响应时间）是性能表现指标中最有意思的，它很大程度上是受物理分布的限制，而与经济条件限制无关。你很难通过利用更好的硬件资源来减少分布式系统的延迟

There are a lot of really specific definitions for latency, but I really like the idea that the etymology of the word evokes:

>Latency	
The state of being latent; delay, a period between the initiation of something and the occurrence.
延迟性：事务从发生开始到产生具象的时长


And what does it mean to be "latent"?

>Latent
From Latin latens, latentis, present participle of lateo ("lie hidden"). Existing or present but concealed or inactive.

This definition is pretty cool, because it highlights how latency is really the time between when something happened and the time it has an impact or becomes visible.

For example, imagine that you are infected with an airborne virus that turns people into zombies. The latent period is the time between when you became infected, and when you turn into a zombie. That's latency: the time during which something that has already happened is concealed from view.

假设一个分布式系统在处理一个高级别的任务：给定一个查询，需要读取系统的各个节点的数据最终计算返回一个值

Let's assume for a moment that our distributed system does just one high-level task: given a query, it takes all of the data in the system and calculates a single result. In other words, think of a distributed system as a data store with the ability to run a single deterministic computation (function) over its current content:

`result = query(all data in the system)`

`结果=查询（系统的所有数据）`

Then, what matters for latency is not the amount of old data, but rather the speed at which new data "takes effect" in the system. For example, latency could be measured in terms of how long it takes for a write to become visible to readers.

影响系统延迟性的并不在于数据的多少，而在于系统处理数据到返回结果的速度。具体而言，延迟性应该是系统能够返回一个直观结果所需要花费的时长

The other key point based on this definition is that if nothing happens, there is no "latent period". A system in which data doesn't change doesn't (or shouldn't) have a latency problem.

In a distributed system, there is a minimum latency that cannot be overcome: the speed of light limits how fast information can travel, and hardware components have a minimum latency cost incurred per operation (think RAM and hard drives but also CPUs).

**在分布式系统中，有一个最小延迟无法避免：信息传播速度(光速)限制，硬件操作的时间**
**最小延迟时长的大小取决于查询语句本身，以及这些信息之间传输的物理距离**

How much that minimum latency impacts your queries depends on the nature of those queries and the physical distance the information needs to travel.

## Availability (and fault tolerance) **可用性（和容错性）**

The second aspect of a scalable system is availability.

> [Availability](http://en.wikipedia.org/wiki/High_availability) the proportion of time a system is in a functioning condition. If a user cannot access the system, it is said to be unavailable.
可用性：系统所能处于可用状态的时间比例

**Availability = uptime /（uptime + downtime）**

Distributed systems allow us to achieve desirable characteristics that would be hard to accomplish on a single system. For example, a single machine cannot tolerate any failures since it either fails or doesn't.

分布式系统建立冗余（组件、服务、数据等方面）来允许部分失败的发生，从而提升它的可用性。

Distributed systems can take a bunch of unreliable components, and build a reliable system on top of them.

Systems that have no redundancy can only be as available as their underlying components. Systems built with redundancy can be tolerant of partial failures and thus be more available. It is worth noting that "redundant" can mean different things depending on what you look at - components, servers, datacenters and so on.

Formulaically, availability is: `Availability = uptime / (uptime + downtime)`.

Availability from a technical perspective is mostly about being fault tolerant. Because the probability of a failure occurring increases with the number of components, the system should be able to compensate so as to not become less reliable as the number of components increases.

**可用性从技术角度而言，与系统的容错性相关。**例如：当系统的组件数量增多时，系统发生错误的可能性会上升，但一个高可用性的系统应该保证系统的可靠性不会随着组件数量的增多而下降。

For example:

| Availability %|  How much downtime is allowed per year?    |
| ---- | ---- |
| 90% (“one nine”) | More than a month |
| 99% (“two nines”) | Less than 4 days |
| 99.9% (“three nines”) | Less than 9 hours |
| 99.99% (“four nines”) | Less than an hour |
| 99.999% (“five nines”) | ~ 5 minutes |
| 99.9999% (“six nines”) | ~ 31 seconds |

Availability is in some sense a much wider concept than uptime, since the availability of a service can also be affected by, say, a network outage or the company owning the service going out of business (which would be a factor which is not really relevant to fault tolerance but would still influence the availability of the system). But without knowing every single specific aspect of the system, the best we can do is design for fault tolerance.

可用性的概念比正常运行时间概念更广。例如一个系统如果遭遇断电、或者服务发生中断，这个时候与系统的容错性无关，但是这些情况仍然会影响系统的可用性。
但通常而言，**系统的容错性越强，可用性越高。**提高系统的容错性是我们最需要关注的

What does it mean to be fault tolerant?

>Fault tolerance
ability of a system to behave in a well-defined manner once faults occur
Fault tolerance boils down to this: define what faults you expect and then design a system or an algorithm that is tolerant of them. You can't tolerate faults you haven't considered.
容错性：系统在错误发生后有明确的处理方式

系统定义发生的错误，并定义相应的处理方法。系统的容错性设计是考虑已想到的故障，没有考虑到的故障，系统是没法容错的

## What prevents us from achieving good things?<br/>什么阻止我们取得好的效果?

Distributed systems are constrained by two physical factors:

- the number of nodes (which increases with the required storage and computation capacity)
- the distance between nodes (information travels, at best, at the speed of light)

**分布式系统受两个物理因素的限制：**

- **节点数量（当需要更大的存储、计算能力时，节点数会增多）**
- **节点间的距离（信息传播）**

Working within those constraints:

- an increase in the number of independent nodes increases the probability of failure in a system (reducing availability and increasing administrative costs)
- an increase in the number of independent nodes may increase the need for communication between nodes (reducing performance as scale increases)
- an increase in geographic distance increases the minimum latency for communication between distant nodes (reducing performance for certain operations)

**限制：**

- **节点增加，系统发生失败的可能性增加（降低可用性，增加管理的花费）**
- **节点增加，独立节点之间的通信增多（随着尺度增大降低性能）**
- **地理距离增加，最小延迟时长增加（降低特定的操作的性能）**

Beyond these tendencies - which are a result of the physical constraints - is the world of system design options.

Both performance and availability are defined by the external guarantees the system makes. On a high level, you can think of the guarantees as the SLA (service level agreement) for the system: if I write data, how quickly can I access it elsewhere? After the data is written, what guarantees do I have of durability? If I ask the system to run a computation, how quickly will it return results? When components fail, or are taken out of operation, what impact will this have on the system?

性能和可用性都由系统的保证确定的。例如：在SLA中，系统保证如果写数据，那么多长时间能从其它地方访问它？如果让系统进行一个运算，多长时间能返回结果？当组件发生失败时，系统会遭受什么样的影响？

There is another criterion, which is not explicitly mentioned but implied: intelligibility. How understandable are the guarantees that are made? Of course, there are no simple metrics for what is intelligible.

这里有一个不明确但是需要实施的标准：可理解性。也就是这些保证需要能够被理解和明白的。

I was kind of tempted to put "intelligibility" under physical limitations. After all, it is a hardware limitation in people that we have a hard time understanding anything that involves [more moving things than we have fingers](http://en.wikipedia.org/wiki/Working_memory#Capacity). That's the difference between an error and an anomaly - an error is incorrect behavior, while an anomaly is unexpected behavior. If you were smarter, you'd expect the anomalies to occur.

## Abstractions and models 	**分布式系统中的抽象和模型**

This is where abstractions and models come into play. Abstractions make things more manageable by removing real-world aspects that are not relevant to solving a problem. Models describe the key properties of a distributed system in a precise manner. I'll discuss many kinds of models in the next chapter, such as:

- System model (asynchronous / synchronous)
- Failure model (crash-fail, partitions, Byzantine)
- Consistency model (strong, eventual)

**抽象：将现实层面的事务抽象出来，便于更好的管理和处理**

**模型：精确描述分布式系统中的关键属性**

**本文主要讨论下面三种类型的模型：**

- **系统模型（异步/同步）**
- **故障模型（crash-fail，分区，Byzantine）**
- **一致性模型（强一致性、最终一致性）**

A good abstraction makes working with a system easier to understand, while capturing the factors that are relevant for a particular purpose.

好的抽象能使系统的运行更方便理解，同时能捕获与特定目标相关的因素

There is a tension between the reality that there are many nodes and with our desire for systems that "work like a single system". Often, the most familiar model (for example, implementing a shared memory abstraction on a distributed system) is too expensive.

我们希望多节点上运行的分布式系统能像单系统一样运作。但是通常最熟悉的模型需要花费的代价是很大的，比如在一个分布式系统上实施内存共享

A system that makes weaker guarantees has more freedom of action, and hence potentially greater performance - but it is also potentially hard to reason about. People are better at reasoning about systems that work like a single system, rather than a collection of nodes.

一个实施若保证的分布式系统通常能获得更自由以及更好的操作性能，但同时它会难以推理验证。相比于多节点，我们往往能够很好的理解单系统的工作原理

One can often gain performance by exposing more details about the internals of the system. For example, in [columnar storage](http://en.wikipedia.org/wiki/Column-oriented_DBMS), the user can (to some extent) reason about the locality of the key-value pairs within the system and hence make decisions that influence the performance of typical queries. Systems which hide these kinds of details are easier to understand (since they act more like single unit, with fewer details to think about), while systems that expose more real-world details may be more performant (because they correspond more closely to reality).

**当对系统内部的了解更深时，系统的性能将会越高。**举个例子，在列存储技术中，典型的查询语句，用户能够推断键值对所在的位置从而做出影响性能的决定。**系统隐藏更多的技术细节，用户更容易理解，但性能会更低。**

Several types of failures make writing distributed systems that act like a single system difficult. Network latency and network partitions (e.g. total network failure between some nodes) mean that a system needs to sometimes make hard choices about whether it is better to stay available but lose some crucial guarantees that cannot be enforced, or to play it safe and refuse clients when these types of failures occur.

分布式系统中可能出现的各种问题，使得它像单系统一样运行变得困难。当发生网络延迟和网络分区时，系统将要面临是保证可用性还是保证系统的安全性的取舍

The CAP theorem - which I will discuss in the next chapter - captures some of these tensions. In the end, the ideal system meets both programmer needs (clean semantics) and business needs (availability/consistency/latency).

**CAP定理**会讨论分布式系统中这些问题的取舍关系

**一个理想的分布式系统需要满足编程人员的需求（明确的语义）和业务需求（可用性/一致性/延迟性）**

## Design techniques: partition and replicate<br>**分布式系统设计的技术：分区和复制**

The manner in which a data set is distributed between multiple nodes is very important. In order for any computation to happen, we need to locate the data and then act on it.

数据集在多节点中的分配方式非常重要。对于系统的任何计算，我们需要定位数据然后再对其进行操作

There are two basic techniques that can be applied to a data set. It can be split over multiple nodes (partitioning) to allow for more parallel processing. It can also be copied or cached on different nodes to reduce the distance between the client and the server and for greater fault tolerance (replication).

对数据集的操作设计到两个基本的技术：

- **分区：为了进行并行处理，数据将被分割到多个节点中**
- **复制：为了减少客户端和服务器之间的距离，同时为了提高容错性，数据会被备份或缓存到不同的节点中**

> Divide and conquer - I mean, partition and replicate.
>
> 分而治之-分区和复制

The picture below illustrates the difference between these two: partitioned data (A and B below) is divided into independent sets, while replicated data (C below) is copied to multiple locations.

下图阐述了两者之间的差异：分区数据（A和B）被划分成两个独立的数据集，于此同时复制数据（C）被拷贝到不同的节点上

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190423180954.png)

This is the one-two punch for solving any problem where distributed computing plays a role. Of course, the trick is in picking the right technique for your concrete implementation; there are many algorithms that implement replication and partitioning, each with different limitations and advantages which need to be assessed against your design objectives.

许多分布式的算法都用到了分区和复制。不同的限制条件取决于你设计的目的

### Partitioning **分区**

Partitioning is dividing the dataset into smaller distinct independent sets; this is used to reduce the impact of dataset growth since each partition is a subset of the data.

- Partitioning improves performance by limiting the amount of data to be examined and by locating related data in the same partition
- Partitioning improves availability by allowing partitions to fail independently, increasing the number of nodes that need to fail before availability is sacrificed

将数据集分割成相互独立的小数据集，减少因数据集增长而带来对单个节点的压力

- **提高性能：限制分区中数据量的大小，降低数据压力**
- **提高可用性：数据之间相互独立，不同分区之间失败互不影响，允许失败节点的存在**

Partitioning is also very much application-specific, so it is hard to say much about it without knowing the specifics. That's why the focus is on replication in most texts, including this one.

Partitioning is mostly about defining your partitions based on what you think the primary access pattern will be, and dealing with the limitations that come from having independent partitions (e.g. inefficient access across partitions, different rate of growth etc.).

分区需要考虑：

- **基于将要进行的主要访问模式如何定义分区**
- **怎样处理独立分区带来的局限性（跨分区低效率访问）**

### Replication **复制**

Replication is making copies of the same data on multiple machines; this allows more servers to take part in the computation.

将同样的数据备份到多个机器当中，这样能够使得更多的服务器参与到计算当中

Let me inaccurately quote [Homer J. Simpson](http://en.wikipedia.org/wiki/Homer_vs._the_Eighteenth_Amendment):

> To replication! The cause of, and solution to all of life's problems.

Replication - copying or reproducing something - is the primary way in which we can fight latency.

- Replication improves performance by making additional computing power and bandwidth applicable to a new copy of the data
- Replication improves availability by creating additional copies of the data, increasing the number of nodes that need to fail before availability is sacrificed

复制是解决延迟的主要方法之一

- **提高性能：复制使额外的计算能力和带宽适用于数据的新副本，从而提高了性能**
- **提高可用性：备份数据，允许更多的节点出错，提高容错性**

Replication is about providing extra bandwidth, and caching where it counts. It is also about maintaining consistency in some way according to some consistency model.

Replication allows us to achieve scalability, performance and fault tolerance. Afraid of loss of availability or reduced performance? Replicate the data to avoid a bottleneck or single point of failure. Slow computation? Replicate the computation on multiple systems. Slow I/O? Replicate the data to a local cache to reduce latency or onto multiple machines to increase throughput.

Replication is also the source of many of the problems, since there are now independent copies of the data that has to be kept in sync on multiple machines - this means ensuring that the replication follows a consistency model.

复制能够让我们实现系统的可扩展性和容错性。

- **避免单节点故障和瓶颈**
- **多系统上复制计算能够加快计算速度**
- **复制数据到本地缓存中能够减少在多个机器上进行运算的延迟时长，同时提高吞吐量**

The choice of a consistency model is crucial: a good consistency model provides clean semantics for programmers (in other words, the properties it guarantees are easy to reason about) and meets business/design goals such as high availability or strong consistency.

Only one consistency model for replication - strong consistency - allows you to program as-if the underlying data was not replicated. Other consistency models expose some internals of the replication to the programmer. However, weaker consistency models can provide lower latency and higher availability - and are not necessarily harder to understand, just different.

复制来到好处的同时，也带来了很多问题，最大的就是数据一致性问题，只有当模型是 strong consistency 的时候，我们才会得到一个简单的编程模型（和单机系统一致），其他模型我们都好去理解系统内部是怎么做的，这样子才能很好的满足我们的需求。

## Further reading

- [The Datacenter as a Computer - An Introduction to the Design of Warehouse-Scale Machines](http://www.morganclaypool.com/doi/pdf/10.2200/s00193ed1v01y200905cac006) - Barroso &  Hölzle, 2008
- [Fallacies of Distributed Computing](http://en.wikipedia.org/wiki/Fallacies_of_Distributed_Computing)
- [Notes on Distributed Systems for Young Bloods](http://www.somethingsimilar.com/2013/01/14/notes-on-distributed-systems-for-young-bloods/) - Hodges, 2013


