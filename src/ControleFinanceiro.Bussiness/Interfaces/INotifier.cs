using ControleFinanceiro.Bussiness.Notifications;

namespace ControleFinanceiro.Bussiness.Interfaces;

public interface INotifier
{
    void Handle(Notification notification);
    List<Notification> GetNotifications();
    bool HasNotification();
}
