# 物联网平台
物联网平台为各种设备提供快速接入能力，支持Http/WebSocket/MQTT等多种标准协议以及自定义协议，提供设备管理、认证鉴权、指令下发等能力。
演示地址：http://feifan.link  

## 系统架构
物联网平台由物联服务平台、物联管理平台和物联网关三部分组成，支持集群部署以支撑海量设备接入。

1, 授权服务器  
2, 设备服务器  
3, 用户服务器  
4, Web端  
5, 手机APP  
6, 硬件设备  

### 通信协议
基于新生命团队标准网络封包协议  
标准网络封包协议：`1 Flag + 1 Sequence + 2 Length + N Payload`  
1个字节标识位，标识请求、响应、错误、加密、压缩等；  
1个字节序列号，用于请求响应包配对；  
2个字节数据长度N，小端字节序，指示后续负载数据长度（不包含头部4个字节），解决粘包问题；  
N个字节负载数据，数据内容完全由业务决定，最大长度65535=64k。  

### 指令格式
默认采用Json序列化，utf-8编码  

标准指令例子如下：  
请求，00013400{action:"Login",args:{user:"Stone",pass:"password"}}  
响应，80011E00{result:true,data:"大石头"}  
头部4字节，第一个字节最高位区分请求响应，第二个字节序列化，第三第四字节是长度，小字节序  

## 新生命开源项目矩阵
各项目默认支持net4.5/net4.0/netstandard2.0  

|                               项目                               | 年份  |  状态  | .NET Core | 说明                                                |
| :--------------------------------------------------------------: | :---: | :----: | :-------: | --------------------------------------------------- |
|                             基础组件                             |       |        |           | 支撑其它中间件以及产品项目                          |
|          [NewLife.Core](https://github.com/NewLifeX/X)           | 2002  | 维护中 |     √     | 算法、日志、网络、RPC、序列化、缓存、多线程         |
|              [XCode](https://github.com/NewLifeX/X)              | 2005  | 维护中 |     √     | 数据中间件，MySQL、SQLite、SqlServer、Oracle        |
|      [NewLife.Net](https://github.com/NewLifeX/NewLife.Net)      | 2005  | 维护中 |     √     | 网络库，千万级吞吐率，学习gRPC、Thrift              |
|     [NewLife.Cube](https://github.com/NewLifeX/NewLife.Cube)     | 2010  | 维护中 |     √     | Web魔方，企业级快速开发框架，集成OAuth              |
|    [NewLife.Agent](https://github.com/NewLifeX/NewLife.Agent)    | 2008  | 维护中 |     √     | 服务管理框架，Windows服务、Linux的Systemd           |
|                              中间件                              |       |        |           | 对接各知名中间件平台                                |
|    [NewLife.Redis](https://github.com/NewLifeX/NewLife.Redis)    | 2017  | 维护中 |     √     | Redis客户端，微秒级延迟，百亿级项目验证             |
| [NewLife.RocketMQ](https://github.com/NewLifeX/NewLife.RocketMQ) | 2018  | 维护中 |     √     | 支持Apache RocketMQ和阿里云消息队列，十亿级项目验证 |
|     [NewLife.MQTT](https://github.com/NewLifeX/NewLife.MQTT)     | 2019  | 维护中 |     √     | 物联网消息协议，客户端支持阿里云物联网              |
|     [NewLife.LoRa](https://github.com/NewLifeX/NewLife.LoRa)     | 2016  | 维护中 |     √     | 超低功耗的物联网远程通信协议LoRaWAN                 |
|   [NewLife.Thrift](https://github.com/NewLifeX/NewLife.Thrift)   | 2019  | 维护中 |     √     | Thrift协议实现                                      |
|     [NewLife.Hive](https://github.com/NewLifeX/NewLife.Hive)     | 2019  | 维护中 |     √     | 纯托管读写Hive，Hadoop数据仓库，基于Thrift协议      |
|             [NoDb](https://github.com/NewLifeX/NoDb)             | 2017  | 开发中 |     √     | NoSQL数据库，百万级kv读写性能，持久化               |
|      [NewLife.Ftp](https://github.com/NewLifeX/NewLife.Ftp)      | 2008  | 维护中 |     √     | Ftp客户端实现                                       |
|                             产品平台                             |       |        |           | 产品平台级，编译部署即用，个性化自定义              |
|           [AntJob](https://github.com/NewLifeX/AntJob)           | 2019  | 维护中 |     √     | 蚂蚁调度系统，大数据实时计算平台                    |
|         [Stardust](https://github.com/NewLifeX/Stardust)         | 2018  | 维护中 |     √     | 星尘，微服务平台，分布式平台                        |
|            [XLink](https://github.com/NewLifeX/XLink)            | 2016  | 维护中 |     √     | 物联网云平台                                        |
|           [XProxy](https://github.com/NewLifeX/XProxy)           | 2005  | 维护中 |     √     | 产品级反向代理                                      |
|          [XScript](https://github.com/NewLifeX/XScript)          | 2010  | 维护中 |     ×     | C#脚本引擎                                          |
|          [SmartOS](https://github.com/NewLifeX/SmartOS)          | 2014  | 维护中 |   C++11   | 嵌入式操作系统，完全独立自主，ARM Cortex-M芯片架构  |
|         [GitCandy](https://github.com/NewLifeX/GitCandy)         | 2015  | 维护中 |     ×     | Git管理系统                                         |
|                               其它                               |       |        |           |                                                     |
|           [XCoder](https://github.com/NewLifeX/XCoder)           | 2006  | 维护中 |     √     | 码神工具，开发者必备                                |
|        [XTemplate](https://github.com/NewLifeX/XTemplate)        | 2008  | 维护中 |     √     | 模版引擎，T4(Text Template)语法                     |
|       [X组件 .NET2.0](https://github.com/NewLifeX/X_NET20)       | 2002  | 存档中 |  .NET2.0  | 日志、网络、RPC、序列化、缓存、Windows服务、多线程  |

#### 新生命开发团队  
网站：http://www.NewLifeX.com  
QQ群：1600800  

## 项目位置
https://git.newlifex.com/NewLife/xLink
