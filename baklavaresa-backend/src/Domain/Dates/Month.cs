namespace Domain.Dates;

// This class is used to represent a month, (year, month)
public class BakMonth
{
    public int Year { get; }
    public int MonthNumber { get; }

    public BakMonth(int year, int month)
    {
        Year = year;
        MonthNumber = month;
    }
    
    public static implicit operator DateTime(BakMonth bakMonth)
    {
        return new DateTime(bakMonth.Year, bakMonth.MonthNumber, 1);
    }
    
    public static implicit operator BakMonth(DateTime date)
    {
        return new BakMonth(date.Year, date.Month);
    }
    
    public static BakMonth FromDateTime(DateTime date)
    {
        return new BakMonth(date.Year, date.Month);
    }

    public static bool operator ==(BakMonth month1, BakMonth month2)
    {
        return month1.Year == month2.Year && month1.MonthNumber == month2.MonthNumber;
    }

    public static bool operator !=(BakMonth month1, BakMonth month2)
    {
        return !(month1 == month2);
    }

    public override bool Equals(object obj)
    {
        return obj is BakMonth month && this == month;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Year, MonthNumber);
    }
}