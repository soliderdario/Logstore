namespace Logdtore.Domain.Model
{
    public class Customer:Base
    {        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string UF { get; set; }
    }
}
