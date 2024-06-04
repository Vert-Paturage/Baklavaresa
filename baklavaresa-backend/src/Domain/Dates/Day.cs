namespace Domain.Dates;

// This class is used to represent a day, (year, month, day)
public class BakDay
{
    public int Year { get; }
    public int Month { get; }
    public int Day { get; }

    public BakDay(int year, int month, int day)
    {
        Year = year;
        Month = month;
        Day = day;
    }
    
    public static implicit operator DateTime(BakDay bakDay)
    {
        return new DateTime(bakDay.Year, bakDay.Month, bakDay.Day);
    }
    
    public static implicit operator BakDay(DateTime date)
    {
        return new BakDay(date.Year, date.Month, date.Day);
    }
    
    public static BakDay FromDateTime(DateTime date)
    {
        return new BakDay(date.Year, date.Month, date.Day);
    }

    public static bool operator ==(BakDay a, BakDay b)
    {
        return a.ToDateTime() == b.ToDateTime();
    }

    public static bool operator !=(BakDay day1, BakDay day2)
    {
        return !(day1 == day2);
    }
    
    public static bool operator <=(BakDay a, BakDay b)
    {
        return a.ToDateTime() <= b.ToDateTime();
    }

    public static bool operator >=(BakDay a, BakDay b)
    {
        return a.ToDateTime() >= b.ToDateTime();
    }

    public override bool Equals(object obj)
    {
        return obj is BakDay day && this == day;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Year, Month, Day);
    }
    
    public DateTime ToDateTime() => new(Year, Month, Day);
}