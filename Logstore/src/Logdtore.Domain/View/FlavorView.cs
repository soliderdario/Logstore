using System.ComponentModel.DataAnnotations;

namespace Logdtore.Domain.View
{

    public class FlavorView 
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public double Price { get; set; }
    }
}
