using SchoolSubjectsSystem.Repositories.Implementation;
using SchoolSubjectsSystem.Services.Implementation;

ConsoleHelper.DisplayWelcomeMessage();

var repository = new SubjectRepository();
var service = new SubjectService(repository);

var ui = new UserInterface(service);
await ui.RunAsync();



