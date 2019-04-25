---
title: '[译]Distributed systems for fun and profit 引言&基本概念'
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

# 二. Up and down the level of abstraction 抽象描述系统的特征

In this chapter, we'll travel up and down the level of abstraction, look at some impossibility results (CAP and FLP), and then travel back down for the sake of performance.

这一节将关注一些不可能的结果（CAP和FLP），并且会继续讨论性能

If you've done any programming, the idea of levels of abstraction is probably familiar to you. You'll always work at some level of abstraction, interface with a lower level layer through some API, and probably provide some higher-level API or user interface to your users. The seven-layer [OSI model of computer networking](http://en.wikipedia.org/wiki/OSI_model) is a good example of this.

通常编程会在一些抽象层次上工作，比如API接口，7层计算机网络OSI模型（开放系统互连模型，将通信系统分为7层：物理层、数据链路层、网络层、传输层、会话层、表示层、应用层）

Distributed programming is, I'd assert, in large part dealing with consequences of distribution (duh!). That is, there is a tension between the reality that there are many nodes and with our desire for systems that "work like a single system". That means finding a good abstraction that balances what is possible with what is understandable and performant.

分布式编程在很大程度上都在解决由于分布带来的一系列问题。怎样让分布式系统工作起来像单机一样有一定的困难。我们希望找到一个能够平衡“可理解性”和“高效性”的好的抽象

What do we mean when say X is more abstract than Y? First, that X does not introduce anything new or fundamentally different from Y. In fact, X may remove some aspects of Y or present them in a way that makes them more manageable.
Second, that X is in some sense easier to grasp than Y, assuming that the things that X removed from Y are not important to the matter at hand.

通常我们说X是一个比Y更好的抽象，代表我们认为X在Y的基础上不会给人带来额外的信息，X只是比Y更好理解而已

As [Nietzsche](http://oregonstate.edu/instruct/phl201/modules/Philosophers/Nietzsche/Truth_and_Lie_in_an_Extra-Moral_Sense.htm) wrote:

> Every concept originates through our equating what is unequal. No leaf ever wholly equals another, and the concept "leaf" is formed through an arbitrary abstraction from these individual differences, through forgetting the distinctions; and now it gives rise to the idea that in nature there might be something besides the leaves which would be "leaf" - some kind of original form after which all leaves have been woven, marked, copied, colored, curled, and painted, but by unskilled hands, so that no copy turned out to be a correct, reliable, and faithful image of the original form.

正如尼采所言：
 我们将不同事物的共性抽象成为一个概念。例如没有一片叶子和其余的叶子完全相同。。。

Abstractions, fundamentally, are fake. Every situation is unique, as is every node. But abstractions make the world manageable: simpler problem statements - free of reality - are much more analytically tractable and provided that we did not ignore anything essential, the solutions are widely applicable.

事实上，在分布式系统中，每一个节点、每一种情况都是不一样的，抽象其实在根本上是不存在的。但是一个抽象能够让一些问题能够被陈述出来，更利于理解和处理，并且由于关注的是问题的本质，所以解决方案往往应用会更广泛

Indeed, if the things that we kept around are essential, then the results we can derive will be widely applicable. This is why impossibility results are so important: they take the simplest possible formulation of a problem, and demonstrate that it is impossible to solve within some set of constraints or assumptions.

All abstractions ignore something in favor of equating things that are in reality unique. The trick is to get rid of everything that is not essential. How do you know what is essential? Well, you probably won't know a priori.

Every time we exclude some aspect of a system from our specification of the system, we risk introducing a source of error and/or a performance issue. That's why sometimes we need to go in the other direction, and selectively introduce some aspects of real hardware and the real-world problem back. It may be sufficient to reintroduce some specific hardware characteristics (e.g. physical sequentiality) or other physical characteristics to get a system that performs well enough.

With this in mind, what is the least amount of reality we can keep around while still working with something that is still recognizable as a distributed system? A system model is a specification of the characteristics we consider important; having specified one, we can then take a look at some impossibility results and challenges.

例如不可能的结果就是一种好的抽象：由于对于问题采用最简单的公式来描述，并且证明不可能存在一组约束或假设能解决这个问题。

## A system model 系统模型

A key property of distributed systems is distribution. More specifically, programs in a distributed system:

- run concurrently on independent nodes ...
- are connected by a network that may introduce nondeterminism and message loss ...
- and have no shared memory or shared clock.

分布式系统最重要的一个属性就是：**分布**，程序在分布式系统上需要满足：

- **在独立的节点上同时运行**
- **网络连接可能会带来不确定性和丢失信息**
- **不共享内存和时钟**

There are many implications:

- each node executes a program concurrently
- knowledge is local: nodes have fast access only to their local state, and any information about global state is potentially out of date
- nodes can fail and recover from failure independently
- messages can be delayed or lost (independent of node failure; it is not easy to distinguish network failure and node failure)
- and clocks are not synchronized across nodes (local timestamps do not correspond to the global real time order, which cannot be easily observed)

具体而言：

- **每个节点会同时执行程序**
- **信息局部存在：局部节点之间相互访问快，但是关于全局的信息可能会过期**
- **节点之间的失败和恢复能够相互独立**
- **信息可能会出现延迟或丢失（网络失败与节点失败难以辨别）**
- **节点之间的时钟不同步（局部时间戳和全局时间顺序不吻合，并且难以被观察到）**

A system model enumerates the many assumptions associated with a particular system design.

>System model 系统模型
>a set of assumptions about the environment and facilities on which a distributed system is implemented 
分布式系统实施过程中的一系列关于环境与设置的假设

System models vary in their assumptions about the environment and facilities. These assumptions include:

- what capabilities the nodes have and how they may fail
- how communication links operate and how they may fail and
- properties of the overall system, such as assumptions about time and order

这些假设包括：

- **节点容量及怎样算失败**
- **通信交互操作如何进行及怎样算失败**
- **总体系统的属性，如：时间和顺序**

A robust system model is one that makes the weakest assumptions: any algorithm written for such a system is very tolerant of different environments, since it makes very few and very weak assumptions.

On the other hand, we can create a system model that is easy to reason about by making strong assumptions. For example, assuming that nodes do not fail means that our algorithm does not need to handle node failures. However, such a system model is unrealistic and hence hard to apply into practice.

弱假设会使得系统具有更高的鲁棒性，强假设会使系统具有更高的理解性和可推理性*

Let's look at the properties of nodes, links and time and order in more detail.

### Nodes in our system model 分布式系统中的节点

Nodes serve as hosts for computation and storage. They have:

- the ability to execute a program
- the ability to store data into volatile memory (which can be lost upon failure) and into stable state (which can be read after a failure)
- a clock (which may or may not be assumed to be accurate)

节点用来计算和存储，它拥有：

- **执行程序的能力**
- **存储大量数据的能力（数据可能会丢失，但也可以恢复）**
- **时钟（可能不是很精确）**

Nodes execute deterministic algorithms: the local computation, the local state after the computation, and the messages sent are determined uniquely by the message received and local state when the message was received.

节点执行时满足：当消息被接收到时，本地状态与消息决定了本地运算、本地运算后的状态以及消息发送的唯一性

There are many possible failure models which describe the ways in which nodes can fail. In practice, most systems assume a crash-recovery failure model: that is, nodes can only fail by crashing, and can (possibly) recover after crashing at some later point.

有许多故障模型会描述能够容许节点发生什么样类型的失败。实际上，大部分系统都是一个**崩溃恢复失效模型**：节点只能由于崩溃而失败,并且在接下来的时间节点能够被恢复

Another alternative is to assume that nodes can fail by misbehaving in any arbitrary way. This is known as [Byzantine fault tolerance](http://en.wikipedia.org/wiki/Byzantine_fault_tolerance). Byzantine faults are rarely handled in real world commercial systems, because algorithms resilient to arbitrary faults are more expensive to run and more complex to implement. I will not discuss them here.

另一种故障模型是**拜占庭容错**：节点可以能以任意方式发生故障，其容错过于复杂昂贵，现实中很少处理。

### Communication links in our system model 系统模型中的通信链路

Communication links connect individual nodes to each other, and allow messages to be sent in either direction. Many books that discuss distributed algorithms assume that there are individual links between each pair of nodes, that the links provide FIFO (first in, first out) order for messages, that they can only deliver messages that were sent, and that sent messages can be lost.

通信链路使得各个分布的节点之间能相互连接，并传输信息。

Some algorithms assume that the network is reliable: that messages are never lost and never delayed indefinitely. This may be a reasonable assumption for some real-world settings, but in general it is preferable to consider the network to be unreliable and subject to message loss and delays.

A network partition occurs when the network fails while the nodes themselves remain operational. When this occurs, messages may be lost or delayed until the network partition is repaired. Partitioned nodes may be accessible by some clients, and so must be treated differently from crashed nodes. The diagram below illustrates a node failure vs. a network partition:

当节点仍在操作时，网络发生失败会导致网络分区，信息将会丢失或者发生延迟，直到网络修复。但此时用户仍然能够访问节点。所以网络失败必须要和节点失败区分开来。下同阐明了两种情况：

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190424215207.png)

It is rare to make further assumptions about communication links. We could assume that links only work in one direction, or we could introduce different communication costs (e.g. latency due to physical distance) for different links. However, these are rarely concerns in commercial environments except for long-distance links (WAN latency) and so I will not discuss them here; a more detailed model of costs and topology allows for better optimization at the cost of complexity.

### Timing / ordering assumptions 时间/顺序假设

One of the consequences of physical distribution is that each node experiences the world in a unique manner. This is inescapable, because information can only travel at the speed of light. If nodes are at different distances from each other, then any messages sent from one node to the others will arrive at a different time and potentially in a different order at the other nodes.

分布式中，节点之间存在距离，信息传递一定存在通信时间，并且每个节点接收到信息的时刻不一致

Timing assumptions are a convenient shorthand for capturing assumptions about the extent to which we take this reality into account. The two main alternatives are:

- **Synchronous system model** 

  Processes execute in lock-step; there is a known upper bound on message transmission delay; each process has an accurate clock

- **Asynchronous system model**

  No timing assumptions - e.g. processes execute at independent rates; there is no bound on message transmission delay; useful clocks do not exist

两种关于时间假设选择的模型：

- **同步系统模型**
  程序锁步执行，消息传播延迟有明确的上限，每一步都有精确的时钟
- **异步系统模型**
  没有时间假设，每个进程执行在相互独立的比率下，消息传播没有延迟上限，时钟不存在

The synchronous system model imposes many constraints on time and order. It essentially assumes that the nodes have the same experience: that messages that are sent are always received within a particular maximum transmission delay, and that processes execute in lock-step. This is convenient, because it allows you as the system designer to make assumptions about time and order, while the asynchronous system model doesn't.

同步系统有许多时间与顺序的限制，默认节点有相同的体验，消息传播延迟在一定的范围内，程序执行严格按照锁步。但在异步系统中，时间的依赖不存在

Asynchronicity is a non-assumption: it just assumes that you can't rely on timing (or a "time sensor").

It is easier to solve problems in the synchronous system model, because assumptions about execution speeds, maximum message transmission delays and clock accuracy all help in solving problems since you can make inferences based on those assumptions and rule out inconvenient failure scenarios by assuming they never occur.

Of course, assuming the synchronous system model is not particularly realistic. Real-world networks are subject to failures and there are no hard bounds on message delay. Real world systems are at best partially synchronous: they may occasionally work correctly and provide some upper bounds, but there will be times where messages are delayed indefinitely and clocks are out of sync. I won't really discuss algorithms for synchronous systems here, but you will probably run into them in many other introductory books because they are analytically easier (but unrealistic).

因此对于同步系统来说，解决问题更容易一些，因为你能通过对于时间和顺序的假设，来推测和排除失败场景发生在哪些地方。

但通常而言，同步系统模型能更好解释和理解，但不现实

### The consensus problem 一致性问题

During the rest of this text, we'll vary the parameters of the system model. Next, we'll look at how varying two system properties:

- whether or not network partitions are included in the failure model, and
- synchronous vs. asynchronous timing assumptions

influence the system design choices by discussing two impossibility results (FLP and CAP).

接下来讨论两个系统中的属性差异：

- **失败模型中是否包含网络分区**
- **同步异步时间假设**

会对系统设计造成什么样的影响，以及会带来的两种结果（FLP与CAP）

Of course, in order to have a discussion, we also need to introduce a problem to solve. The problem I'm going to discuss is the [consensus problem](http://en.wikipedia.org/wiki/Consensus_%28computer_science%29).

Several computers (or nodes) achieve consensus if they all agree on some value. More formally:

1. Agreement: Every correct process must agree on the same value.
2. Integrity: Every correct process decides at most one value, and if it decides some value, then it must have been proposed by some process.
3. Termination: All processes eventually reach a decision.
4. Validity: If all correct processes propose the same value V, then all correct processes decide V.

The consensus problem is at the core of many commercial distributed systems. After all, we want the reliability and performance of a distributed system without having to deal with the consequences of distribution (e.g. disagreements / divergence between nodes), and solving the consensus problem makes it possible to solve several related, more advanced problems such as atomic broadcast and atomic commit.

首先解释什么是**一致性问题**：

1.  **一致性（Agreement]）**：每个正确的进程对某个值达成共识

2.  **完整性（Integrity）**：每个正确的进程最多决定一个值，一旦决定了某个值，那么这个值一定被其它进程接受

3. **终止性（Termination）**：所有进程最终达成一致同意某个值

4.  **合法性（Validity）**：如果所有正确的节点进程提出相同的值V，那么所有正确的节点进程达成一致，承认值V

共识问题是许多商业分布式系统的核心，解决共识问题可以解决几个相关的、更高级的问题，如原子广播和原子提交

### Two impossibility results 两个不可能结果

The first impossibility result, known as the FLP impossibility result, is an impossibility result that is particularly relevant to people who design distributed algorithms. The second - the CAP theorem - is a related result that is more relevant to practitioners; people who need to choose between different system designs but who are not directly concerned with the design of algorithms.

- **FLP定理**
  分布式算法需要关注
- **CAP定理**
  实践者需要关注（不关心算法，关心选择什么样的系统设计）

## The FLP impossibility result

I will only briefly summarize the [FLP impossibility result](http://en.wikipedia.org/wiki/Consensus_%28computer_science%29#Solvability_results_for_some_agreement_problems), though it is considered to be [more important](http://en.wikipedia.org/wiki/Dijkstra_Prize) in academic circles. The FLP impossibility result (named after the authors, Fischer, Lynch and Patterson) examines the consensus problem under the asynchronous system model (technically, the agreement problem, which is a very weak form of the consensus problem). It is assumed that nodes can only fail by crashing; that the network is reliable, and that the typical timing assumptions of the asynchronous system model hold: e.g. there are no bounds on message delay.

Under these assumptions, the FLP result states that "there does not exist a (deterministic) algorithm for the consensus problem in an asynchronous system subject to failures, even if messages can never be lost, at most one process may fail, and it can only fail by crashing (stopping executing)".

This result means that there is no way to solve the consensus problem under a very minimal system model in a way that cannot be delayed forever.  The argument is that if such an algorithm existed, then one could devise an execution of that algorithm in which it would remain undecided ("bivalent") for an arbitrary amount of time by delaying message delivery - which is allowed in the asynchronous system model. Thus, such an algorithm cannot exist.

This impossibility result is important because it highlights that assuming the asynchronous system model leads to a tradeoff: algorithms that solve the consensus problem must either give up safety or liveness when the guarantees regarding bounds on message delivery do not hold.

This insight is particularly relevant to people who design algorithms, because it imposes a hard constraint on the problems that we know are solvable in the asynchronous system model. The CAP theorem is a related theorem that is more relevant to practitioners: it makes slightly different assumptions (network failures rather than node failures), and has more clear implications for practitioners choosing between system designs.

### FLP

**FLP定理：在异步通信场景，即使只有一个进程失败了，没有任何算法能保证非失败进程能够达到一致性**（在假设网络可靠、节点只会因崩溃而失效的最小化异步模型系统中，仍然不存在一个可以解决一致性问题的确定性算法。FLP只是证明了异步通信的最坏情况，实际上根据FLP定理，异步网络中是无法完全同时保证 safety 和 liveness 的一致性算法，但如果我们 safety 或 liveness 要求，这个算法进入无法表决通过的无限死循环的概率是非常低的。）

因为异步系统中的消息传播具有延迟性，那么如果存在这样一个算法，这个算法会在任何的时间内保持着不确定信，无法保证达成一致性，与假设矛盾。因此通常异步系统模型需要进行折中处理：放弃一定的安全性当消息传播延迟上限不能确定时

## The CAP theorem

The CAP theorem was initially a conjecture made by computer scientist Eric Brewer. It's a popular and fairly useful way to think about tradeoffs in the guarantees that a system design makes. It even has a [formal proof](http://www.comp.nus.edu.sg/~gilbert/pubs/BrewersConjecture-SigAct.pdf) by [Gilbert](http://www.comp.nus.edu.sg/~gilbert/biblio.html) and [Lynch](http://en.wikipedia.org/wiki/Nancy_Lynch) and no, [Nathan Marz](http://nathanmarz.com/) didn't debunk it, in spite of what [a particular discussion site](http://news.ycombinator.com/) thinks.

The theorem states that of these three properties:

- Consistency: all nodes see the same data at the same time.
- Availability: node failures do not prevent survivors from continuing to operate.
- Partition tolerance: the system continues to operate despite message loss due to network and/or node failure

### CAP

**CAP定理：一个分布式系统不可能同时满足consistency、availability、partition tolerance这三个基本需求，最多同时满足两个**

-  **consistency一致性**：所有节点同一时刻看到相同数据
-  **availability可用性**：节点失败不阻止其他正在运行的节点的工作
-  **partition tolerance分区容错**：即使出现信息丢失或网络、节点失败，系统也能继续运行（通过复制）

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190424221433.png)

only two can be satisfied simultaneously. We can even draw this as a pretty diagram, picking two properties out of three gives us three types of systems that correspond to different intersections:

这三种性质进行俩俩组合，得到下面三种情况

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190424221652.png)

Note that the theorem states that the middle piece (having all three properties) is not achievable. Then we get three different system types:

- CA (consistency + availability). Examples include full strict quorum protocols, such as two-phase commit.
- CP (consistency + partition tolerance). Examples include majority quorum protocols in which minority partitions are unavailable such as Paxos.
- AP (availability + partition tolerance). Examples include protocols using conflict resolution, such as Dynamo.

- **CA：完全严格的仲裁协议**例如2PC（两阶段提交协议，第一阶段投票，第二阶段事物提交）
- **CP：不完全（多数）仲裁协议**，例如Paxos、Raft
- **AP：使用冲突解决的协议**，例如Dynamo、Gossip

The CA and CP system designs both offer the same consistency model: strong consistency. The only difference is that a CA system cannot tolerate any node failures; a CP system can tolerate up to `f` faults given `2f+1` nodes in a non-Byzantine failure model (in other words, it can tolerate the failure of a minority `f` of the nodes as long as majority `f+1` stays up). The reason is simple:

- A CA system does not distinguish between node failures and network failures, and hence must stop accepting writes everywhere to avoid introducing divergence (multiple copies). It cannot tell whether a remote node is down, or whether just the network connection is down: so the only safe thing is to stop accepting writes.
- A CP system prevents divergence (e.g. maintains single-copy consistency) by forcing asymmetric behavior on the two sides of the partition. It only keeps the majority partition around, and requires the minority partition to become unavailable (e.g. stop accepting writes), which retains a degree of availability (the majority partition) and still ensures single-copy consistency.

CA和CP系统设计遵循的都是强一致性理论。不同的是CA系统不能容忍节点发生故障。CP系统能够容忍2f+1个节点中有f个节点发生失败。原因如下：

- CA系统无法辨别是网络故障还是节点故障，因此一旦发生失败，系统为了避免带来数据分歧，会立马阻止写操作（不能判别，所以安全的办法就是stop）
- CP系统通过强制性分开两侧分区的不对称行为来阻止发生分歧。仅仅保证主要的分区工作，并且要求最少的分区是不可用的。最终能够使得主要的分区能够运行工作，保证一定程度上的可用性，确保单拷贝一致性

I'll discuss this in more detail in the chapter on replication when I discuss Paxos. The important thing is that CP systems incorporate network partitions into their failure model and distinguish between a majority partition and a minority partition using an algorithm like Paxos, Raft or viewstamped replication. CA systems are not partition-aware, and are historically more common: they often use the two-phase commit algorithm and are common in traditional distributed relational databases.

重要的一点是，CP系统中将网络分区考虑到了它的故障模型中，并且用算法识别主要分区和小分区。CA系统不关注分区

Assuming that a partition occurs, the theorem reduces to a binary choice between availability and consistency.

当我们假设分区一定发生时，那么在可用性和一致性中怎样进行一个选择呢

![](https://raw.githubusercontent.com/JayChenFE/pic/master/20190424222016.png)

I think there are four conclusions that should be drawn from the CAP theorem:

First, that *many system designs used in early distributed relational database systems did not take into account partition tolerance* (e.g. they were CA designs). Partition tolerance is an important property for modern systems, since network partitions become much more likely if the system is geographically distributed (as many large systems are).

第一,早期的分布式系统没有考虑分区容错（CA设计）。但是分区容错是一个很重要的性质，因为一旦系统规模很大，网络分区是不可避免的

Second, that *there is a tension between strong consistency and high availability during network partitions*. The CAP theorem is an illustration of the tradeoffs that occur between strong guarantees and distributed computation.

第二,P存在时  强一致性和高可用性之间存在矛盾.CAP理论说明了在P存在时权衡A和C

In some sense, it is quite crazy to promise that a distributed system consisting of independent nodes connected by an unpredictable network "behaves in a way that is indistinguishable from a non-distributed system".

Strong consistency guarantees require us to give up availability during a partition. This is because one cannot prevent divergence between two replicas that cannot communicate with each other while continuing to accept writes on both sides of the partition.

强一致性会导致当存在分区时系统不可用。因为当两个复制集之间不能相互通信时，无法将写操作告诉对方，因此会带来分歧

How can we work around this? By strengthening the assumptions (assume no partitions) or by weakening the guarantees. Consistency can be traded off against availability (and the related capabilities of offline accessibility and low latency). If "consistency" is defined as something less than "all nodes see the same data at the same time" then we can have both availability and some (weaker) consistency guarantee.

那么该怎样避免这种情况发生呢。一致性和可用性之间应该怎样进行折中处理？这个时候就需要将强一致性进行弱化，转成弱一致性，来使得我们再保证弱一致性的情况下，也能保证系统的可用性

Third, that *there is a tension between strong consistency and performance in normal operation*.

第三,在一般操作中,强一致性和性能之间有矛盾

Strong consistency / single-copy consistency requires that nodes communicate and agree on every operation. This results in high latency during normal operation.

强一致/单拷贝一致性 性需要节点之间的通讯时间,所以会有高延迟

If you can live with a consistency model other than the classic one, a consistency model that allows replicas to lag or to diverge, then you can reduce latency during normal operation and maintain availability in the presence of partitions.

如果舍弃传统的一致性模型,可以通过复制来减少延迟

When fewer messages and fewer nodes are involved, an operation can complete faster. But the only way to accomplish that is to relax the guarantees: let some of the nodes be contacted less frequently, which means that nodes can contain old data.

允许某些节点存在旧数据

This also makes it possible for anomalies to occur. You are no longer guaranteed to get the most recent value. Depending on what kinds of guarantees are made, you might read a value that is older than expected, or even lose some updates.

可能会导致异常,可能会读到旧数据,甚至丢失某些更新

Fourth - and somewhat indirectly - that *if we do not want to give up availability during a network partition, then we need to explore whether consistency models other than strong consistency are workable for our purposes*.

第四,不直接的一点,如果不想降低可用性（在网络分区的情况下），就应该找到另一个一致性模型而不是继续锁定强一致性

For example, even if user data is georeplicated to multiple datacenters, and the link between those two datacenters is temporarily out of order, in many cases we'll still want to allow the user to use the website / service. This means reconciling two divergent sets of data later on, which is both a technical challenge and a business risk. But often both the technical challenge and the business risk are manageable, and so it is preferable to provide high availability.

例如：当数据复制到不同的机器上，并且节点间的联系发生故障时，需要协调两方的数据。这个存在一定的技术挑战和商业风险，当两者都能解决时，此时系统还是高可用性的

Consistency and availability are not really binary choices, unless you limit yourself to strong consistency. But strong consistency is just one consistency model: the one where you, by necessity, need to give up availability in order to prevent more than a single copy of the data from being active. As [Brewer himself points out](http://www.infoq.com/articles/cap-twelve-years-later-how-the-rules-have-changed), the "2 out of 3" interpretation is misleading.

If you take away just one idea from this discussion, let it be this: "consistency" is not a singular, unambiguous property. Remember:

>   [ACID](http://en.wikipedia.org/wiki/ACID) consistency != <br>
>    [CAP](http://en.wikipedia.org/wiki/CAP_theorem) consistency != <br>
>    [Oatmeal](http://en.wikipedia.org/wiki/Oatmeal) consistency


Instead, a consistency model is a guarantee - any guarantee - that a data store gives to programs that use it.

>Consistency model
>a contract between programmer and system, wherein the system guarantees that if the programmer follows some specific rules, the results of operations on the data store will be predictable

The "C" in CAP is "strong consistency", but "consistency" is not a synonym for "strong consistency".

CAP中的一致性指的是强一致性，但一致性并不代表强一致性**，一致性没有单一、明确的属性

Let's take a look at some alternative consistency models.

### Strong consistency vs. other consistency models

### 一致性模型（强一致性和其它一致性）

Consistency models can be categorized into two types: strong and weak consistency models:

- Strong consistency models (capable of maintaining a single copy)
  - Linearizable consistency
  - Sequential consistency
- Weak consistency models (not strong)
  - Client-centric consistency models
  - Causal consistency: strongest model available
  - Eventual consistency models

Strong consistency models guarantee that the apparent order and visibility of updates is equivalent to a non-replicated system. Weak consistency models, on the other hand, do not make such guarantees.

Note that this is by no means an exhaustive list. Again, consistency models are just arbitrary contracts between the programmer and system, so they can be almost anything.

### Strong consistency models

Strong consistency models can further be divided into two similar, but slightly different consistency models:

- *Linearizable consistency*: Under linearizable consistency, all operations **appear** to have executed atomically in an order that is consistent with the global real-time ordering of operations. (Herlihy & Wing, 1991)
- *Sequential consistency*: Under sequential consistency, all operations **appear** to have executed atomically in some order that is consistent with the order seen at individual nodes and that is equal at all nodes. (Lamport, 1979)

一致性模型能被分为两类：强一致性模型和弱一致性模型

- **强一致性模型（数据维持一份）**
  - 线性一致
  - 顺序一致
- **弱一致性模型**
  - 客户为中心的一致性模型
  -  因果一致性可用最强模型
  -  最终一致性模型

The key difference is that linearizable consistency requires that the order in which operations take effect is equal to the actual real-time ordering of operations. Sequential consistency allows for operations to be reordered as long as the order observed on each node remains consistent. The only way someone can distinguish between the two is if they can observe all the inputs and timings going into the system; from the perspective of a client interacting with a node, the two are equivalent.

The difference seems immaterial, but it is worth noting that sequential consistency does not compose.

Strong consistency models allow you as a programmer to replace a single server with a cluster of distributed nodes and not run into any problems.

All the other consistency models have anomalies (compared to a system that guarantees strong consistency), because they behave in a way that is distinguishable from a non-replicated system. But often these anomalies are acceptable, either because we don't care about occasional issues or because we've written code that deals with inconsistencies after they have occurred in some way.

Note that there really aren't any universal typologies for weak consistency models, because "not a strong consistency model" (e.g. "is distinguishable from a non-replicated system in some way") can be almost anything.

强一致性模型保证数据更新的表现和顺序像无复制系统一样，弱一致性模型则不保证

强一致性模型两种有轻微差别的形式：

-  **线性一致性**：所有操作都是有序进行的，按照全局真实时间操作顺序
-  **顺序一致性**：所有操作也是有序进行的，任一单独节点操作顺序与其它节点一致

不同的是，线性一致性模型要求操作按照全局真实时间顺序来，而顺序一致性模型只要节点操作被观察到的顺序是一致的就行

强一致性模型能够允许你的单服务程序一直到分布式节点集群上并且不会发生任何错误

相比于强一致性模型，其它的一致性模型都会存在一些异常，但是它们允许异常发生，或者写了处理异常的代码

### Client-centric consistency models 客户为中心的一致性模型

*Client-centric consistency models* are consistency models that involve the notion of a client or session in some way. For example, a client-centric consistency model might guarantee that a client will never see older versions of a data item. This is often implemented by building additional caching into the client library, so that if a client moves to a replica node that contains old data, then the client library returns its cached value rather than the old value from the replica.

Clients may still see older versions of the data, if the replica node they are on does not contain the latest version, but they will never see anomalies where an older version of a value resurfaces (e.g. because they connected to a different replica). Note that there are many kinds of consistency models that are client-centric.

客户为中心的一致性模型是一种以某种方式涉及客户或会话概念的模型。举例来说，一个以客户为中心的一致性模型可以保证单个用户看到的数据版本永远是最新的版本。通常通过建立缓存到客户端库，使得如果一个用户移动到一个包含了老版本数据的复制的节点时，通过客户端库能够返回数据的缓存值而不是复制集的老版本。

如果复制节点没有包含最新的数据版本，客户仍然会看到数据的老版本，但是客户永远不会看到旧版本的值重现的异常情况。

### Eventual consistency 最终一致性

The *eventual consistency* model says that if you stop changing values, then after some undefined amount of time all replicas will agree on the same value. It is implied that before that time results between replicas are inconsistent in some undefined manner. Since it is [trivially satisfiable](http://www.bailis.org/blog/safety-and-liveness-eventual-consistency-is-not-safe/) (liveness property only), it is useless without supplemental information.

最终一致性模型中，当停止改变数值的一段不确定的时间后，所有的复制集将会最终保持一致。这表明，在这段时间之前，复制集在某种情形下是不一致的。由于这个条件非常容易满足（只用保证liveness），因此没有补充信息它是没有用的。**经过一定的时间，各个复制集最终保持一致**

Saying something is merely eventually consistent is like saying "people are eventually dead". It's a very weak constraint, and we'd probably want to have at least some more specific characterization of two things:

说一个事情最终能够保证一致就像是说"人总有一死",这是一个很弱的约束条件。这里至少需要加一些比较明确的特征来形容它：

First, how long is "eventually"? It would be useful to have a strict lower bound, or at least some idea of how long it typically takes for the system to converge to the same value.

Second, how do the replicas agree on a value? A system that always returns "42" is eventually consistent: all replicas agree on the same value. It just doesn't converge to a useful value since it just keeps returning the same fixed value. Instead, we'd like to have a better idea of the method. For example, one way to decide is to have the value with the largest timestamp always win.

**have a strict lower bound**
 首先，“最终保持一致”的最终到底是多久？这个需要有一个明确严格的上限，或者至少要知道通常系统达到一致结果时所需要的时间

 **replicas agree on a value**
 其次，复制集最终是如何到达一致的？一个总是返回固定值“42”的系统能保证最终一致性：所有的复制集都为同一值。但这个系统仅仅是范围一个固定的数值，并非收敛于同一个有用值。这不是我们想要的。我们希望能有一个使得复制集最终保持一致的好方法，例如，使用最大时间限的值作为最终值。**这里系统需要达到一致的目标，而不是固定的返回某一个值**

So when vendors say "eventual consistency", what they mean is some more precise term, such as "eventually last-writer-wins, and read-the-latest-observed-value in the meantime" consistency. The "how?" matters, because a bad method can lead to writes being lost - for example, if the clock on one node is set incorrectly and timestamps are used.

所以，当提到”最终一致“时，它实际上想表达的是更精确的术语，类似于想表达”最后的写操作为最终值，与此同时，读操作读取最新的数据“这样的一致性。这里我们最需要关注的是”如何到达最终一致“中的”如何“，因为一个坏的方法可能会使得写操作的数据丢失，例如：如果某个节点上的时钟是不正确的，但是它的时间戳却仍然被使用，这个时候可能会造成写失误。

I will look into these two questions in more detail in the chapter on replication methods for weak consistency models.

关于这两点，在复制方法章节会进行更加详细地介绍。

------

## Further reading

- [Brewer's Conjecture and the Feasibility of Consistent, Available, Partition-Tolerant Web Services](http://www.comp.nus.edu.sg/~gilbert/pubs/BrewersConjecture-SigAct.pdf) - Gilbert & Lynch, 2002
- [Impossibility of distributed consensus with one faulty process](http://scholar.google.com/scholar?q=Impossibility+of+distributed+consensus+with+one+faulty+process) - Fischer, Lynch and Patterson, 1985
- [Perspectives on the CAP Theorem](http://scholar.google.com/scholar?q=Perspectives+on+the+CAP+Theorem) - Gilbert & Lynch, 2012
- [CAP Twelve Years Later: How the "Rules" Have Changed](http://www.infoq.com/articles/cap-twelve-years-later-how-the-rules-have-changed) - Brewer, 2012
- [Uniform consensus is harder than consensus](http://scholar.google.com/scholar?q=Uniform+consensus+is+harder+than+consensus) - Charron-Bost & Schiper, 2000
- [Replicated Data Consistency Explained Through Baseball](http://pages.cs.wisc.edu/~remzi/Classes/739/Papers/Bart/ConsistencyAndBaseballReport.pdf) - Terry, 2011
- [Life Beyond Distributed Transactions: an Apostate's Opinion](http://scholar.google.com/scholar?q=Life+Beyond+Distributed+Transactions%3A+an+Apostate%27s+Opinion) - Helland, 2007
- [If you have too much data, then 'good enough' is good enough](http://dl.acm.org/citation.cfm?id=1953140) - Helland, 2011
- [Building on Quicksand](http://scholar.google.com/scholar?q=Building+on+Quicksand) - Helland & Campbell, 2009

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

Lamport clocks and vector clocks are replacements for physical clocks which rely on counters and communication to determine the order of events across a distributed system. These clocks provide a counter that is comparable across different nodes.

*A Lamport clock* is simple. Each process maintains a counter using the following rules:

- Whenever a process does work, increment the counter
- Whenever a process sends a message, include the counter
- When a message is received, set the counter to `max(local_counter, received_counter) + 1`

Expressed as code:

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

This is known as clock consistency condition: if one event comes before another, then that event's logical clock comes before the others. If `a` and `b` are from the same causal history, e.g. either both timestamp values were produced on the same process; or `b` is a response to the message sent in `a` then we know that `a` happened before `b`.

Intuitively, this is because a Lamport clock can only carry information about one timeline / history; hence, comparing Lamport timestamps from systems that never communicate with each other may cause concurrent events to appear to be ordered when they are not.

Imagine a system that after an initial period divides into two independent subsystems which never communicate with each other.

For all events in each independent system, if a happened before b, then `ts(a) < ts(b)`; but if you take two events from the different independent systems (e.g. events that are not causally related) then you cannot say anything meaningful about their relative order.  While each part of the system has assigned timestamps to events, those timestamps have no relation to each other. Two events may appear to be ordered even though they are unrelated.

However - and this is still a useful property - from the perspective of a single machine, any message sent with `ts(a)` will receive a response with `ts(b)` which is `> ts(a)`.

*A vector clock* is an extension of Lamport clock, which maintains an array `[ t1, t2, ... ]` of N logical clocks - one per each node. Rather than incrementing a common counter, each node increments its own logical clock in the vector by one on each internal event. Hence the update rules are:

- Whenever a process does work, increment the logical clock value of the node in the vector
- Whenever a process sends a message, include the full vector of logical clocks
- When a message is received:
  - update each element in the vector to be `max(local, received)`
  - increment the logical clock value representing the current node in the vector

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

This illustration ([source](http://en.wikipedia.org/wiki/Vector_clock)) shows a vector clock:

![from http://en.wikipedia.org/wiki/Vector_clock](D:/git_home/download/distsysbook/input/images/vector_clock.svg.png)

Each of the three nodes (A, B, C) keeps track of the vector clock. As events occur, they are timestamped with the current value of the vector clock. Examining a vector clock such as `{ A: 2, B: 4, C: 1 }` lets us accurately identify the messages that (potentially) influenced that event.

The issue with vector clocks is mainly that they require one entry per node, which means that they can potentially become very large for large systems. A variety of techniques have been applied to reduce the size of vector clocks (either by performing periodic garbage collection, or by reducing accuracy by limiting the size).

We've looked at how order and causality can be tracked without physical clocks. Now, let's look at how time durations can be used for cutoff.

## Failure detectors (time for cutoff)

As I stated earlier, the amount of time spent waiting can provide clues about whether a system is partitioned or merely experiencing high latency. In this case, we don't need to assume a global clock of perfect accuracy - it is simply enough that there is a reliable-enough local clock.

Given a program running on one node, how can it tell that a remote node has failed? In the absence of accurate information, we can infer that an unresponsive remote node has failed after some reasonable amount of time has passed.

But what is a "reasonable amount"? This depends on the latency between the local and remote nodes. Rather than explicitly specifying algorithms with specific values (which would inevitably be wrong in some cases), it would be nicer to deal with a suitable abstraction.

A failure detector is a way to abstract away the exact timing assumptions. Failure detectors are implemented using heartbeat messages and timers. Processes exchange heartbeat messages. If a message response is not received before the timeout occurs, then the process suspects the other process.

A failure detector based on a timeout will carry the risk of being either overly aggressive (declaring a node to have failed) or being overly conservative (taking a long time to detect a crash). How accurate do failure detectors need to be for them to be usable?

[Chandra et al.](http://www.google.com/search?q=Unreliable%20Failure%20Detectors%20for%20Reliable%20Distributed%20Systems) (1996) discuss failure detectors in the context of solving consensus - a problem that is particularly relevant since it underlies most replication problems where the replicas need to agree in environments with latency and network partitions.

They characterize failure detectors using two properties, completeness and accuracy:

<dl>
  <dt>Strong completeness.</dt>
  <dd>Every crashed process is eventually suspected by every correct process.</dd>
  <dt>Weak completeness.</dt>
  <dd>Every crashed process is eventually suspected by some correct process.</dd>
  <dt>Strong accuracy.</dt>
  <dd>No correct process is suspected ever.</dd>
  <dt>Weak accuracy.</dt>
  <dd>Some correct process is never suspected.</dd>
</dl>

Completeness is easier to achieve than accuracy; indeed, all failure detectors of importance achieve it - all you need to do is not to wait forever to suspect someone. Chandra et al. note that a failure detector with weak completeness can be transformed to one with strong completeness (by broadcasting information about suspected processes), allowing us to concentrate on the spectrum of accuracy properties.

Avoiding incorrectly suspecting non-faulty processes is hard unless you are able to assume that there is a hard maximum on the message delay. That assumption can be made in a synchronous system model - and hence failure detectors can be strongly accurate in such a system. Under system models that do not impose hard bounds on message delay, failure detection can at best be eventually accurate.

Chandra et al. show that even a very weak failure detector - the eventually weak failure detector ⋄W (eventually weak accuracy + weak completeness) - can be used to solve the consensus problem. The diagram below (from the paper) illustrates the relationship between system models and problem solvability:

![From Chandra and Toueg. Unreliable failure detectors for reliable distributed systems. JACM 43(D:/git_home/download/distsysbook/input/images/chandra_failure_detectors.png):225–267, 1996.](images/chandra_failure_detectors.png)

As you can see above, certain problems are not solvable without a failure detector in asynchronous systems. This is because without a failure detector (or strong assumptions about time bounds e.g. the synchronous system model), it is not possible to tell whether a remote node has crashed, or is simply experiencing high latency. That distinction is important for any system that aims for single-copy consistency: failed nodes can be ignored because they cannot cause divergence, but partitioned nodes cannot be safely ignored.

How can one implement a failure detector? Conceptually, there isn't much to a simple failure detector, which simply detects failure when a timeout expires. The most interesting part relates to how the judgments are made about whether a remote node has failed.

Ideally, we'd prefer the failure detector to be able to adjust to changing network conditions and to avoid hardcoding timeout values into it. For example, Cassandra uses an [accrual failure detector](https://www.google.com/search?q=The+Phi+accrual+failure+detector), which is a failure detector that outputs a suspicion level (a value between 0 and 1) rather than a binary "up" or "down" judgment. This allows the application using the failure detector to make its own decisions about the tradeoff between accurate detection and early detection.

## Time, order and performance

Earlier, I alluded to having to pay the cost for order. What did I mean?

If you're writing a distributed system, you presumably own more than one computer. The natural (and realistic) view of the world is a partial order, not a total order. You can transform a partial order into a total order, but this requires communication, waiting and imposes restrictions that limit how many computers can do work at any particular point in time.

All clocks are mere approximations bound by either network latency (logical time) or by physics. Even keeping a simple integer counter in sync across multiple nodes is a challenge.

While time and order are often discussed together, time itself is not such a useful property. Algorithms don't really care about time as much as they care about more abstract properties:

- the causal ordering of events
- failure detection (e.g. approximations of upper bounds on message delivery)
- consistent snapshots (e.g. the ability to examine the state of a system at some point in time; not discussed here)

Imposing a total order is possible, but expensive. It requires you to proceed at the common (lowest) speed. Often the easiest way to ensure that events are delivered in some defined order is to nominate a single (bottleneck) node through which all operations are passed.

Is time / order / synchronicity really necessary? It depends. In some use cases, we want each intermediate operation to move the system from one consistent state to another. For example, in many cases we want the responses from a database to represent all of the available information, and we want to avoid dealing with the issues that might occur if the system could return an inconsistent result.

But in other cases, we might not need that much time / order / synchronization. For example, if you are running a long running computation, and don't really care about what the system does until the very end - then you don't really need much synchronization as long as you can guarantee that the answer is correct.

Synchronization is often applied as a blunt tool across all operations, when only a subset of cases actually matter for the final outcome. When is order needed to guarantee correctness? The CALM theorem - which I will discuss in the last chapter - provides one answer.

In other cases, it is acceptable to give an answer that only represents the best known estimate - that is, is based on only a subset of the total information contained in the system. In particular, during a network partition one may need to answer queries with only a part of the system being accessible. In other use cases, the end user cannot really distinguish between a relatively recent answer that can be obtained cheaply and one that is guaranteed to be correct and is expensive to calculate. For example, is the Twitter follower count for some user X, or X+1? Or are movies A, B and C the absolutely best answers for some query? Doing a cheaper, mostly correct "best effort" can be acceptable.

In the next two chapters we'll examine replication for fault-tolerant strongly consistent systems - systems which provide strong guarantees while being increasingly resilient to failures. These systems provide solutions for the first case: when you need to guarantee correctness and are willing to pay for it. Then, we'll discuss systems with weak consistency guarantees, which can remain available in the face of partitions, but that can only give you a "best effort" answer.

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