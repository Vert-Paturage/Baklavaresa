using System.Text.RegularExpressions;
using Application.Services;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Reservation.Commands.CreateReservation;

public record CreateReservationCommand(
    string FirstName,
    string LastName,
    string Email,
    DateTime Date,
    int NumberOfPeople
    ) : IRequest<int>;

public partial class CreateReservationCommandHandler(IReservationRepository reservationRepository, ITableRepository tableRepository,IClockService clockService) : IRequestHandler<CreateReservationCommand, int>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly ITableRepository _tableRepository = tableRepository;
    private readonly IClockService _clockService = clockService;
    public Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        if (request.FirstName == null || request.LastName == null || request.FirstName.Length == 0 || request.LastName.Length == 0)
        {
            throw new Domain.Exceptions.Reservation.InvalidNameException();
        }
        
        if (request.Date < _clockService.Now)
        {
            throw new Domain.Exceptions.Reservation.InvalidReservationDateException(request.Date);
        }
        if (request.NumberOfPeople <= 0)
        {
            throw new Domain.Exceptions.Reservation.InvalidNumberOfPeopleException(request.NumberOfPeople);
        }
        var emailRegex = EmailRegex();
        if (request.Email == null || !emailRegex.IsMatch(request.Email))
        {
            throw new Domain.Exceptions.Reservation.InvalidEmailException(request.Email);
        }
       
        var table = _tableRepository.GetTablesByCapacity(request.NumberOfPeople).Result.FirstOrDefault();
        if (table == null)
        {
            throw new Domain.Exceptions.Table.NoTablesAvailableException(request.Date, request.NumberOfPeople);
        }
        var reservation = new Domain.Entities.Reservation(request.FirstName, request.LastName, request.Email,
            request.Date, request.NumberOfPeople, table);
        var id = _reservationRepository.Create(reservation);
        return id;
    }

    [GeneratedRegex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$")]
    private static partial Regex EmailRegex();
}