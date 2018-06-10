using System;
namespace wpg.@internal.xml.serializer
{
    public class DateSerializer
    {

        public static DateTime readDate(XmlBuilder builder)
        {
            int dayOfMonth = builder.aToInt("dayOfMonth");
            int month = builder.aToInt("month");
            int year = builder.aToInt("year");

            DateTime result = new DateTime(year, month, dayOfMonth);
            return result;
        }

        public static DateTime readDateTime(XmlBuilder builder)
        {
            builder.e("date");

            int year = builder.aToInt("year");
            int month = builder.aToInt("month");
            int dayOfMonth = builder.aToInt("dayOfMonth");

            int? hour = builder.aToInt("hour");
            int? minute = builder.aToInt("minute");
            int? second = builder.aToInt("second");

            builder.up();

            DateTime result;
            if (hour != null && minute != null && second != null)
            {
                result = new DateTime(year, month, dayOfMonth, (int) hour, (int) minute, (int) second);
            }
            else
            {
                result = new DateTime(year, month, dayOfMonth);
            }

            return result;
        }

        public static void writeDateTime(XmlBuilder builder, DateTime dateTime)
        {
            builder.e("date");
            builder.a("year", dateTime.Year.ToString());
            builder.a("month", dateTime.Month.ToString());
            builder.a("dayOfMonth", dateTime.Day.ToString());
            builder.a("hour", dateTime.Hour.ToString());
            builder.a("minute", dateTime.Minute.ToString());
            builder.a("second", dateTime.Second.ToString());
            builder.up();
        }

    }
}
