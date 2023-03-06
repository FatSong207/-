namespace Yuebon.Commons.Options
{
    /// <summary>
    /// 代碼生成器配置
    /// </summary>
    public class CodeGenerateOption
    {

        /// <summary>
        /// 項目命名空間
        /// </summary>
        public string BaseNamespace { get; set; }
        /// <summary>
        /// 數據實體命名空間
        /// </summary>
        public string ModelsNamespace { get; set; }
        /// <summary>
        /// 輸入輸出數據實體名稱空間
        /// </summary>
        public string DtosNamespace { get; set; }
        /// <summary>
        /// 倉儲接口命名空間
        /// </summary>
        public string IRepositoriesNamespace { get; set; }
        /// <summary>
        /// 倉儲實現名稱空間
        /// </summary>
        public string RepositoriesNamespace { get; set; }
        /// <summary>
        /// 服務接口命名空間
        /// </summary>
        public string IServicsNamespace { get; set; }
        /// <summary>
        /// 服務接口實現命名空間
        /// </summary>
        public string ServicesNamespace { get; set; }

        /// <summary>
        /// Api控制器命名空間
        /// </summary>
        public string ApiControllerNamespace { get; set; }

        /// <summary>
        /// 去掉的表頭字符
        /// </summary>
        public string ReplaceTableNameStr { get; set; }
        /// <summary>
        /// 要生數據的表，用“，”分割
        /// </summary>
        public string TableList { get; set; }
    }
}
