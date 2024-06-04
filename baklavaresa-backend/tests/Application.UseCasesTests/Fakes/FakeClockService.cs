using Application.Services;
using Domain.Dates;

namespace Application.UseCasesTests.Fakes;

public class FakeClockService(BakDate dateTime): IClockService
{
    private DateTime _dateTime = dateTime;
    public BakDate Now => _dateTime;

    public BakMonth CurrentMonth => new DateTime(dateTime.Year, dateTime.Month, 1);
    public void SetDateTime(BakDate dateTime)
    {
        _dateTime = dateTime;
    }
}