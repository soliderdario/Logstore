using Logdtore.Domain.View;
using Logstore.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logstore.Domain.Interfaces
{
    public interface IOrderRepository
    {        
        Task Save(OrderNoCustomerView orderView);
        Task Save(OrderYesCustomerView orderView);
        Task Delete(long id);
        Task<IEnumerable<T>> Query<T>(string query, object parameters = null);
    }
}
