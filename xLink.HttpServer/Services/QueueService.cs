using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NewLife.Caching;

namespace xLinkServer.Services
{
    /// <summary>队列服务</summary>
    public interface IQueueService
    {
        ICache Cache { get; set; }

        /// <summary>发布消息</summary>
        /// <param name="topic">主题</param>
        /// <param name="value">消息</param>
        /// <returns></returns>
        Int32 Public(String topic, String value);

        /// <summary>订阅</summary>
        /// <param name="clientId">客户标识</param>
        /// <param name="topic">主题</param>
        Boolean Subscribe(String clientId, String topic);

        /// <summary>取消订阅</summary>
        /// <param name="clientId">客户标识</param>
        /// <param name="topic">主题</param>
        Boolean UnSubscribe(String clientId, String topic);

        /// <summary>消费消息</summary>
        /// <param name="clientId">客户标识</param>
        /// <param name="topic">主题</param>
        /// <param name="count">要拉取的消息数</param>
        /// <returns></returns>
        String[] Consume(String clientId, String topic, Int32 count);
    }

    /// <summary>队列服务</summary>
    public class QueueService : IQueueService
    {
        #region 属性
        public ICache Cache { get; set; } = MemoryCache.Default;

        private readonly ConcurrentDictionary<String, IList<String>> _topics = new ConcurrentDictionary<String, IList<String>>();
        #endregion

        #region 方法
        /// <summary>发布消息</summary>
        /// <param name="topic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Int32 Public(String topic, String value)
        {
            //var queue = Cache.GetQueue<String>(topic);
            //return queue.Add(new[] { value });

            var rs = 0;
            if (_topics.TryGetValue(topic, out var list))
            {
                foreach (var item in list)
                {
                    var key = $"{topic}_{item}";
                    var queue = Cache.GetQueue<String>(key);
                    rs += queue.Add(new[] { value });
                }
            }

            return rs;
        }

        /// <summary>订阅</summary>
        /// <param name="clientId">客户标识</param>
        /// <param name="topic">主题</param>
        public Boolean Subscribe(String clientId, String topic)
        {
            var list = _topics.GetOrAdd(topic, k => new List<String>());
            if (!list.Contains(clientId)) list.Add(clientId);

            return true;
        }

        /// <summary>取消订阅</summary>
        /// <param name="clientId">客户标识</param>
        /// <param name="topic">主题</param>
        public Boolean UnSubscribe(String clientId, String topic)
        {
            if (_topics.TryGetValue(topic, out var list))
            {
                if (list.Contains(clientId))
                {
                    list.Remove(clientId);
                    return true;
                }
            }

            return false;
        }

        /// <summary>消费消息</summary>
        /// <param name="topic"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public String[] Consume(String clientId, String topic, Int32 count)
        {
            //var queue = Cache.GetQueue<String>(topic);
            //return queue.Take(count).ToArray();

            if (_topics.TryGetValue(topic, out var list))
            {
                if (list.Contains(clientId))
                {
                    var key = $"{topic}_{clientId}";
                    var queue = Cache.GetQueue<String>(key);
                    return queue.Take(count).ToArray();
                }
            }

            return Array.Empty<String>();
        }
        #endregion
    }
}