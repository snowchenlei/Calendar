﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snow.Calendar.Web.Model
{
    /// <summary>
    /// 响应结果
    /// </summary>
    public class Response
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 响应结果
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class Response<T> : Response
    {
        /// <summary>
        /// 具体数据
        /// </summary>
        public T Data { get; set; }
    }
}