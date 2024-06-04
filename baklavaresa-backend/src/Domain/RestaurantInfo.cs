namespace Domain;
using System;

public record Hours(TimeSpan openingHour, TimeSpan closingHour);

public static class RestaurantInfo
{
    private static readonly TimeSpan OpeningHourLunch = new TimeSpan(12, 0, 0);
    private static readonly TimeSpan ClosingHourLunch = new TimeSpan(15, 0, 0);
    private static readonly TimeSpan OpeningHourDinner = new TimeSpan(19, 0, 0);
    private static readonly TimeSpan ClosingHourDinner = new TimeSpan(22, 0, 0);

    public static List<Hours> LunchHours { get; } = new List<Hours>()
    {
        new Hours(OpeningHourLunch, ClosingHourLunch),
        new Hours(OpeningHourDinner, ClosingHourDinner)
    };
    public static readonly DayOfWeek[] OpenDays = { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
    public static readonly int SlotsInterval = 30;
}