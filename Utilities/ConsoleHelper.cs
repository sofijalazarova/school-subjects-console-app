public static class ConsoleHelper
{
    public static void DisplayWelcomeMessage()
    {
        Console.Title = "🎓 School Subjects Information System";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=====================================");
        Console.WriteLine("Welcome to Our Online School System!");
        Console.WriteLine("=====================================");
        Console.ResetColor();
    }

    public static void GoodbyeMessage()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n👋 Thank you for using the School Subjects System. Goodbye!");
        Console.ResetColor();
    }
}