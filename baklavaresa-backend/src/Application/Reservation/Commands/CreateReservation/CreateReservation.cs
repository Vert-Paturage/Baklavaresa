using Domain.Repositories;

namespace Application.Reservation.Commands.CreateReservation;

public record CreateReservationCommand(string FirstName, string LastName, string Email) : IRequest<Unit>;

public class CreateReservationCommandHandler(IReservationRepository reservationRepository) : IRequestHandler<CreateReservationCommand, Unit>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    public Task<Unit> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Reservation reservation = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };
        // WARNING: MEUCH ??!
        _reservationRepository.Create(reservation);
        return Unit.Task;
    }
}