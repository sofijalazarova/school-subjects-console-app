namespace SchoolSubjectsSystem.DomainModels;

public class Literature
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public int ReleaseYear { get; set; }
    public required string Content { get; set; }
}