namespace Application.Services;

public interface IClockService
{
    public DateTime Now { get; } 
    public DateTime CurrentMonth { get; }
}