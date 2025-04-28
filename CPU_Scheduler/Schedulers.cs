using System;
using System.Collections.Generic;
using System.Linq;

public static class Schedulers
{
    public static void RunSRTF(List<Process> processes)
    {
        int currentTime = 0;
        int completed = 0;
        int n = processes.Count;
        List<Process> readyQueue = new List<Process>();
        Process currentProcess = null;

        while (completed < n)
        {
            // Add newly arrived processes to ready queue
            foreach (var p in processes.Where(p => p.ArrivalTime == currentTime))
            {
                readyQueue.Add(p);
            }

            // Select process with shortest remaining time
            if (readyQueue.Any())
            {
                currentProcess = readyQueue
                    .Where(p => !p.IsCompleted)
                    .OrderBy(p => p.RemainingTime)
                    .ThenBy(p => p.ArrivalTime)
                    .First();

                // First time the process starts
                if (currentProcess.StartTime == -1)
                {
                    currentProcess.StartTime = currentTime;
                }

                // Execute process for 1 unit of time
                currentProcess.RemainingTime--;

                // If process is completed
                if (currentProcess.RemainingTime == 0)
                {
                    currentProcess.CompletionTime = currentTime + 1;
                    currentProcess.TurnaroundTime = currentProcess.CompletionTime - currentProcess.ArrivalTime;
                    currentProcess.WaitingTime = currentProcess.TurnaroundTime - currentProcess.BurstTime;
                    currentProcess.IsCompleted = true;
                    completed++;
                }
            }

            currentTime++;
        }

        // After simulation, print results
        PrintResults(processes, "SRTF Scheduling");
    }
    
    private static void PrintResults(List<Process> processes, string title)
    {
        Console.WriteLine($"\n=== {title} ===");
        Console.WriteLine($"{"PID",5} {"AT",5} {"BT",5} {"ST",5} {"CT",5} {"TAT",5} {"WT",5}");

        double totalTAT = 0;
        double totalWT = 0;
        double totalResponseTime = 0;
        double totalBurstTime = processes.Sum(p => p.BurstTime);
        int totalTime = processes.Max(p => p.CompletionTime);

        foreach (var p in processes)
        {
            Console.WriteLine($"{p.ID,5} {p.ArrivalTime,5} {p.BurstTime,5} {p.StartTime,5} {p.CompletionTime,5} {p.TurnaroundTime,5} {p.WaitingTime,5}");
            totalTAT += p.TurnaroundTime;
            totalWT += p.WaitingTime;
            totalResponseTime += (p.StartTime - p.ArrivalTime);
        }

        Console.WriteLine($"\nAverage Turnaround Time: {totalTAT / processes.Count:F2}");
        Console.WriteLine($"Average Waiting Time: {totalWT / processes.Count:F2}");
        Console.WriteLine($"Average Response Time: {totalResponseTime / processes.Count:F2}");
        Console.WriteLine($"CPU Utilization: {((totalBurstTime) / totalTime) * 100:F2}%");
        Console.WriteLine($"Throughput: {(double)processes.Count / totalTime:F2} processes/unit time");
    }

    public static void RunHRRN(List<Process> processes)
    {
        int currentTime = 0;
        int completed = 0;
        int n = processes.Count;
        List<Process> readyQueue = new List<Process>();
        Process currentProcess = null;

        while (completed < n)
        {
            // Add newly arrived processes to ready queue
            foreach (var p in processes.Where(p => p.ArrivalTime <= currentTime && !readyQueue.Contains(p) && !p.IsCompleted))
            {
                readyQueue.Add(p);
            }

            // If no process is ready, CPU is idle
            if (!readyQueue.Any())
            {
                currentTime++;
                continue;
            }

            // Calculate response ratio for all ready processes
            foreach (var p in readyQueue)
            {
                int waitingTime = currentTime - p.ArrivalTime;
                p.Priority = ((waitingTime + p.BurstTime) * 1000) / p.BurstTime; // Avoid float by scaling
            }

            // Pick the process with highest response ratio
            currentProcess = readyQueue
                .Where(p => !p.IsCompleted)
                .OrderByDescending(p => p.Priority)
                .First();

            // First time the process starts
            if (currentProcess.StartTime == -1)
            {
                currentProcess.StartTime = currentTime;
            }

            // Execute process completely (non-preemptive)
            currentTime += currentProcess.BurstTime;

            currentProcess.CompletionTime = currentTime;
            currentProcess.TurnaroundTime = currentProcess.CompletionTime - currentProcess.ArrivalTime;
            currentProcess.WaitingTime = currentProcess.TurnaroundTime - currentProcess.BurstTime;
            currentProcess.IsCompleted = true;

            readyQueue.Remove(currentProcess);
            completed++;
        }

        // After simulation, print results
        PrintResults(processes, "HRRN Scheduling");
    }
}