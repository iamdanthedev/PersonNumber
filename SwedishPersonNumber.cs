using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Bonliva.PersonNumber
{
    public class SwedishPersonNumber
    {
        public const string SsnFormat1 =
            @"^([1-2][0-9][0-9][0-9])([0-9][0-9])([0-9][0-9])[\-|\+]?([0-9][0-9][0-9][0-9])$";

        public const string SsnFormat2 =
            @"^([0-9][0-9])([0-9][0-9])([0-9][0-9])([\-|\+])([0-9][0-9][0-9][0-9])$";

        public const string SsnFormat3 =
            @"^([0-9][0-9])([0-9][0-9])([0-9][0-9])([0-9][0-9][0-9][0-9])$";

        public static SwedishPersonNumber? Parse(string sourceSsn)
        {
            if (Regex.IsMatch(sourceSsn, SsnFormat1))
            {
                var match = Regex.Match(sourceSsn, SsnFormat1);
                if (int.TryParse(match.Groups[1]
                            .Value, NumberStyles.Any, CultureInfo.CurrentCulture,
                        out var year)
                    && int.TryParse(match.Groups[2]
                            .Value, NumberStyles.Any, CultureInfo.CurrentCulture,
                        out var month)
                    && int.TryParse(match.Groups[3]
                        .Value, NumberStyles.Any, CultureInfo.CurrentCulture, out var day)
                    && int.TryParse(match.Groups[4]
                            .Value, NumberStyles.Any, CultureInfo.CurrentCulture,
                        out var number))
                {
                    return new SwedishPersonNumber(year, month, day, number);
                }
            }

            if (Regex.IsMatch(sourceSsn, SsnFormat2))
            {
                var match = Regex.Match(sourceSsn, SsnFormat2);
                if (int.TryParse(match.Groups[1]
                            .Value, NumberStyles.Any, CultureInfo.CurrentCulture,
                        out var year)
                    && int.TryParse(match.Groups[2]
                            .Value, NumberStyles.Any, CultureInfo.CurrentCulture,
                        out var month)
                    && int.TryParse(match.Groups[3]
                        .Value, NumberStyles.Any, CultureInfo.CurrentCulture, out var day)
                    && int.TryParse(match.Groups[5]
                            .Value, NumberStyles.Any, CultureInfo.CurrentCulture,
                        out var number))
                {
                    var century = DateTime.Now.Year / 100 * 100;
                    var separator = match.Groups[4]
                        .Value;
                    if (separator == "-")
                        century -= 100;
                    year += century;

                    return new SwedishPersonNumber(year, month, day, number);
                }
            }

            if (Regex.IsMatch(sourceSsn, SsnFormat3))
            {
                var match = Regex.Match(sourceSsn, SsnFormat3);
                if (int.TryParse(match.Groups[1]
                            .Value, NumberStyles.Any, CultureInfo.CurrentCulture,
                        out var year)
                    && int.TryParse(match.Groups[2]
                            .Value, NumberStyles.Any, CultureInfo.CurrentCulture,
                        out var month)
                    && int.TryParse(match.Groups[3]
                        .Value, NumberStyles.Any, CultureInfo.CurrentCulture, out var day)
                    && int.TryParse(match.Groups[4]
                            .Value, NumberStyles.Any, CultureInfo.CurrentCulture,
                        out var number))
                {
                    var century = DateTime.Now.Year / 100 * 100;
                    year += century;
                    var refDate = new DateTime(year, month, day);
                    if (DateTime.Now.Subtract(refDate)
                        .TotalDays < 5500)
                        year -= 100;
                    return new SwedishPersonNumber(year, month, day, number);
                }
            }

            return null;
        }

        // public static bool Compare(string ssn1, string ssn2)
        // {
        //     if (!GetNormalizedSocialSecurityNumber(ssn1, out var s1))
        //         return false;
        //
        //     if (!GetNormalizedSocialSecurityNumber(ssn2, out var s2))
        //         return false;
        //
        //     return string.CompareOrdinal(s1!.ToString(), s2!.ToString()) == 0;
        // }

        private SwedishPersonNumber(int year, int month, int day, int number)
        {
            Year = year;
            Month = month;
            Day = day;
            Number = number;
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Number { get; set; }


        public override string ToString() =>
            $"{Year:0000}{Month:00}{Day:00}{Number:0000}";

        public string ToDisplay() =>
            $"{Year:0000}{Month:00}{Day:00}-{Number:0000}";

        public string ToCrmString() =>
            $"{Year:0000}{Month:00}{Day:00}-{Number:0000}";
    }
}