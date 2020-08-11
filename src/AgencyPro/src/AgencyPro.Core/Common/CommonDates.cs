// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Globalization;

namespace AgencyPro.Core.Common
{
    public static class CommonDates
    {
        public static DayOfWeek StartingDayOfWeek = DayOfWeek.Monday;

        public static DateTime StartOfLastWeek => StartOfThisWeek.AddDays(-7);

        public static DateTime StartOf2WeeksAgo => StartOfThisWeek.AddDays(-14);

        public static DateTime EndOfThisWeek => GetStartOfWeek(DateTime.UtcNow).AddDays(7);

        public static DateTime StartOfThisWeek => GetStartOfWeek(DateTime.UtcNow);

        public static DateTime RoundUp(this DateTime dt, TimeSpan d)
        {
            var modTicks = dt.Ticks % d.Ticks;
            var delta = modTicks != 0 ? d.Ticks - modTicks : 0;
            return new DateTime(dt.Ticks + delta, dt.Kind);
        }

        public static DateTime RoundDown(this DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            return new DateTime(dt.Ticks - delta, dt.Kind);
        }

        public static DateTime RoundToNearest(this DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            var roundUp = delta > d.Ticks / 2;
            var offset = roundUp ? d.Ticks : 0;

            return new DateTime(dt.Ticks + offset - delta, dt.Kind);
        }

        public static DateTime RoundToNearest15Minutes(this DateTime dateTime)
        {
            var segment = dateTime.GetSegmentIdentifier();
            return dateTime.Date.AddMinutes(segment * 15);
        }

        public static int GetSegmentIdentifier(this DateTime dateTime)
        {
            var span = dateTime - dateTime.Date;
            return Convert.ToInt32(span.TotalMinutes / 15);
        }

        public static DateTime GetDateFromSegment(int year, int month, int day, int segment)
        {
            return new DateTime(year, month, day).AddMinutes(segment * 15);
        }

        public static DateTime GetStartOfWeek(DateTime dateTime)
        {
            var startingDate = dateTime.Date;

            while (startingDate.DayOfWeek != StartingDayOfWeek)
                startingDate = startingDate.AddDays(-1);

            return startingDate;
        }

        public static DateTime GetStartOfWeekForWeekNumber(int week)
        {
            if (week > 52) throw new Exception();

            var startDate = FirstDayOfThisYear();
            var returnDate = startDate;
            for (var i = 0; i < week; i++) returnDate = startDate.AddDays(7 * i);
            return returnDate;
        }

        public static int SecondsSince1970()
        {
            var span = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return span.Seconds;
        }

        public static DateTime FirstDayOfThisYear()
        {
            return new DateTime(DateTime.UtcNow.Year, 1, 1);
        }

        public static DateTime LastDayOfLastYear()
        {
            return FirstDayOfThisYear().AddDays(-1);
        }

        public static DateTime FirstDayOfLastYear()
        {
            return FirstDayOfThisYear().AddYears(-1);
        }

        public static DateTime LastDayOfLastMonth()
        {
            return FirstDayOfMonth().AddDays(-1);
        }

        public static DateTime FirstDayOfLastMonth()
        {
            var aMonthAgo = DateTime.UtcNow.AddMonths(-1);
            return new DateTime(aMonthAgo.Year, aMonthAgo.Month, 1);
        }

        public static DateTime FirstDayOfMonth()
        {
            return new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        }

        public static DateTime Yesterday()
        {
            return DateTime.Today.AddDays(-1);
        }

        private static int GetQuarter(DateTime date)
        {
            return (date.Month + 2) / 3;
        }

        public static DateTime LastDayOfLastQuarter()
        {
            var threeMonthsAgoFromToday = DateTime.UtcNow.AddMonths(-3);
            return LastDayOfQuarterFromDate(threeMonthsAgoFromToday);
        }

        public static DateTime LastDayOfQuarterFromDate(DateTime? date)
        {
            if (date == null)
                date = DateTime.UtcNow;

            var quarter = GetQuarter(date.Value);

            switch (quarter)
            {
                case 1:
                    return new DateTime(date.Value.Year, 3, 31);

                case 2:
                    return new DateTime(date.Value.Year, 6, 30);

                case 3:
                    return new DateTime(date.Value.Year, 9, 30);

                default:
                    return new DateTime(date.Value.Year, 12, 31);
            }
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday) time = time.AddDays(3);

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        }

        public static DateTime LastDateOfWeekISO8601(int year, int weekOfYear)
        {
            return FirstDateOfWeekISO8601(year, weekOfYear).AddDays(7);
        }

        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            var firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            var firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1) weekNum -= 1;

            // Using the first Thursday as starting week ensures we're staring in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }
    }
}