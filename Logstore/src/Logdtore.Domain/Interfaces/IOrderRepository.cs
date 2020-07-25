using Logstore.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logstore.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task Insert(Order order);
        Task Update(Order order);
        Task Delete(long id);
        Task<IEnumerable<T>> Query<T>(string query, object parameters = null);
    }
}
