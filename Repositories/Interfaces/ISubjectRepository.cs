using SchoolSubjectsSystem.DomainModels;

namespace SchoolSubjectsSystem.Repositories;

public interface ISubjectRepository
{
    public List<Subject> GetAllSubjects();
    public List<Literature> GetLiteratureBySubjectId(int subjectId);
    void AddSubject(Subject subject);
    void UpdateSubject(Subject subject);
    void AddLiterature(Literature literature);
}