using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.Injector;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Snow.Calendar.Web.Interceptor
{
    /// <summary>
    /// 缓存代理类
    /// </summary>
    public class CacheInterceptorAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 缓存
        /// </summary>
        [FromContainer]
        public IMemoryCache _memoryCache { get; set; }

        /// <summary>
        /// 日志
        /// </summary>
        [FromContainer]
        public ILogger _logger { get; set; }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                //使用方法的命名空间和参数作为key
                String cacheKey = context.ImplementationMethod.ReflectedType != null ?
                    $"{context.ImplementationMethod.ReflectedType.FullName}{context.ImplementationMethod.Name}({JsonConvert.SerializeObject(context.Parameters)})" :
                    $"{context.ImplementationMethod.Name}({JsonConvert.SerializeObject(context.Parameters)})";

                if (_memoryCache.TryGetValue(cacheKey, out object o))
                {
                    context.ReturnValue = o;
                    return;
                }

                await next(context);

                var cacheValue = context.ReturnValue;
                _memoryCache.Set(cacheKey, cacheValue);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.ToString());
                throw;
            }
        }
    }
}