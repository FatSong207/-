using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Core.Dtos
{
    public class UpdateEventCostIncomePersonnelDto
    {
        public List<EventCost> eventCosts { get; set; }

        public List<string> eventPersonnels { get; set; }

        /// <summary>
        /// 合計費用
        /// </summary>
        public virtual int? TotalCost { get; set; }

        /// <summary>
        /// 合計收入
        /// </summary>
        public virtual int? TotalIncome { get; set; }

        /// <summary>
        /// 收支小計
        /// </summary>
        public virtual int? SubTotal { get; set; }

        /// <summary>
        /// 人員數量
        /// </summary>
        public virtual int? TotalPerson { get; set; }

    }
}
