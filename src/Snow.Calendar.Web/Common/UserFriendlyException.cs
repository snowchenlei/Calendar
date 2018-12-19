using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snow.Calendar.Web.Common
{
    /// <summary>
    /// 友好提示异常
    /// </summary>
    public class UserFriendlyException : Exception
    {
        /// <summary>
        /// 支持消息的构造
        /// </summary>
        /// <param name="message"></param>
        public UserFriendlyException(string message) : base(message)
        {
        }
    }
}