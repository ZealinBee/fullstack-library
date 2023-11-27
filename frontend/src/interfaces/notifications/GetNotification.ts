export interface GetNotification {
    notificationId: string;
    notificationMessage: string;
    notificationType: string;
    userId: string;
    notificationData: Record<string, string>;
}