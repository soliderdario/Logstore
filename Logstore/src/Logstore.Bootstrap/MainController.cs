using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AutoMapper;
using Logstore.Infrastructure.Notifiers;

namespace Logstore.Bootstrap
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotifier _notifier;
        protected readonly IMapper _mapper;

        public MainController(
            INotifier notifier,
            IMapper mapper)
        {
            _notifier = notifier;
            _mapper = mapper;
        }

        protected bool IsValid()
        {
            return !_notifier.HasNotification();
        }

        protected void NotifyError(string message)
        {
            _notifier.SetNotification(new Notification(message));
        }

        protected void NotifyError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var message = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(message);
            }
        }
        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyError(modelState);
            return CustomResponse();
        }
    }
}
