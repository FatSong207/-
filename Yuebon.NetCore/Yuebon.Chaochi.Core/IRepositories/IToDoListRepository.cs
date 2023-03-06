using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義待辦事項倉儲接口
    /// </summary>
    public interface IToDoListRepository:IRepository<ToDoList, string>
    {
    }
}