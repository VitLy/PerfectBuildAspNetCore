using System;
using System.Collections.Generic;
using System.Linq;

namespace PerfectBuild.Services
{
    public class TrainingDayConverter : ITrainigDayConverter
    {
        private static readonly Dictionary<int, DayOfWeek> day = new Dictionary<int, DayOfWeek> {
            { 0, DayOfWeek.Monday },{ 1, DayOfWeek.Tuesday },{ 2, DayOfWeek.Wednesday },{ 3, DayOfWeek.Thursday },
            { 4, DayOfWeek.Friday },{ 5, DayOfWeek.Saturday },{ 6, DayOfWeek.Sunday }
        };

        public IEnumerable<DayOfWeek> ByteToDays(byte days)
        {
            List<DayOfWeek> result = new List<DayOfWeek>();
            for (byte i = 0; i < 7; i++)
            {
                var bit = days >> i;
                if (bit % 2 != 0)
                {
                    result.Add(day[i]);
                }
            }
            return result;
        }

        public IEnumerable<DayOfWeek> ByteToDays(IEnumerable<byte> byteDays)
        {
            List<DayOfWeek> result = new List<DayOfWeek>();

            foreach (var item in byteDays)
            {
                result.AddRange(ByteToDays(item));
            }
            return result.Distinct().ToList();
        }

        public byte DaysToByte(DayOfWeek day)
        {
            return DaysToByte(new List<DayOfWeek> { day });
        }
        public byte DaysToByte(IEnumerable<DayOfWeek> days)
        {
            if (days == null)
            {
                throw new ArgumentNullException(nameof(days));
            }
            int result = 00000000;
            foreach (DayOfWeek day in days)
            {
                switch (day)
                {
                    case DayOfWeek.Monday:
                        result |= 0b00000001;
                        break;
                    case DayOfWeek.Tuesday:
                        result |= 0b00000010;
                        break;
                    case DayOfWeek.Wednesday:
                        result |= 0b00000100;
                        break;
                    case DayOfWeek.Thursday:
                        result |= 0b00001000;
                        break;
                    case DayOfWeek.Friday:
                        result |= 0b00010000;
                        break;
                    case DayOfWeek.Saturday:
                        result |= 0b00100000;
                        break;
                    case DayOfWeek.Sunday:
                        result |= 0b01000000;
                        break;
                }
            }
            return (byte)result;
        }
    }
}
