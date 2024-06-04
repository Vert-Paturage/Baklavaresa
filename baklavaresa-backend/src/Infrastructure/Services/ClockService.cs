using Application.Services;
using Domain.Dates;

namespace Infrastructure.Services;

internal class ClockService: IClockService
{
    public BakDate Now => DateTime.Now;
    public BakMonth CurrentMonth => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
}