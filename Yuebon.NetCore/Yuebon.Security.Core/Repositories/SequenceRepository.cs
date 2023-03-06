using System;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.Repositories
{
    /// <summary>
    /// 鍗曟嵁緙栫爜浠撳偍鎺ュ彛鐨勫疄鐜?    /// </summary>
    public class SequenceRepository : BaseRepository<Sequence, string>, ISequenceRepository
    {
		public SequenceRepository()
        {
        }

        public SequenceRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}