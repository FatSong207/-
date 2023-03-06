using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;

namespace Yuebon.Commons.Models
{
    /// <summary>
    /// 所有數據庫視圖對應實體類必須繼承此類
    /// </summary>
    [Serializable]
    public abstract class BaseViewModel : IEntity
    {

    }
}
