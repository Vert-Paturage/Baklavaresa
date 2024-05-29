using Domain.Entities;
using Domain.Repositories;

namespace Application.Reservation.Commands.CreateReservation;

public record CreateReservationCommand(
    string FirstName,
    string LastName,
    string Email,
    DateTime Date,
    int NumberOfPeople,
    Table Table
    ) : IRequest<Unit>;

public class CreateReservationCommandHandler(IReservationRepository reservationRepository) : IRequestHandler<CreateReservationCommand, Unit>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    public Task<Unit> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = new Domain.Entities.Reservation(request.FirstName, request.LastName, request.Email,
            request.Date, request.NumberOfPeople, request.Table);
        _reservationRepository.Create(reservation);
        return Unit.Task;
    }
}