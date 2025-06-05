using SchoolSubjectsSystem.DomainModels;
using SchoolSubjectsSystem.Repositories;
using SchoolSubjectsSystem.Services.Interfaces;

namespace SchoolSubjectsSystem.Services.Implementation;

public class SubjectService(ISubjectRepository repository) : ISubjectService
{

    private readonly ISubjectRepository _repository = repository;


    public List<Subject> GetAll() => _repository.GetAllSubjects();

    public void AddSubject(Subject subject)
    {
        _repository.AddSubject(subject);
    }

    public List<Literature> GetLiteratureBySubjectId(int subjectId)
    {
        return _repository.GetLiteratureBySubjectId(subjectId);
    }

    public void ShowSubjects()
    {
        var subjects = _repository.GetAllSubjects();
        foreach (var s in subjects)
        {
            Console.WriteLine($"{s.Id}. {s.Name}");
        }
    }

    public void UpdateSubject(Subject subject)
    {
        _repository.UpdateSubject(subject);
    }
}