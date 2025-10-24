using System;
using System.Globalization;

namespace drupAuto.Utils
{


    public static class NumberUtils
    {

        public static bool IsDecimal(string input)
        {
            decimal _;
            return IsDecimal(input, out _);
        }

        public static bool IsDecimal(string input, out decimal value)
        {
            value = 0m;
            if (input == null)
                return false;

            var trimmed = input.Trim();
            return decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }
    }
}
