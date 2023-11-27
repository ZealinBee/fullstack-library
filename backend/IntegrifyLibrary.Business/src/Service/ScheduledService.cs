using IntegrifyLibrary.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace IntegrifyLibrary.Business;
public class ScheduledService : BackgroundService
{
    // Checks for overdue loans every day, and sends notifications to users if they have overdue    loans
    private readonly PeriodicTimer _timer = new (TimeSpan.FromDays(1));
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public ScheduledService(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var loanService = scope.ServiceProvider.GetRequiredService<ILoanService>();
                var reservationService = scope.ServiceProvider.GetRequiredService<IReservationService>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                var loans = await loanService.GetAll(new QueryOptions());
                foreach (var loan in loans)
                {
                    if(!loan.IsReturned && loan.DueDate < DateOnly.FromDateTime(DateTime.Now) && !loan.IsOverdue) {
                        var updateLoanDto = _mapper.Map<UpdateLoanDto>(await loanService.GetOne(loan.LoanId));
                        updateLoanDto.IsOverdue = true;
                        await loanService.UpdateOne(loan.LoanId, updateLoanDto);
                        var notification = new NotificationDto();
                        notification.UserId = loan.UserId;
                        notification.NotificationMessage = "You have an overdue loan";
                        notification.NotificationType = "OverdueLoan";
                        notification.NotificationData.Add("LoanId", loan.LoanId.ToString());
                        await notificationService.CreateOne(notification);
                    }
                    if(!loan.IsReturned && loan.DueDate == DateOnly.FromDateTime(DateTime.Now.AddDays(1)) && !loan.IsOverdue) {
                        var notification = new NotificationDto();
                        notification.UserId = loan.UserId;
                        notification.NotificationMessage = "Your loan is due tomorrow, please return it on time";
                        notification.NotificationType = "OverdueLoan";
                        notification.NotificationData.Add("LoanId", loan.LoanId.ToString());
                        await notificationService.CreateOne(notification);
                    }
                }
            }
            // delay for 24 hours
            // await Task.Delay(86400000, stoppingToken);
        }
    }
}
