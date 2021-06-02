using System;
using System.Diagnostics;
using System.Threading.Tasks;


namespace R5T.T0030
{
    public static class CommandLine
    {
        /// <summary>
        /// The base run implementation. Synchronous. Returns exit code.
        /// </summary>
        public static int RunSynchronous(string command, string arguments, DataReceivedEventHandler receiveOutputData, DataReceivedEventHandler receiveErrorData)
        {
            ProcessStartInfo startInfo = new(command, arguments)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true
            };

            Process process = new()
            {
                StartInfo = startInfo
            };

            process.OutputDataReceived += receiveOutputData;
            process.ErrorDataReceived += receiveErrorData;

            process.Start();
            process.BeginOutputReadLine(); // Must occur after start?
            process.BeginErrorReadLine(); // Must occur after start?

            process.WaitForExit();

            process.OutputDataReceived -= receiveOutputData;
            process.ErrorDataReceived -= receiveErrorData;

            int exitCode = process.ExitCode; // Must get value before closing the process?

            process.Close();

            return exitCode;
        }

        public static async Task<int> Run(string command, string arguments, DataReceivedEventHandler receiveOutputData, DataReceivedEventHandler receiveErrorData)
        {
            ProcessStartInfo startInfo = new(command, arguments)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true
            };

            Process process = new()
            {
                StartInfo = startInfo
            };

            process.OutputDataReceived += receiveOutputData;
            process.ErrorDataReceived += receiveErrorData;

            process.Start();
            process.BeginOutputReadLine(); // Must occur after start?
            process.BeginErrorReadLine(); // Must occur after start?

            await process.WaitForExitAsync();

            process.OutputDataReceived -= receiveOutputData;
            process.ErrorDataReceived -= receiveErrorData;

            int exitCode = process.ExitCode; // Must get value before closing the process?

            process.Close();

            return exitCode;
        }
    }
}
