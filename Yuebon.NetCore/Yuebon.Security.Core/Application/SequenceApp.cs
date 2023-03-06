using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.Core.App;
using Yuebon.Commons.IoC;
using Yuebon.Commons.Models;
using Yuebon.Security.IServices;

namespace Yuebon.Security.Application
{
    /// <summary>
    /// 業務編碼規則
    /// </summary>
    public class  SequenceApp
    {
        ISequenceService iService = App.GetService<ISequenceService>();
        static object locker = new object(); 
        /// <summary>
        /// 獲取新的業務單據編碼
        /// </summary>
        /// <param name="name">單據編碼規則名稱</param>
        public  string GetSequenceNext(string name)
        {
            lock (locker)
            {
                CommonResult commonResult = new CommonResult();
                commonResult = iService.GetSequenceNext(name);
                if (commonResult.Success)
                {
                    return commonResult.ResData.ToString();
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
