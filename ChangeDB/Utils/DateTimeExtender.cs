namespace ChangeDB.Utils;

public static class DateTimeExtender
{
    public static readonly DateTime DateTime1970 = new DateTime(1970, 1, 1, 0, 0, 0);

    /// <summary>
    /// thời gian 1970 + 7
    /// </summary>
    public static DateTime DateTime1970H7 => new DateTime(1970, 1, 1, 7, 0, 0, DateTimeKind.Local);

    private static readonly long Tick1970 = DateTime1970.Ticks;

    /// <summary>
    /// Cộng trừ 7h
    /// </summary>
    public static int SEVEN_HOUR = 7 * 60 * 60;

    /// <summary>
    /// chuyển đổi từ DateTime sang seconds
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static long CurrentSeconds(this DateTime time)
    {
        return Convert.ToInt32((time - DateTime1970H7).TotalSeconds);
    }

    public static DateTime FromTimestamp(this double totalSeconds)
    {
        //return new DateTime(1970, 1, 1, new CultureInfo("vi-VN", false).Calendar).AddSeconds(totalSeconds);
        return DateTime1970.AddSeconds(totalSeconds + SEVEN_HOUR);
    }

    public static double ToTimestamp(this DateTime dateTime)
    {
        return (dateTime - DateTime1970).TotalSeconds - SEVEN_HOUR;
    }

    public static double TimeSpanFistrDay(double totalSeconds, bool? addLocalUtc = false)
    {
        var datetime = new DateTime();
        if (addLocalUtc != true)
        {
            datetime = FromTimestamp(totalSeconds);
        }
        else
        {
            datetime = FromTimestamp_AddLocalUtc(totalSeconds);

        }
        var date = datetime.Date;
        return ToTimestamp(date);
    }

    /// <summary>
    /// So sánh 2 ngày: 0 => bằng nhau, -1 => số trước lớn hơn số sau, 1 => số sau lớn hơn số trước
    /// </summary>
    /// <param name="dateFisrt">Ngày trước</param>
    /// <param name="dateLast">Ngày sau</param>
    /// <returns></returns>
    public static int CompareTwoDate(double? dateFisrt, double? dateLast)
    {
        if (FromTimestamp(dateFisrt ?? 0).Date == FromTimestamp(dateLast ?? 0).Date)
        {
            return 0;
        }
        else if (FromTimestamp(dateFisrt ?? 0).Date > FromTimestamp(dateLast ?? 0).Date)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    public static DateTime DateTimeFistrDay(double totalSeconds, bool? addLocalUtc = false)
    {
        var datetime = new DateTime();
        if (addLocalUtc != true)
        {
            datetime = FromTimestamp(totalSeconds);
        }
        else
        {
            datetime = FromTimestamp_AddLocalUtc(totalSeconds);

        }
        var date = datetime.Date;
        return date;
    }

    public static double TimeSpanEndDay(double totalSeconds)
    {
        DateTime datetime = FromTimestamp_AddLocalUtc(totalSeconds);

        var date = datetime.Date;
        return ToTimestamp(date) + 86399;
    }

    public static DateTime FromTimestamp_AddLocalUtc(this double totalSeconds)
    {
        return DateTime1970H7.AddSeconds(totalSeconds);
    }

    public static DateTime FromTimestampByUtc(double totalSeconds)
    {
        var timeByUtc = CurrentSeconds(DateTime.Now) - CurrentSeconds(DateTime.UtcNow);
        totalSeconds = totalSeconds + timeByUtc;
        return DateTime1970.AddSeconds(totalSeconds);
    }

    #region Tìm kiếm Không có giờ(lấy đầu ngày - cuối ngày)
    public static double TimeSpanFistrDayUtcV2(double totalSeconds)
    {

        var timeByUtc = CurrentSeconds(DateTime.Now) - CurrentSeconds(DateTime.UtcNow);
        var datetime = FromTimestampByUtc(totalSeconds);
        var date = datetime.Date;
        return ToTimestamp(date) - timeByUtc;
    }

    public static double TimeSpanEndDayUtcV2(double totalSeconds)
    {
        var timeByUtc = CurrentSeconds(DateTime.Now) - CurrentSeconds(DateTime.UtcNow);
        var datetime = FromTimestampByUtc(totalSeconds);
        var date = datetime.Date;
        return ToTimestamp(date) + 86399 - timeByUtc;
    }

    #endregion

    #region Tìm kiếm Có giờ(lấy theo giờ của tgian truyền vào)
    public static double TimeSpanFistrDayUtcHasTimeV2(double totalSeconds)
    {
        var timeByUtc = CurrentSeconds(DateTime.Now) - CurrentSeconds(DateTime.UtcNow);
        var datetime = FromTimestampByUtc(totalSeconds);
        var date = datetime.Date;
        return (ToTimestamp(date) + datetime.TimeOfDay.TotalSeconds) - timeByUtc;
    }
    public static double TimeSpanEndDayUtcHasTimeV2(double totalSeconds)
    {
        var timeByUtc = CurrentSeconds(DateTime.Now) - CurrentSeconds(DateTime.UtcNow);
        var datetime = FromTimestampByUtc(totalSeconds);
        var date = datetime.Date;
        return (ToTimestamp(date) + datetime.TimeOfDay.TotalSeconds) - timeByUtc;
    }
    #endregion


    public static double TimeSpanFistrDayFromDateTime(DateTime dateTime)
    {
        var date = dateTime.Date;
        return ToTimestamp(date);
    }

    public static double TimeSpanFistrDayFromDateTime_UTC(DateTime dateTime)
    {
        var timeByUtc = CurrentSeconds(DateTime.Now) - CurrentSeconds(DateTime.UtcNow);
        var date = dateTime.Date;
        return ToTimestamp(date) - timeByUtc;
    }

    public static double TimeSpanEndDayFromDateTime(DateTime dateTime)
    {
        var date = dateTime.Date;
        return ToTimestamp(date) + 86399;
    }
    public static double TimeSpanEndDayFromDateTime_Utc(DateTime dateTime)
    {
        var timeByUtc = CurrentSeconds(DateTime.Now) - CurrentSeconds(DateTime.UtcNow);
        var date = dateTime.Date;
        return ToTimestamp(date) + 86399 - timeByUtc;
    }
    public static double ToEndOfDayTime(this DateTime dateTime)
    {
        var date = dateTime.Date;
        return ToTimestamp(date) + 86399;
    }
    public static double ToEndOfDayTime(this double dateTime)
    {
        var datetime = FromTimestamp(dateTime);
        var date = datetime.Date;
        return ToTimestamp(date) + 86399;
    }
    public static double ToStartOfDayTime(this DateTime dateTime)
    {
        var date = dateTime.Date;
        return ToTimestamp(date);
    }
    public static double ToStartOfDayTime(this double dateTime)
    {
        var datetime = FromTimestamp(dateTime);
        var date = datetime.Date;
        return ToTimestamp(date);
    }
    public static DateTime StartDateOfMonth(DateTime dateTime)
    {
        var startDate = new DateTime(dateTime.Year, dateTime.Month, 1);
        return startDate;
    }
    public static DateTime EndDateOfMonth(DateTime dateTime)
    {
        var endDate = new DateTime(dateTime.Year, dateTime.AddMonths(1).Month, 1).AddDays(-1);
        return endDate;
    }
}
