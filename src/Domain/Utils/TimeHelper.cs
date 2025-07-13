namespace NotificationService.Domain.Utils
{
    public static class TimeHelper
    {
        public static string ToThu(string date)
        {
            string shortDayOfWeek = string.Empty;
            shortDayOfWeek = date switch
            {
                "Sunday" => "CN",
                "Monday" => "T2",
                "Tuesday" => "T3",
                "Wednesday" => "T4",
                "Thursday" => "T5",
                "Friday" => "T6",
                "Saturday" => "T7",
                _ => "",
            };
            return shortDayOfWeek;
        }

        public static DateTimeOffset ConvertToUtcPlus7(DateTimeOffset dateTimeOffset)
        {
            TimeSpan utcPlus7Offset = new(7, 0, 0);
            return dateTimeOffset.ToOffset(utcPlus7Offset);
        }

    }
}
