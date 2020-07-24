using Logdtore.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logdtore.Domain
{
    public interface IFlavorRepository
    {
        Task<long> Insert(Flavor flavor);
        Task Update(Flavor flavor);
        Task Delete(long id);
        Task<List<Flavor>> Query<Flavor>(string query, object parameters = null);
    }
}
