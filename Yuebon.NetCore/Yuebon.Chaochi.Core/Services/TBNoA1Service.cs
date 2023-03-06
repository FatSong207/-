using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Services;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class TBNoA1Service : BaseService<TBNoA1, TBNoA1, string>, ITBNoA1Service
    {
        private readonly ITBNoA1Repository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userLogOnRepository"></param>
        /// <param name="logService"></param>
        /// <param name="roleService"></param>
        /// <param name="organizeService"></param>
        public TBNoA1Service(ITBNoA1Repository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}