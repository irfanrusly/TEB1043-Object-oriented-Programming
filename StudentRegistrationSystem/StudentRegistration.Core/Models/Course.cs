using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentRegistration.Core.Models
{
    public class Course
    {
        private readonly object _lock = new object();

        public string CourseCode { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public List<string> Prerequisites { get; set; } = new List<string>();

        public List<Student> EnrolledStudents { get; private set; } = new List<Student>();
        public Queue<Student> Waitlist { get; private set; } = new Queue<Student>();

        public bool TryRegister(Student student, out string resultMessage)
        {
            lock (_lock)
            {
                if (EnrolledStudents.Any(s => s.UserId == student.UserId))
                {
                    resultMessage = $"[ALREADY ENROLLED] Student {student.Name} is already in {CourseCode}.";
                    return false;
                }

                foreach (var prereq in Prerequisites)
                {
                    if (!student.CompletedCourses.Contains(prereq, StringComparer.OrdinalIgnoreCase))
                    {
                        resultMessage = $"[PREREQUISITE ERROR] {student.Name} missing required prerequisite '{prereq}' for {CourseCode}.";
                        return false;
                    }
                }

                if (EnrolledStudents.Count < Capacity)
                {
                    EnrolledStudents.Add(student);
                    student.EnrolledCourses.Add(this);
                    resultMessage = $"[SUCCESS] {student.Name} successfully enrolled in {CourseCode}.";
                    return true;
                }
                else
                {
                    Waitlist.Enqueue(student);
                    resultMessage = $"[WAITLISTED] {CourseCode} is at full capacity ({Capacity}/{Capacity}). {student.Name} added to waitlist.";
                    return false;
                }
            }
        }

        public bool Drop(Student student, out string resultMessage)
        {
            lock (_lock)
            {
                if (EnrolledStudents.Remove(student))
                {
                    student.EnrolledCourses.Remove(this);
                    resultMessage = $"[DROPPED] {student.Name} dropped from {CourseCode}.";

                    if (Waitlist.Count > 0)
                    {
                        var nextStudent = Waitlist.Dequeue();
                        EnrolledStudents.Add(nextStudent);
                        nextStudent.EnrolledCourses.Add(this);
                        resultMessage += $"\n[WAITLIST PROMOTION] {nextStudent.Name} promoted from waitlist to enrolled!";
                    }

                    return true;
                }

                resultMessage = $"[DROP ERROR] {student.Name} is not enrolled in {CourseCode}.";
                return false;
            }
        }
    }
}
