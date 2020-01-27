using System;
using System.Collections.Generic;

namespace PerfectBuild.Services
{
    public interface ITrainigDayConverter
    {
        IEnumerable<DayOfWeek> ByteToDays(byte days);
        IEnumerable<DayOfWeek> ByteToDays(IEnumerable<byte> byteDays);
        byte DaysToByte(DayOfWeek day);
        byte DaysToByte(IEnumerable<DayOfWeek> days);
    }
}