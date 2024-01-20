using System.Diagnostics;

var timespans = new TimeSpan[] { TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5) };
var warningTime = TimeSpan.FromMinutes(30);
Console.WriteLine("7DTD Update notifier by Topias Mariapori, 2024");

foreach (var timespan in timespans)
{
    Process process = new Process();
    process.StartInfo.FileName = "telnet";
    process.StartInfo.Arguments = "127.0.0.1 8081";
    process.StartInfo.RedirectStandardOutput = false;
    process.StartInfo.RedirectStandardInput = true;
    process.Start();
    Console.WriteLine(warningTime.TotalMinutes);
    process.StandardInput.WriteLine($"say \"Server maintenance starts after {warningTime.TotalMinutes} minutes \"");
    process.StandardInput.WriteLine("exit");
    process.WaitForExit();
    warningTime = warningTime.Subtract(timespan);
    Thread.Sleep(timespan);
}
Console.WriteLine("Now");
var process2 = new Process();
process2.StartInfo.FileName = "sdtdserver";
process2.StartInfo.WorkingDirectory = "/home/sdtdserver";
process2.StartInfo.Arguments = "restart";
process2.Start();
process2.WaitForExit();