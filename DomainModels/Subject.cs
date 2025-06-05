namespace SchoolSubjectsSystem.DomainModels;

public class Subject
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int WeeklyClasses { get; set; }

    public void DisplayDetails()
    {
        Console.WriteLine($"\n--- {Name} ---");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Weekly Classes: {WeeklyClasses}");
        Console.WriteLine();
    }

}