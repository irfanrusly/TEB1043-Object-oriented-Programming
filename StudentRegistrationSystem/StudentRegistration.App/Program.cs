using System;
using System.Collections.Generic;
using System.Linq;
using StudentRegistration.Core.Models;
using StudentRegistration.Core.Services;

namespace StudentRegistration.App
{
    class Program
    {
        static List<Student> students = new List<Student>();
        static List<Course> courses = new List<Course>();
        static RegistrationService registrationService = new RegistrationService();

        static void Main(string[] args)
        {
            SetupInitialData();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n==============================================");
                Console.WriteLine("   STUDENT REGISTRATION SYSTEM (INTERACTIVE)");
                Console.WriteLine("==============================================");
                Console.WriteLine("1. Add a New Student");
                Console.WriteLine("2. Add a New Course");
                Console.WriteLine("3. Register Student for a Course");
                Console.WriteLine("4. Drop a Course for a Student");
                Console.WriteLine("5. View Student Schedule");
                Console.WriteLine("6. View System Audit Logs");
                Console.WriteLine("7. Exit");
                Console.Write("\nEnter choice (1-7): ");

                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        AddCourse();
                        break;
                    case "3":
                        RegisterCourse();
                        break;
                    case "4":
                        DropCourse();
                        break;
                    case "5":
                        ViewSchedule();
                        break;
                    case "6":
                        registrationService.PrintAuditLogs();
                        break;
                    case "7":
                        exit = true;
                        Console.WriteLine("\nExiting application. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please enter a number between 1 and 7.");
                        break;
                }
            }
        }

        static void SetupInitialData()
        {
            var cs201 = new Course
            {
                CourseCode = "CS201",
                Title = "Data Structures",
                Capacity = 1,
                Prerequisites = new List<string> { "PRG101" }
            };
            courses.Add(cs201);

            var alice = new Student
            {
                UserId = "S101",
                Name = "Alice",
                CompletedCourses = new List<string> { "PRG101" }
            };
            students.Add(alice);
        }

        static void AddStudent()
        {
            Console.Write("\nEnter Student ID (e.g. S102): ");
            string id = Console.ReadLine()?.Trim();

            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine()?.Trim();

            Console.Write("Enter completed courses separated by comma (e.g. PRG101, MTH101) or leave empty: ");
            string prereqInput = Console.ReadLine()?.Trim();

            List<string> completed = string.IsNullOrEmpty(prereqInput) 
                ? new List<string>() 
                : prereqInput.Split(',').Select(p => p.Trim()).ToList();

            students.Add(new Student { UserId = id, Name = name, CompletedCourses = completed });
            Console.WriteLine($"\nStudent '{name}' ({id}) added successfully!");
        }

        static void AddCourse()
        {
            Console.Write("\nEnter Course Code (e.g. CS301): ");
            string code = Console.ReadLine()?.Trim().ToUpper();

            Console.Write("Enter Course Title: ");
            string title = Console.ReadLine()?.Trim();

            Console.Write("Enter Capacity Limit (e.g. 2): ");
            int.TryParse(Console.ReadLine()?.Trim(), out int cap);

            Console.Write("Enter required prerequisites separated by comma (e.g. PRG101) or leave empty: ");
            string prereqInput = Console.ReadLine()?.Trim();

            List<string> prereqs = string.IsNullOrEmpty(prereqInput) 
                ? new List<string>() 
                : prereqInput.Split(',').Select(p => p.Trim()).ToList();

            courses.Add(new Course { CourseCode = code, Title = title, Capacity = cap, Prerequisites = prereqs });
            Console.WriteLine($"\nCourse '{code}' added successfully!");
        }

        static void RegisterCourse()
        {
            var student = SelectStudent();
            if (student == null) return;

            var course = SelectCourse();
            if (course == null) return;

            registrationService.RegisterStudentToCourse(student, course);
        }

        static void DropCourse()
        {
            var student = SelectStudent();
            if (student == null) return;

            var course = SelectCourse();
            if (course == null) return;

            registrationService.DropStudentFromCourse(student, course);
        }

        static void ViewSchedule()
        {
            var student = SelectStudent();
            if (student != null)
            {
                student.DisplaySchedule();
            }
        }

        static Student SelectStudent()
        {
            if (!students.Any())
            {
                Console.WriteLine("\nNo students registered yet.");
                return null;
            }

            Console.WriteLine("\n--- Select Student ---");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {students[i].Name} (ID: {students[i].UserId})");
            }
            Console.Write("Select student number: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= students.Count)
            {
                return students[idx - 1];
            }

            Console.WriteLine("Invalid selection.");
            return null;
        }

        static Course SelectCourse()
        {
            if (!courses.Any())
            {
                Console.WriteLine("\nNo courses available yet.");
                return null;
            }

            Console.WriteLine("\n--- Select Course ---");
            for (int i = 0; i < courses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. [{courses[i].CourseCode}] {courses[i].Title} (Capacity: {courses[i].EnrolledStudents.Count}/{courses[i].Capacity})");
            }
            Console.Write("Select course number: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= courses.Count)
            {
                return courses[idx - 1];
            }

            Console.WriteLine("Invalid selection.");
            return null;
        }
    }
}
