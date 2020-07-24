using System.Collections.Generic;

namespace Logstore.Infrastructure.Notifiers
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void SetNotification(Notification notification);
    }
}
