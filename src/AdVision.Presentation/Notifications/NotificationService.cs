using Microsoft.Toolkit.Uwp.Notifications;

namespace AdVision.Presentation.Notifications;

public class NotificationService : INotificationService
{
    public void ShowSuccess(string title, string message)
        => Show("✅ " + title, message, NotificationType.Success);

    public void ShowError(string title, string message)
        => Show("❌ " + title, message, NotificationType.Error);

    public void ShowWarning(string title, string message)
        => Show("⚠️ " + title, message, NotificationType.Warning);

    public void ShowInfo(string title, string message)
        => Show("ℹ️ " + title, message, NotificationType.Info);

    public static void Show(string title, string message, NotificationType type)
    {
        var builder = new ToastContentBuilder()
            .AddArgument("notificationType", type.ToString())
            .AddText(title)
            .AddText(message);

        AddAudio(builder, type);

        builder.Show();
    }

    private static void AddAudio(ToastContentBuilder builder, NotificationType type)
    {
        _ = type switch
        {
            NotificationType.Success => builder.AddAudio(new Uri("ms-winsoundevent:Notification.Default")),
            NotificationType.Error => builder.AddAudio(new Uri("ms-winsoundevent:Notification.Looping.Alarm2")),
            NotificationType.Warning => builder.AddAudio(new Uri("ms-winsoundevent:Notification.Reminder")),
            NotificationType.Info => builder.AddAudio(new Uri("ms-winsoundevent:Notification.IM")),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}