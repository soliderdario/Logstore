using Logdtore.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logdtore.Domain.View
{

    public class FlavorView 
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
