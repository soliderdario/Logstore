using System.ComponentModel.DataAnnotations.Schema;

namespace Logdtore.Domain.Model
{
    [Table("Flavor")]
    public class Flavor:ModelBase
    {       
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
