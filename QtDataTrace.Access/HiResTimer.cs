namespace QtDataTrace.Utility
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class HiResTimer
    {
        private long freq;
        private long startTime = 0L;
        private long stopTime = 0L;

        public HiResTimer()
        {
            if (!QueryPerformanceFrequency(out this.freq))
            {
                throw new Win32Exception();
            }
        }

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);
        public void Start()
        {
            QueryPerformanceCounter(out this.startTime);
        }

        public void Stop()
        {
            QueryPerformanceCounter(out this.stopTime);
        }

        public double Duration
        {
            get
            {
                return (((double) (this.stopTime - this.startTime)) / ((double) this.freq));
            }
        }

        public long ElapsedMicroseconds
        {
            get
            {
                long num = this.stopTime - this.startTime;
                if (num < 0x10c6f7a0b5edL)
                {
                    return ((num * 0xf4240L) / this.freq);
                }
                return ((num / this.freq) * 0xf4240L);
            }
        }

        public long ElapsedTicks
        {
            get
            {
                return (this.stopTime - this.startTime);
            }
        }

        public TimeSpan ElapsedTimeSpan
        {
            get
            {
                long ticks = 10L * this.ElapsedMicroseconds;
                if (ticks > 0L)
                {
                    return new TimeSpan(ticks);
                }
                return TimeSpan.MaxValue;
            }
        }

        public long Frequency
        {
            get
            {
                return this.freq;
            }
        }
    }
}

