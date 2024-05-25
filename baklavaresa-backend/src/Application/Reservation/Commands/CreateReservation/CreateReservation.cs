namespace Application.Reservation.Commands.CreateReservation;

public record CreateReservationCommand: IRequest<int>
{
    public string FirstName { get; init; } 
    public string LastName { get; init; }
    public string Email { get; init; }
}

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, int>
{
    private readonly IDatabase
    public Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Reservation reservation = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };
        
    }
}