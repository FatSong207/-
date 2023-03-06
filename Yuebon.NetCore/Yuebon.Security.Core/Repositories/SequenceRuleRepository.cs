using System;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.Repositories
{
    /// <summary>
    /// 搴忓彿緙栫爜瑙勫垯琛ㄤ粨鍌ㄦ帴鍙ｇ殑瀹炵幇
    /// </summary>
    public class SequenceRuleRepository : BaseRepository<SequenceRule, string>, ISequenceRuleRepository
    {
		public SequenceRuleRepository()
        {
        }

        public SequenceRuleRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}