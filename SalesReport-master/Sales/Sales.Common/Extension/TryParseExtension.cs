using System;

namespace Sales.Common.Extension
{
    public static class TryParseExtension
    {
        public static DateTime GetDateTimeParsed(this string value)
        {
            var newDate = value.Split('/');
            DateTime.TryParse(string.Format("{0}/{1}/{2}", newDate[1], newDate[0], newDate[2]), out DateTime dateResult);
            return dateResult;
        }

        public static Decimal GetDecimalParsed(this string value)
        {
            decimal.TryParse(value, out decimal decimalResult);
            return decimalResult;
        }
    }
}