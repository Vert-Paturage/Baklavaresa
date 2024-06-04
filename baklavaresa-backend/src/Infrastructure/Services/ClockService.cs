using Application.Services;

namespace Infrastructure.Services;

internal class ClockService: IClockService
{
    public DateTime Now => DateTime.Now;
    public DateTime CurrentMonth => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
}