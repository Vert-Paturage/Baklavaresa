using Domain.Entities;
using Domain.Repositories;

namespace Application.Reservation.Commands.DeleteReservation;

public record DeleteReservationCommand(int Id) : IRequest;

internal class DeleteReservationCommandHandler(IReservationRepository reservationRepository) : IRequestHandler<DeleteReservationCommand>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    public async Task Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetById(request.Id);
        await _reservationRepository.Delete(reservation);
    }
}
