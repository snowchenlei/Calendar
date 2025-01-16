namespace Snow.Calendar.Common
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