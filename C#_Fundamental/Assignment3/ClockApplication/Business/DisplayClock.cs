using System;
using ClockApplication.Events;

namespace ClockApplication.Business
{
    public class DisplayClock
    {
        public void Subcrice(Clock clock)
        {
            clock.SecondChange += new Clock.SecondChangeHandler(TimeHasChanged);
        }

        private void TimeHasChanged(object clock, TimeInfoEventArgs args)
        {
            Console.WriteLine("{0}:{1}:{2}", args.hour, args.minute, args.second);
        }
    }
}