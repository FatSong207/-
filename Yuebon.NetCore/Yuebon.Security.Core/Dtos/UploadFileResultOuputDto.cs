using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Models;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 上傳結束輸出文件對象
    /// </summary>
    [Serializable]
    public class UploadFileResultOuputDto : IOutputDto
    {
        /// <summary>
	    ///
	    /// </summary>
        public string Id { get; set; }
        /// <summary>
	    /// 文件名稱
	    /// </summary>
        public string FileName { get; set; }
        /// <summary>
	    /// 文件路徑物理路徑
	    /// </summary>
        public string PhysicsFilePath { get; set; }
        /// <summary>
	    /// 縮略圖
	    /// </summary>
        public string Thumbnail { get; set; }
        /// <summary>
	    /// 文件路徑
	    /// </summary>
        public string FilePath { get; set; }
       
        /// <summary>
	    /// 文件大小
	    /// </summary>
        public long FileSize { get; set; }
        /// <summary>
	    /// 文件類型
	    /// </summary>
        public string FileType { get; set; }
        

    }
}
