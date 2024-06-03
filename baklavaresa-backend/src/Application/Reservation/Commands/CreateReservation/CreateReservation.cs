using Domain.Entities;
using Domain.Repositories;

namespace Application.Reservation.Commands.CreateReservation;

public record CreateReservationCommand(
    string FirstName,
    string LastName,
    string Email,
    DateTime Date,
    int NumberOfPeople
    ) : IRequest<Unit>;

public class CreateReservationCommandHandler(IReservationRepository reservationRepository, ITableRepository tableRepository) : IRequestHandler<CreateReservationCommand, Unit>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly ITableRepository _tableRepository = tableRepository;
    public Task<Unit> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        if (request.Date < DateTime.Now)
        {
            throw new Domain.Exceptions.Reservation.InvalidReservationDateException(request.Date);
        }
        if (request.NumberOfPeople <= 0)
        {
            throw new Domain.Exceptions.Reservation.InvalidNumberOfPeopleException(request.NumberOfPeople);
        }
        var table = _tableRepository.GetTablesByCapacity(request.NumberOfPeople).Result.FirstOrDefault();
        if (table == null)
        {
            throw new Domain.Exceptions.Table.NoTablesAvailableException(request.Date, request.NumberOfPeople);
        }
        var reservation = new Domain.Entities.Reservation(request.FirstName, request.LastName, request.Email,
            request.Date, request.NumberOfPeople, table);
        _reservationRepository.Create(reservation);
        return Unit.Task;
    }
}