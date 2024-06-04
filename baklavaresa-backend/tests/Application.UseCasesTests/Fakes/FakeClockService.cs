using Application.Services;

namespace Application.UseCasesTests.Fakes;

public class FakeClockService(DateTime dateTime): IClockService
{
    private DateTime _dateTime = dateTime;
    public DateTime Now => _dateTime;

    public DateTime CurrentMonth => new DateTime(dateTime.Year, dateTime.Month, 1);
    public void SetDateTime(DateTime dateTime)
    {
        _dateTime = dateTime;
    }
}