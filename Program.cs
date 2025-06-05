// using SchoolSubjectsSystem.DomainModels;
// using SchoolSubjectsSystem.Repositories.Implementation;
// using SchoolSubjectsSystem.Services.Implementation;

// ConsoleHelper.DisplayWelcomeMessage();

// var service = new SubjectService(new SubjectRepository());
// var subjects = service.GetAll();

// while (true)
// {
//     Console.WriteLine();
//     Console.ForegroundColor = ConsoleColor.Yellow;
//     Console.WriteLine("📚 List of Available Subjects:");
//     Console.ResetColor();
//     foreach (var s in subjects)
//     {
//         Console.WriteLine($"{s.Id}. {s.Name}");
//     }
//     Console.WriteLine("0. Exit");
//     Console.WriteLine("-1. Add a new subject");
//     Console.WriteLine("-2. Update an existing subject");
//     Console.Write("\nEnter subject number to view details or choose some of the options: ");

//     if (int.TryParse(Console.ReadLine(), out int choice))
//     {
//         if (choice == 0)
//         {
//             Console.ForegroundColor = ConsoleColor.Green;
//             Console.WriteLine("\n👋 Thank you for using the School Subjects System. Goodbye!");
//             Console.ResetColor();
//             break;

//         }
//         if (choice == -1)
//         {
//             Console.Write("\nEnter subject name: ");
//             string ?name = Console.ReadLine();

//             Console.Write("\nEnter subject description: ");
//             string ?description = Console.ReadLine();

//             Console.Write("Enter number of weekly classes: ");
//             if (int.TryParse(Console.ReadLine(), out int weeklyclasses))
//             {
//                 var newSubject = new Subject
//                 {
//                     Name = name,
//                     Description = description,
//                     WeeklyClasses = weeklyclasses,
//                 };

//                 service.AddSubject(newSubject);
//                 Console.ForegroundColor = ConsoleColor.Green;
//                 Console.WriteLine("\n Subject added successfully!");

//                 subjects = service.GetAll();
//                 Console.ResetColor();
//             } else {
//                 Console.ForegroundColor = ConsoleColor.Red;
//                 Console.WriteLine("Invalid number for weekly classes.");
//                 Console.ResetColor();
//             }

//             continue;
//         }

//         if (choice == -2)
//         {
//             Console.Write("\nEnter the ID of the subject you want to edit: ");
//             if (int.TryParse(Console.ReadLine(), out int subjectId))
//             {
//                 var subjectToEdit = subjects.FirstOrDefault(s => s.Id == subjectId);
//                 if (subjectToEdit != null)
//                 {
//                     Console.Write($"Enter new name (or leave empty to keep '{subjectToEdit.Name}')");
//                     string ?newName = Console.ReadLine();
//                     if (!string.IsNullOrWhiteSpace(newName))
//                     {
//                         subjectToEdit.Name = newName;
//                     }

//                     Console.Write($"Enter new description (or leave empty to keep current): ");
//                     string ?newDescription = Console.ReadLine();
//                     if (!string.IsNullOrWhiteSpace(newDescription))
//                         subjectToEdit.Description = newDescription;

//                     Console.Write($"Enter new number of weekly classes (or leave empty to keep {subjectToEdit.WeeklyClasses}): ");
//                     string ?weeklyInput = Console.ReadLine();
//                     if (int.TryParse(weeklyInput, out int newWeeklyClasses))
//                         subjectToEdit.WeeklyClasses = newWeeklyClasses;

//                     service.UpdateSubject(subjectToEdit);
//                     Console.ForegroundColor = ConsoleColor.Green;
//                     Console.WriteLine("✅ Subject updated successfully!");
//                     Console.ResetColor();

//                     subjects = service.GetAll();
//                 }
//                 else
//                 {
//                     Console.ForegroundColor = ConsoleColor.Red;
//                     Console.WriteLine("Subject not found.");
//                     Console.ResetColor();
//                 }
//             }
//             else
//             {
//                 Console.ForegroundColor = ConsoleColor.Red;
//                 Console.WriteLine("Invalid ID input.");
//                 Console.ResetColor();
//             }
//             continue;      
//         }


//         var subject = subjects.FirstOrDefault(s => s.Id == choice);
//         if (choice > 0 && choice <= subjects.Count)
//         {

//             if (subject != null)
//             {
//                 Console.ForegroundColor = ConsoleColor.Cyan;
//                 Console.WriteLine($"\n📘 Subject Details for: {subject.Name}");
//                 Console.ResetColor();
//                 subject.DisplayDetails();

//                 var books = service.GetLiteratureBySubjectId(choice);

//                 if (books.Any())
//                 {
//                     Console.WriteLine("\n📖 Available Literature:");
//                     foreach (var b in books)
//                     {
//                         Console.WriteLine($"{b.Id}. {b.Title}");
//                     }
//                 }

//                 Console.Write("\n🔍 Enter book ID to read: ");

//                 if (int.TryParse(Console.ReadLine(), out int bookId))
//                 {
//                     var book = books.FirstOrDefault(b => b.Id == bookId);
//                     if (book != null)
//                     {
//                         Console.WriteLine($"\n'{book.Title}' written by {book.Author} in {book.ReleaseYear}.");

//                         Console.WriteLine($"\n{book.Content}");
//                         Console.Write("\nDo you want to summarize this book? (yes/no) ");

//                         string ?answer = Console.ReadLine()?.ToLower();

//                         if (answer == "yes")
//                         {
//                             var ollama = new OllamaService();
//                             Console.WriteLine("\nSumarizing, please wait...");
//                             string summary = await ollama.SummarizeAsync(book.Content);
//                             Console.WriteLine("\nSummarized version:\n");
//                             Console.WriteLine(summary);
//                         }

//                     }
//                     else
//                     {
//                         Console.ForegroundColor = ConsoleColor.Red;
//                         Console.WriteLine("Book not found.");
//                         Console.ResetColor();
//                     }
//                 }
//             }
//         }
//         else
//         {
//             Console.WriteLine("Invalid choice");
//         }
//     }
// }


using SchoolSubjectsSystem.Repositories.Implementation;
using SchoolSubjectsSystem.Services.Implementation;

ConsoleHelper.DisplayWelcomeMessage();

var repository = new SubjectRepository();
var service = new SubjectService(repository);

var ui = new UserInterface(service);
await ui.RunAsync();



