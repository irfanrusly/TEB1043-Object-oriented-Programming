using System;
using System.Collections.Generic;
using System.Linq;
using StudentRegistration.Core.Models;

namespace StudentRegistration.Core.Services
{
    public class RegistrationService
    {
        private readonly List<string> _auditLog = new List<string>();

        public void RegisterStudentToCourse(Student student, Course course)
        {
            if (course.TryRegister(student, out string message))
            {
                LogAudit($"SUCCESS: User {student.UserId} registered for {course.CourseCode}");
            }
            else
            {
                LogAudit($"FAILED/WAITLISTED: User {student.UserId} attempted {course.CourseCode}");
            }

            Console.WriteLine($"\n{message}");
        }

        public void DropStudentFromCourse(Student student, Course course)
        {
            if (course.Drop(student, out string message))
            {
                LogAudit($"DROP: User {student.UserId} dropped {course.CourseCode}");
            }
            else
            {
                LogAudit($"DROP FAILED: User {student.UserId} failed to drop {course.CourseCode}");
            }

            Console.WriteLine($"\n{message}");
        }

        private void LogAudit(string action)
        {
            _auditLog.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {action}");
        }

        public void PrintAuditLogs()
        {
            Console.WriteLine("\n================ SYSTEM AUDIT LOGS ================");
            if (!_auditLog.Any())
            {
                Console.WriteLine("No activity logged yet.");
            }
            else
            {
                foreach (var log in _auditLog)
                {
                    Console.WriteLine(log);
                }
            }
            Console.WriteLine("===================================================");
        }
    }
}
