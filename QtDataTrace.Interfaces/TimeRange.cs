using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.Serializable, StructLayout(LayoutKind.Sequential)]
    public class TimeRange
    {
        public long Begin;
        public long End;

        public TimeRange()
        {
            Begin = 0;
            End = 0;
        }

        public TimeRange(long begin, long end)
        {
            this.Begin = begin;
            this.End = end;
        }

        public TimeRange(long begin, TimeSpan length)
        {
            this.Begin = begin;
            this.End = begin + length.Ticks;
        }

        public TimeSpan Range
        {
            get
            {
                return TimeSpan.FromTicks(this.End - this.Begin);
            }
        }

        public bool IsEmpty
        {
            get
            {
                return (this.Begin >= this.End);
            }
        }

        public void Intersect(TimeRange t)
        {
            this.Begin = Math.Max(this.Begin, t.Begin);
            this.End = Math.Min(this.End, t.End);
        }


        public override string ToString()
        {
            DateTime time = new DateTime(this.Begin);
            DateTime time2 = new DateTime(this.End);
            return string.Format("{0} -> {1}", time.ToString("HH:mm:ss.ffffff"), time2.ToString("HH:mm:ss.ffffff"));
        }

        public override int GetHashCode()
        {
            return (int)((this.Begin / 0xf4240L) + (this.Begin - this.End));
        }

        public override bool Equals(object obj)
        {
            TimeRange range = (TimeRange)obj;
            return (this == range);
        }

        public static bool operator ==(TimeRange t1, TimeRange t2)
        {
            return ((t1.Begin == t2.Begin) && (t1.End == t2.End));
        }

        public static bool operator !=(TimeRange t1, TimeRange t2)
        {
            if (t1.Begin == t2.Begin)
            {
                return (t1.End != t2.End);
            }
            return true;
        }
    }
}
