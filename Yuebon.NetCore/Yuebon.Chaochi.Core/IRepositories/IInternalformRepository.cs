using System.Threading.Tasks;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.IRepositories;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInternalformRepository : IRepository<Internalform, string>
    {

        Task<Internalform> GetByFormId(string FormId);

    }
}