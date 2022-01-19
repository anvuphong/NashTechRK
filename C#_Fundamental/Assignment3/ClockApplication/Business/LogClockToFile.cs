using System.IO;
using System.Text;
using ClockApplication.Events;

namespace ClockApplication.Business
{
    public class LogClockToFile
    {
        public void Subcrice(Clock clock)
        {
            clock.SecondChange += new Clock.SecondChangeHandler(WriteToFile);
        }
        public void WriteToFile(object clock, TimeInfoEventArgs args)
        {
            string outputString = "Time: " + args.hour + ":" + args.minute + ":" + args.second;
            using (FileStream stream = File.Open("C://LogFileStream.txt", FileMode.Append))
            {
                byte[] bytes = new UTF8Encoding(true).GetBytes(outputString+"\n");
                stream.Write(bytes, 0, bytes.Length);
            }

            using (StreamWriter writer = new StreamWriter("C://LogStreamWriter.txt", true))
            {
                writer.WriteLine(outputString);
            }
        }
    }
}