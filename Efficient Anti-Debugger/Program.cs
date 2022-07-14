using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace EfficientAntiDebugger {
    class Program {
        private static string[] _processNames; // String array of malicious process names.

        static void Main(string[] args) {
            Console.ForegroundColor = ConsoleColor.Red;

            _processNames = new string[] {
                "notepad"
                // Add malicious process names, notepad used for testing.
            };

            new Thread(Running) {
                IsBackground = false // Switch to true if using WinForms unless of course you wish the cmd prompt to be visible.
            }.Start(); // Starting new thread.
        }

        private static void Running() {
            var whitelistedIds = new List<int>(); // After checking a process we add its Id to this int list so that it isn't re-checked.
            var _currentProcess = Process.GetCurrentProcess(); // Getting our process.
            whitelistedIds.Add(_currentProcess.Id); // Adding our process Id to the whitelist.

            int _lastLength = 1; // Int for storing the total number of processes.

            while (true) { // Creating a permanent loop.
                var _processes = Process.GetProcesses(); // Getting a new array list of processes each loop.

                if (_processes.Length != _lastLength) { // Checking that the total number of processes wasn't the same as the last loop.
                    foreach (Process process in _processes) { // Looping through each process in the array list.
                        if (!whitelistedIds.Contains(process.Id)) { // Checking that the process Id isn't whitelisted.
                            foreach (string processName in _processNames) { // Looping through each process name in our string array.
                                if (process.ProcessName.ToLower().Contains(processName) || // Checking the process name.
                                    process.MainWindowTitle.ToLower().Contains(processName)) { // Checking the main window title.
                                    // Action after finding malicious process.
                                    Console.WriteLine($"Malicious process found: {processName}");
                                    process.Kill();
                                }
                                else whitelistedIds.Add(process.Id); // If the process has no malicious name/title we'll add it to the whitelist.
                            }
                        }
                    }
                    _lastLength = _processes.Length; // Updating the last total number of processes.
                }
                else Thread.Sleep(_lastLength); // Sleeping once we know that all currently running processes have been checked so we don't reach high usage.
                // Multiply amount to get lower usage.
            }
        }
    }
}
