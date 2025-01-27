﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Enums
{
    /// <summary>
    /// 通知操作類型
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 不通知
        /// </summary>
        No=0,
        /// <summary>
        /// 異常通知
        /// </summary>
        Error=1,
        /// <summary>
        /// 通知所有
        /// </summary>
        All = 2
    }
}
