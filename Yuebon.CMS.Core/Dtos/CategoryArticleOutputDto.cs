using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.CMS.Dtos
{
    [Serializable]
    public class CategoryArticleOutputDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///  子標題
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// 父級ID
        /// </summary>
        public string ParentId { get; set; }

        public List<CategoryArticleOutputDto> Children { get; set; }
    }
}
