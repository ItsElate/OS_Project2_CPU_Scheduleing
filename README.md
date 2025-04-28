# CPU Scheduling Simulator (SRTF and HRRN)

This project is a simple CPU Scheduling Simulator built in C# for an Operating Systems class (CS 3502).  
It simulates two CPU scheduling algorithms: **Shortest Remaining Time First (SRTF)** and **Highest Response Ratio Next (HRRN)**.

The program randomly generates 25 processes, each with an arrival time and burst time, and then runs the selected scheduling algorithm to show the results.

---

## Features
- Shortest Remaining Time First (SRTF) Scheduler (Preemptive)
- Highest Response Ratio Next (HRRN) Scheduler (Non-Preemptive)
- Random Process Generator (25 Processes)
- Calculates:
  - Average Waiting Time (AWT)
  - Average Turnaround Time (ATT)
  - Average Response Time (RT)
  - CPU Utilization (%)
  - Throughput (Processes/Second)
- Console-based menu system
- Clear table output after simulation

---

## How to Run

1. Make sure you have [.NET SDK](https://dotnet.microsoft.com/en-us/download) installed (version 7 or 8).
2. Clone this repository:
   ```bash
   git clone https://github.com/YourUsername/OS_Project2_CPU_Scheduling.git
