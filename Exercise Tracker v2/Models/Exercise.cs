namespace Exercise_Tracker_v2.Models;

public class Exercise
{
    public int Id { get; set; }
    
    public DateTime DateStart { get; set; }
    
    public DateTime DateEnd { get; set; }
    
    public int Duration { get; set; }
    
    public string Comments { get; set; }
}