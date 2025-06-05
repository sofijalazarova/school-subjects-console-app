using SchoolSubjectsSystem.DomainModels;
using SchoolSubjectsSystem.Services.Implementation;

public class UserInterface
{
    private readonly SubjectService _service;

    public UserInterface(SubjectService service)
    {
        _service = service;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📚 List of Available Subjects:");
            Console.ResetColor();

            var subjects = _service.GetAll();
            foreach (var s in subjects)
            {
                Console.WriteLine($"{s.Id}. {s.Name}");
            }

            Console.WriteLine("0. Exit");
            Console.WriteLine("-1. Add a new subject");
            Console.WriteLine("-2. Update an existing subject");
            Console.Write("\nEnter subject number to view details or choose some of the options: ");

            if (!int.TryParse(Console.ReadLine(), out int choice)) continue;

            switch (choice)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n👋 Thank you for using the School Subjects System. Goodbye!");
                    Console.ResetColor();
                    return;

                case -1:
                    AddSubject();
                    break;

                case -2:
                    UpdateSubject(subjects);
                    break;

                default:
                    await ViewSubjectDetailsAsync(choice, subjects);
                    break;
            }
        }
    }

    private void AddSubject()
    {
        Console.Write("\nEnter subject name: ");
        string? name = Console.ReadLine();

        Console.Write("Enter subject description: ");
        string? description = Console.ReadLine();

        Console.Write("Enter number of weekly classes: ");
        if (int.TryParse(Console.ReadLine(), out int weeklyClasses))
        {
            var newSubject = new Subject
            {
                Name = name,
                Description = description,
                WeeklyClasses = weeklyClasses
            };

            _service.AddSubject(newSubject);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Subject added successfully!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❗ Invalid number for weekly classes.");
        }
        Console.ResetColor();
    }

    private void UpdateSubject(List<Subject> subjects)
    {
        Console.Write("\nEnter the ID of the subject you want to edit: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var subject = subjects.FirstOrDefault(s => s.Id == id);
            if (subject == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❗ Subject not found.");
                Console.ResetColor();
                return;
            }

            Console.Write($"Enter new name (or leave empty to keep '{subject.Name}'): ");
            var newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
                subject.Name = newName;

            Console.Write("Enter new description (or leave empty to keep current): ");
            var newDesc = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDesc))
                subject.Description = newDesc;

            Console.Write($"Enter new weekly classes (or leave empty to keep {subject.WeeklyClasses}): ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int newClasses))
                subject.WeeklyClasses = newClasses;

            _service.UpdateSubject(subject);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Subject updated successfully!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❗ Invalid ID input.");
        }
        Console.ResetColor();
    }

    private async Task ViewSubjectDetailsAsync(int subjectId, List<Subject> subjects)
    {
        var subject = subjects.FirstOrDefault(s => s.Id == subjectId);
        if (subject == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❗ Subject not found.");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n📘 Subject Details for: {subject.Name}");
        Console.ResetColor();
        subject.DisplayDetails();

        var books = _service.GetLiteratureBySubjectId(subjectId);
        if (!books.Any())
        {
            Console.WriteLine("No books available.");
            return;
        }

        Console.WriteLine("\n📖 Available Literature:");
        foreach (var book in books)
            Console.WriteLine($"{book.Id}. {book.Title}");

        Console.Write("\n🔍 Enter book ID to read: ");
        if (!int.TryParse(Console.ReadLine(), out int bookId)) return;

        var selectedBook = books.FirstOrDefault(b => b.Id == bookId);
        if (selectedBook == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❗ Book not found.");
            Console.ResetColor();
            return;
        }

        Console.WriteLine($"\n'{selectedBook.Title}' by {selectedBook.Author} ({selectedBook.ReleaseYear})");
        Console.WriteLine($"\n{selectedBook.Content}");

        Console.Write("\nDo you want to summarize this book? (yes/no): ");
        string? answer = Console.ReadLine()?.ToLower();
        if (answer == "yes")
        {
            Console.WriteLine("\nSummarizing, please wait...");
            var ollama = new OllamaService();
            string summary = await ollama.SummarizeAsync(selectedBook.Content);
            Console.WriteLine("\n📄 Summary:\n");
            Console.WriteLine(summary);
        }
    }
}