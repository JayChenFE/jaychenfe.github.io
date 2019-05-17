---
title: [译]Distributed systems for fun and profit_2抽象描述系统的特征
date: 2019-04-22 17:32:04
categories: [微服务理论文章阅读学习]
tags:
top:
---

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
>分布式系统实施过程中的一系列关于环境与设置的假设

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

3.  **终止性（Termination）**：所有进程最终达成一致同意某个值

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
>   [CAP](http://en.wikipedia.org/wiki/CAP_theorem) consistency != <br>
>   [Oatmeal](http://en.wikipedia.org/wiki/Oatmeal) consistency


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
  - 因果一致性可用最强模型
  - 最终一致性模型

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