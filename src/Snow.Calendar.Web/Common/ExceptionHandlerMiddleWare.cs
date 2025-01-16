using Snow.Calendar.Web.Model;
using System.Net;
using System.Text.Json;
using System.Xml.Serialization;
using Snow.Calendar.Common;
using Snow.Calendar.Common.Model;

namespace Snow.Calendar.Web.Common
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class ExceptionHandlerMiddleWare
    {
        private readonly ILogger<ExceptionHandlerMiddleWare> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleWare(RequestDelegate next, ILogger<ExceptionHandlerMiddleWare> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null) return;
            await WriteExceptionAsync(context, exception);
        }

        private async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            //记录日志
            logger.LogError(exception, "发生异常：" + exception.Message);

            //返回友好的提示
            var response = context.Response;

            //状态码
            if (exception is UserFriendlyException)
                response.StatusCode = (int)HttpStatusCode.BadRequest;

            response.ContentType = context.Request.Headers["Accept"];

            if (response.ContentType?.ToLower() == "application/xml")
            {
                await response.WriteAsync(Object2XmlString(new Response()
                {
                    Code = 0,
                    Message = exception.GetBaseException().Message
                })).ConfigureAwait(false);
            }
            else
            {
                response.ContentType = "application/json";
                await response.WriteAsync(JsonSerializer.Serialize(new Response()
                {
                    Code = 0,
                    Message = exception.GetBaseException().Message
                })).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 对象转为Xml
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static string Object2XmlString(object o)
        {
            StringWriter sw = new StringWriter();
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                serializer.Serialize(sw, o);
            }
            catch
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Dispose();
            }
            return sw.ToString();
        }
    }
}