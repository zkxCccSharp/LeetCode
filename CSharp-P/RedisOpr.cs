using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_P
{
    internal class RedisOpr
    {
        public static void Opr()
        {
            RedisCacheOptions redisCacheOptions = new RedisCacheOptions();
            redisCacheOptions.InstanceName = "redisT1";
            redisCacheOptions.Configuration = "a";
            RedisCache redisCache = new RedisCache(redisCacheOptions);
            redisCache.Set("keyt1",new byte[] { 2,3,4,5,1});
            object str= redisCache.Get("keyt1");
            if (str != null)
            {
                Console.WriteLine(str);
            }
        }
    }
}
