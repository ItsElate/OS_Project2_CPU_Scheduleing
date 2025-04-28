using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        string choice;
        //Create sample process list for project (4 total)
        /*
        var processes = new List<Process>
        {
            new Process("P1", 0, 8),
            new Process("P2", 1, 4),
            new Process("P3", 2, 9),
            new Process("P4", 3, 5)
        };
        
        do
        {
            Console.WriteLine("\n=== CPU Scheduler Menu ===");
            Console.WriteLine("1. SRTF");
            Console.WriteLine("2. HRRN");
            Console.WriteLine("3. Exit");
            Console.Write("Choice: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Schedulers.RunSRTF(CloneProcesses(processes));
                    break;
                case "2":
                    Schedulers.RunHRRN(CloneProcesses(processes));
                    break;
                case "3":
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }

            Console.WriteLine("\nPress ENTER to return to the main menu...");
            Console.ReadLine();
            Console.Clear();
        } while (true);
    } */

        // Larger process list of 25
        
        do
        {
            Console.WriteLine("\n=== CPU Scheduler ===");
            Console.WriteLine("1. Shortest Remaining Time First (SRTF)");
            Console.WriteLine("2. Highest Response Ratio Next (HRRN)");
            Console.WriteLine("3. Exit");
            Console.Write("Choice: ");
            choice = Console.ReadLine();

            List<Process> processes;

            switch (choice)
            {
                case "1":
                    processes = GenRandProcess(25); // Generate 25 random processes
                    Schedulers.RunSRTF(CloneProcesses(processes));
                    break;
                case "2":
                    processes = GenRandProcess(25);
                    Schedulers.RunHRRN(CloneProcesses(processes));
                    break;
                case "3":
                    Console.WriteLine("Exiting program...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress ENTER to return to the main menu...");
            Console.ReadLine();
            Console.Clear();

        } while (true);
    }

    static List<Process> CloneProcesses(List<Process> original)
    {
        var cloned = new List<Process>();
        foreach (var p in original)
        {
            cloned.Add(new Process(p.ID, p.ArrivalTime, p.BurstTime, p.Priority));
        }
        return cloned;
    }

    static List<Process> GenRandProcess(int count)
    {
        var random = new Random();
        var processes = new List<Process>();

        for (int i = 1; i <= count; i++)
        {
            string id = "P" + i;
            int arrivalTime = random.Next(0, 10);
            int burstTime = random.Next(1, 10);
            processes.Add(new Process(id, arrivalTime, burstTime));
        }
        return processes.OrderBy(p => p.ArrivalTime).ToList(); // Sort by "ArrivalTime" for clarity
    }
}
