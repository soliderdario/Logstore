using Logstore.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logstore.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task Insert(Customer flavor);
        Task Update(Customer flavor);
        Task Delete(long id);
        Task<IEnumerable<T>> Query<T>(string query, object parameters = null);
    }
}
