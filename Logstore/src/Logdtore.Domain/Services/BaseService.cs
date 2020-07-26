using FluentValidation;
using FluentValidation.Results;
using Logdtore.Domain.Model;
using Logstore.Domain.View;
using Logstore.Infrastructure.Notifiers;

namespace Logstore.Domain.Services
{
    public class BaseService
    {
        private readonly INotifier _notifier;

        public BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string mensagem)
        {
            _notifier.SetNotification(new Notification(mensagem));
        }

        public bool ModelValidation<TV, TE>(TV validation, TE entidade) where TV : AbstractValidator<TE> where TE : ModelBase
        {
            var validator = validation.Validate(entidade);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }

        public bool ViewValidation<TV, TE>(TV validation, TE entidade) where TV : AbstractValidator<TE> where TE : ViewBase
        {
            var validator = validation.Validate(entidade);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }

    }
}

