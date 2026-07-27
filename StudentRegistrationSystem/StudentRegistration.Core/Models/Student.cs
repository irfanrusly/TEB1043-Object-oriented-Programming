using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentRegistration.Core.Models
{
    public class Student : User
    {
        public List<string> CompletedCourses { get; set; } = new List<string>();
        public List<Course> EnrolledCourses { get; set; } = new List<Course>();

        public Student()
        {
            UserRole = Role.Student;
        }

        public void DisplaySchedule()
        {
            Console.WriteLine($"\nSchedule for {Name} ({UserId}):");
            if (!EnrolledCourses.Any())
            {
                Console.WriteLine("  No enrolled courses.");
                return;
            }

            foreach (var course in EnrolledCourses)
            {
                Console.WriteLine($"  - [{course.CourseCode}] {course.Title}");
            }
        }
    }
}
