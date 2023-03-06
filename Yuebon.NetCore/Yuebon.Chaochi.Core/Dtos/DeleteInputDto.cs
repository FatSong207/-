using System;

namespace Yuebon.Chaochi.Core.Dtos
{
    [Serializable]
    public class DeleteInputDto
    {
        /// <summary>
        /// 主鍵Id集合
        /// </summary>
        public dynamic[] Ids { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public string Status { get; set; }
    }
}
