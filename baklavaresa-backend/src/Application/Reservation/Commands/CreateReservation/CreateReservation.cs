using Domain.Repositories;

namespace Application.Reservation.Commands.CreateReservation;

public record CreateReservationCommand(string FirstName, string LastName, string Email, DateTime Schedule) : IRequest<Unit>;

public class CreateReservationCommandHandler(IReservationRepository reservationRepository) : IRequestHandler<CreateReservationCommand, Unit>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    public Task<Unit> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = new Domain.Entities.Reservation(request.FirstName, request.LastName, request.Email, request.Schedule);
        _reservationRepository.Create(reservation);
        return Unit.Task;
    }
}