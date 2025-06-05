using Npgsql;
using SchoolSubjectsSystem.DomainModels;

namespace SchoolSubjectsSystem.Repositories.Implementation;

public class SubjectRepository : ISubjectRepository
{
    public List<Subject> GetAllSubjects()
    {
        var subjects = new List<Subject>();

        using var connection = Database.GetConnection();
        connection.Open();

        using var cmd = new NpgsqlCommand("SELECT * FROM Subjects", connection);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            subjects.Add(new Subject
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                WeeklyClasses = reader.GetInt32(3)
            });
        }

        return subjects;
    }

    public List<Literature> GetLiteratureBySubjectId(int subjectId)
    {
        var books = new List<Literature>();
        using var connection = Database.GetConnection();
        connection.Open();
        using var cmd = new NpgsqlCommand("SELECT * FROM Literature WHERE SubjectId = @id", connection);
        cmd.Parameters.AddWithValue("id", subjectId);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            books.Add(new Literature
            {
                Id = reader.GetInt32(0),
                SubjectId = reader.GetInt32(1),
                Title = reader.GetString(2),
                Author = reader.GetString(3),
                ReleaseYear = reader.GetInt16(4),
                Content = reader.GetString(5),              
            });
        }

        return books;
    }

    public void AddSubject(Subject subject)
    {
        using var connection = Database.GetConnection();
        connection.Open();

        using var cmd = new NpgsqlCommand("INSERT INTO subjects (name, description, weeklyclasses) VALUES (@name, @description, @weeklyclasses)", connection);
        cmd.Parameters.AddWithValue("name", subject.Name);
        cmd.Parameters.AddWithValue("description", subject.Description);
        cmd.Parameters.AddWithValue("weeklyclasses", subject.WeeklyClasses);

        cmd.ExecuteNonQuery();

    }

    public void UpdateSubject(Subject subject)
    {
        using var connection = Database.GetConnection();
        connection.Open();

        using var cmd = new NpgsqlCommand("UPDATE subjects SET name = @name, description = @description, weeklyclasses = @weeklyclasses WHERE id = @id", connection);
        cmd.Parameters.AddWithValue("id", subject.Id);
        cmd.Parameters.AddWithValue("name", subject.Name);
        cmd.Parameters.AddWithValue("description", subject.Description ?? "");
        cmd.Parameters.AddWithValue("weeklyclasses", subject.WeeklyClasses);

        cmd.ExecuteNonQuery();
    }

    public void AddLiterature(Literature literature)
    {
        using var connection = Database.GetConnection();
        connection.Open();
        using var cmd = new NpgsqlCommand("INSERT INTO literature (subjectid, title, content, author, releaseyear) VALUES (@subjectid, @title, @content, @author, @year)", connection);
        cmd.Parameters.AddWithValue("subjectid", literature.SubjectId);
        cmd.Parameters.AddWithValue("title", literature.Title);
        cmd.Parameters.AddWithValue("content", literature.Content);
        cmd.Parameters.AddWithValue("author", literature.Author);
        cmd.Parameters.AddWithValue("year", literature.ReleaseYear);

        cmd.ExecuteNonQuery();
    }
}