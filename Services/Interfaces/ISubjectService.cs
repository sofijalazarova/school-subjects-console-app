using SchoolSubjectsSystem.DomainModels;

namespace SchoolSubjectsSystem.Services.Interfaces;

public interface ISubjectService
{
    List<Subject> GetAll();
    void AddSubject(Subject subject);
    void UpdateSubject(Subject subject);
    void AddLiterature(Literature literature);
    List<Literature> GetLiteratureBySubjectId(int subjectId);
}