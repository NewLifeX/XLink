using NewLife.Caching;
using NewLife.Collections;
using System;
using System.Collections.Generic;

namespace xLinkServer.Common
{
    public class TokenSession
    {
        /// <summary>有效期</summary>
        public TimeSpan Expire { get; set; } = TimeSpan.FromMinutes(20);

        /// <summary>会话存储</summary>
        public ICache Cache { get; set; } = new MemoryCache { Expire = 20 * 60, Period = 60 * 10 };

        /// <summary>创建新的Session</summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IDictionary<String, Object> CreateSession(String token)
        {
            var key = GetKey(token);
            var dic = Cache.GetDictionary<Object>(key);
            Cache.SetExpire(key, Expire);

            //!! 临时修正可空字典的BUG
            if (Cache is MemoryCache mc)
            {
                dic = new NullableDictionary<String, Object>();
                mc.Set(key, dic);
            }

            return dic;
        }

        /// <summary>根据Token获取session</summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IDictionary<String, Object> GetSession(String token)
        {
            if (token.IsNullOrEmpty()) return null;

            var key = GetKey(token);
            // 当前缓存没有指定的token 则直接返回null
            if (!Cache.ContainsKey(key)) return null;

            // 采用哈希结构。内存缓存用并行字段，Redis用Set
            var dic = Cache.GetDictionary<Object>(key);
            Cache.SetExpire(key, Expire);

            return dic;
        }

        /// <summary>
        /// 刷新token有效期
        /// </summary>
        /// <param name="token"></param>
        /// <param name="newToken"></param>
        public IDictionary<String, Object> CopySession(String token, String newToken)
        {
            if (newToken.IsNullOrEmpty()) return null;

            var dic = GetSession(token);
            if (dic != null)
            {
                var nkey = GetKey(newToken);
                if (Cache is MemoryCache mc)
                {
                    mc.Set(nkey, dic, Expire);
                }
                else
                {
                    var ndic = CreateSession(newToken);
                    foreach (var item in dic)
                    {
                        ndic[item.Key] = item.Value;
                    }
                }
            }

            // 确保建立新的
            return GetSession(newToken);
        }

        /// <summary>根据令牌活期缓存Key</summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected virtual String GetKey(String token) => (!token.IsNullOrEmpty() && token.Length > 16) ? token.MD5() : token;
    }
}
