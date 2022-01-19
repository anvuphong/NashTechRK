using ClockApplication.Business;
using ClockApplication.Events;

namespace ClockApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Clock clock = new Clock();
            DisplayClock display = new DisplayClock();
            display.Subcrice(clock);
            LogClockToFile log = new LogClockToFile();
            log.Subcrice(clock);
            clock.Run();
        }
    }
}
