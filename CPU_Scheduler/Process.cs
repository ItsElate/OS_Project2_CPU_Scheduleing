using System;

public class Process{
    public string ID { get; set; }
    public int ArrivalTime {get; set; }
    public int BurstTime { get; set; }
    public int RemainingTime { get; set; }
    public int CompletionTime { get; set; }
    public int WaitingTime { get; set; }
    public int TurnaroundTime { get; set; }
    public int StartTime { get; set; } = -1;
    public bool IsCompleted {get; set; } = false;
    public int Priority { get; set; }

    public Process(string id, int arrivalTime, int burstTime, int priority = 0){
        ID = id;
        ArrivalTime = arrivalTime;
        BurstTime = burstTime;
        RemainingTime = burstTime;
        Priority = priority;
    }

}