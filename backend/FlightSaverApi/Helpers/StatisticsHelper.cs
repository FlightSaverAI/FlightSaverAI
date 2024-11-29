using System.Globalization;

namespace FlightSaverApi.Helpers;

public static class StatisticsHelper
{
    
    public static int GetWeekNumber(DateTime date)
    {
        var calendar = CultureInfo.InvariantCulture.Calendar;
        return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
    }
    
    public static DateTime GetStartOfWeek(DateTime date, int weekNumber)
    {
        var jan1 = new DateTime(date.Year, 1, 1);
        var daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
        var firstMonday = jan1.AddDays(daysOffset);
        var startOfWeek = firstMonday.AddDays((weekNumber - 1) * 7);
        return startOfWeek;
    }
    
    public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        double lat1Rad = ToRadians(lat1);
        double lon1Rad = ToRadians(lon1);
        double lat2Rad = ToRadians(lat2);
        double lon2Rad = ToRadians(lon2);

        double dLat = lat2Rad - lat1Rad;
        double dLon = lon2Rad - lon1Rad;

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double distanceKm = Constants.EarthRadiusKm * c;

        return distanceKm;
    }

    private static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
}