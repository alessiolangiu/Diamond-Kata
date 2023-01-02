namespace Diamond.Integration.Tests;

using System.Diagnostics;
using System.Linq;

internal static class TestUtil
{
    public static string[] RunDiamondWithoutArguments()
    {
        ProcessStartInfo procStartInfo = new ProcessStartInfo("diamond.exe");
        return Run(procStartInfo);
    }

    public static string[] RunDiamondWithArguments(string arg)
    {
        ProcessStartInfo procStartInfo = new ProcessStartInfo("diamond.exe", arg);
        return Run(procStartInfo);
    }

    private static string[] Run(ProcessStartInfo procStartInfo)
    {
        procStartInfo.RedirectStandardOutput = true;
        procStartInfo.UseShellExecute = false;
        procStartInfo.CreateNoWindow = true;

        using (Process process = new Process())
        {
            process.StartInfo = procStartInfo;
            process.Start();
            process.WaitForExit();
            string result = process.StandardOutput.ReadToEnd();
            var lines = result.Split('\n');
            return lines.Where(line => line != "").Select(line => line.EndsWith('\r') ? line.Substring(0, line.Length -1): line).ToArray();
        }
    }
}