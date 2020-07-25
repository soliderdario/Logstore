using System.Collections.Generic;
using System.Threading.Tasks;
using Logdtore.Domain.Model;

namespace Logstore.Domain.Interfaces
{
    public interface IFlavorRepository
    {
        Task Insert(Flavor flavor);
        Task Update(Flavor flavor);
        Task Delete(long id);
        Task<IEnumerable<T>> Query<T>(string query, object parameters = null);
    }
}
