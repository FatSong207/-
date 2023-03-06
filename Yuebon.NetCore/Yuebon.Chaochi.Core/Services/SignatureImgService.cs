using Yuebon.Chaochi.Core.Models;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Commons.Services;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class SignatureImgService : BaseService<SignatureImg, SignatureImg, string>, ISignatureImgService
    {
        private readonly ISignatureImgRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userLogOnRepository"></param>
        /// <param name="logService"></param>
        /// <param name="roleService"></param>
        /// <param name="organizeService"></param>
        public SignatureImgService(ISignatureImgRepository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}