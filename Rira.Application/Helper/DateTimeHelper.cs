using System;
using System.Globalization;

namespace Rira.Application.Helper;

public static class DateTimeHelper
{
    public static string ConvertToPersian(this DateTime date)
    {
        if (date == DateTime.MinValue)
            return string.Empty;

        var persianCalendar = new PersianCalendar();

        int year = persianCalendar.GetYear(date);
        int month = persianCalendar.GetMonth(date);
        int day = persianCalendar.GetDayOfMonth(date);

        return $"{year:D4}/{month:D2}/{day:D2}";
    }
}
