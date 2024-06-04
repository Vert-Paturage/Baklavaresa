using Domain.Dates;

namespace Application.Services;

public interface IClockService
{
    public BakDate Now { get; } 
    public BakMonth CurrentMonth { get; }
}