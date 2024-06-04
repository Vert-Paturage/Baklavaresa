namespace Domain.Dates;

// This class is used to represent a day, (year, month, day, hour, minute)
public class BakDate
{
    public int Year { get; }
    public int Month { get; }
    public int Day { get; }
    public int Hour { get; }
    public int Minute { get; }

    public BakDate(int year, int month, int day, int hour, int minute)
    {
        Year = year;
        Month = month;
        Day = day;
        Hour = hour;
        Minute = minute;
    }
    
    
    public static implicit operator DateTime(BakDate bakDate) => new(bakDate.Year, bakDate.Month, bakDate.Day, bakDate.Hour, bakDate.Minute, 0);

    public static implicit operator BakDate(DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute);

    public static bool operator <(BakDate a, BakDate b) => a.ToDateTime() < b.ToDateTime();

    public static bool operator >(BakDate a, BakDate b) => a.ToDateTime() > b.ToDateTime();

    public static bool operator <(DateTime a, BakDate b) => a < b.ToDateTime();

    public static bool operator >(DateTime a, BakDate b) => a > b.ToDateTime();
    
    public static bool operator >=(BakDate a, BakDate b) => a.ToDateTime() >= b.ToDateTime();
    public static bool operator <=(BakDate a, BakDate b) => a.ToDateTime() <= b.ToDateTime();

    public static BakDate FromDateTime(DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute);


    public static bool operator ==(BakDate date1, BakDate date2)
    {
        return date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day && date1.Hour == date2.Hour && date1.Minute == date2.Minute;
    }

    public static bool operator !=(BakDate date1, BakDate date2) => !(date1 == date2);

    public override bool Equals(object? obj) => obj is BakDate date && this == date;

    public override int GetHashCode() => HashCode.Combine(Year, Month, Day, Hour, Minute);
    
    public DateTime ToDateTime() => new(Year, Month, Day, Hour, Minute, 0);

    public BakDate AddDays(int i) => ToDateTime().AddDays(i);

    public BakDate AddHours(int p0) => ToDateTime().AddHours(p0);

    public BakDay GetBakDay() => new BakDay(Year, Month, Day);

    public override string ToString()
    {
        return this.ToDateTime().ToString();
    }
}