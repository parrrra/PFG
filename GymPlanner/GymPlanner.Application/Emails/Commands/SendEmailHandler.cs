using GymPlanner.Common.CQRS;

namespace GymPlanner.Application.Emails.Commands;

public class SendEmailHandler : ICommandHandler<SendEmailCommand, bool>
{
    private readonly IEmailSender _emailSender;

    public SendEmailHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task<bool> Handle(SendEmailCommand command, CancellationToken cancellation)
    {
        await _emailSender.SendAsync(command.To, command.Subject, command.Body);
        return true;
    }
}
