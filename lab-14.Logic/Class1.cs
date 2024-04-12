﻿using lab_14.Persistence;

namespace lab_14.Logic;

public class CourseLogic
{
    public Course AddCourse(string subject, int number, string title)
    {
        var course = new Course
        {
            Subject = subject,
            Number = number,
            Title = title
        };
        IStorageManager.Instance.AddCourse(course);
        return course;
    }
    public IEnumerable<Course> GetCourses()
    {
        return IStorageManager.Instance.GetCourses();
    }
    public AssignmentGroup AddAssignmentGroup(int courseId, string name, int weight)
    {
        var group = new AssignmentGroup
        {
            CourseId = courseId,
            Name = name,
            Weight = weight
        };
        IStorageManager.Instance.AddAssignmentGroup(group);
        return group;
    }
    public Assignment AddAssignment(int groupId, string name, int pointsPossible)
    {
        var assignment = new Assignment
        {
            AssignmentGroupId = groupId,
            Name = name,
            PointsPossible = pointsPossible
        };
        IStorageManager.Instance.AddAssignment(assignment);
        return assignment;
    }
    public void SetAssignmentPoints(int assignmentId, int pointsEarned)
    {
        var assignment = IStorageManager.Instance.GetAssignment(assignmentId);
        if (assignment == null)
        {
            throw new ArgumentException("Assignment not found");
        }
        assignment.PointsEarned = pointsEarned;
        IStorageManager.Instance.UpdateAssignment(assignment);
    }
}
