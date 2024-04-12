using SQLite;

namespace lab_14.Persistence;

public interface IStorageManager
{
    IEnumerable<Course> GetCourses();
    void AddCourse(Course course);
    void AddAssignmentGroup(AssignmentGroup group);
    void AddAssignment(Assignment assignment);
    Assignment GetAssignment(int assignmentId);
    void UpdateAssignment(Assignment assignment);

    public static IStorageManager Instance { get; } = new SQLiteStorageManager("courses.db");
}

public class SQLiteStorageManager : IStorageManager
{
    private readonly SQLiteConnection _connection;
    public SQLiteStorageManager(string dbPath)
    {
        _connection = new SQLiteConnection(dbPath);
        _connection.CreateTable<Course>();
        _connection.CreateTable<AssignmentGroup>();
        _connection.CreateTable<Assignment>();
    }
    public IEnumerable<Course> GetCourses()
    {
        var courses = _connection.Table<Course>().ToList();
        var groups = _connection.Table<AssignmentGroup>().ToList();
        var assignments = _connection.Table<Assignment>().ToList();
        var filledCourses = courses.Select(course =>
        {
            course.AssignmentGroups = groups.Where(group => group.CourseId == course.Id).ToList();
            course.AssignmentGroups.ForEach(group =>
            {
                group.Assignments = assignments.Where(assignment => assignment.AssignmentGroupId == group.Id).ToList();
            });
            return course;
        });

        return filledCourses;
    }
    public void AddCourse(Course course)
    {
        _connection.Insert(course);
    }
    public void AddAssignmentGroup(AssignmentGroup group)
    {
        _connection.Insert(group);
    }
    public void AddAssignment(Assignment assignment)
    {
        _connection.Insert(assignment);
    }
    public Assignment GetAssignment(int assignmentId)
    {
        return _connection.Table<Assignment>().FirstOrDefault(assignment => assignment.Id == assignmentId) ?? throw new AssignmentNotFoundException();
    }
    public void UpdateAssignment(Assignment assignment)
    {
        _connection.Update(assignment);
    }
}

public class Course
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Subject { get; set; }
    public int Number { get; set; }
    public string Title { get; set; }
    [Ignore]
    public List<AssignmentGroup> AssignmentGroups { get; set; } = new();
}
public class AssignmentGroup
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Weight { get; set; }
    public int CourseId { get; set; }
    [Ignore]
    public List<Assignment> Assignments { get; set; } = new();
}
public class Assignment
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public int PointsPossible { get; set; }
    public int PointsEarned { get; set; }
    public int AssignmentGroupId { get; set; }
}
