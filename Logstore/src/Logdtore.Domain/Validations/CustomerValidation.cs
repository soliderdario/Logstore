using FluentValidation;
using Logstore.Domain.Model;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Logstore.Domain.Validations
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(c => EmailIsValid(c.Email))
                .Equal(true)
                .WithMessage("O campo preço precisa ser preenchido");
        }        

        public static bool EmailIsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
