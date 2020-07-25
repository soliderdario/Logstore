using System.ComponentModel.DataAnnotations.Schema;

namespace Logdtore.Domain.Model
{
    public class ModelBase
    {        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; internal set; }
    }
}
