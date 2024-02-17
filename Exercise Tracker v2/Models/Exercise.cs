namespace Exercise_Tracker_v2.Models;

public class Exercise
{
    public int Id { get; set; }
    
    public string DateStart { get; set; }
    
    public string DateEnd { get; set; }
    
    public string Duration { get; set; }
    
    public string Comments { get; set; }
}