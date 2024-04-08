using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

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
        [FromServiceContext]
        public IMemoryCache MemoryCache { get; set; } = default!;

        /// <summary>
        /// 日志
        /// </summary>
        [FromServiceContext]
        public ILogger Logger { get; set; } = default!;

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                //使用方法的命名空间和参数作为key
                String cacheKey = context.ImplementationMethod.ReflectedType != null ?
                    $"{context.ImplementationMethod.ReflectedType.FullName}{context.ImplementationMethod.Name}({JsonSerializer.Serialize(context.Parameters)})" :
                    $"{context.ImplementationMethod.Name}({JsonSerializer.Serialize(context.Parameters)})";

                if (MemoryCache.TryGetValue(cacheKey, out object? o))
                {
                    context.ReturnValue = o;
                    return;
                }

                await next(context);

                var cacheValue = context.ReturnValue;
                MemoryCache.Set(cacheKey, cacheValue);
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e.ToString());
                throw;
            }
        }
    }
}