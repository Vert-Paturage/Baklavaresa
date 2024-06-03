using Application.Services;

namespace Infrastructure.Services;

internal class ClockService: IClockService
{
    public DateTime Now => DateTime.Now;
}