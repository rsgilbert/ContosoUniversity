using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            // Look for any students
            if (context.Students.Any())
            {
                Console.WriteLine("Found one");
                // DB has data or has been seeded
                return;
            }
            Console.WriteLine("None found");

            // students
            var studentPM = new Student { FirstMidName = "Peter", LastName = "Mayaga", EnrollmentDate = DateTime.Parse("2015-05-03") };
            var studentJK = new Student { FirstMidName = "John", LastName = "Kampulunguse", EnrollmentDate = DateTime.Parse("2018-12-30") };
            var studentWS = new Student { FirstMidName = "Were", LastName = "Simon", EnrollmentDate = DateTime.Parse("2020-10-21") };
            var studentCJ = new Student { FirstMidName = "Collins", LastName = "Jackson", EnrollmentDate = DateTime.Parse("2009-01-02") };
            var studentMS = new Student { FirstMidName = "Matthew", LastName = "Sekitoleko", EnrollmentDate = DateTime.Parse("2021-3-23") };

            // instructors
            var instructor1 = new Instructor { FirstMidName = "John", LastName = "Ung", HireDate = DateTime.Parse("2001-04-14") };
            var instructor2 = new Instructor { FirstMidName = "Kabenge", LastName = "Moses", HireDate = DateTime.Parse("2005-01-29") };
            var instructor3 = new Instructor { FirstMidName = "Ssenoga", LastName = "Abram", HireDate = DateTime.Parse("2001-11-02") };
            var instructor4 = new Instructor { FirstMidName = "Wandegeya", LastName = "Kulekana", HireDate = DateTime.Parse("2015-10-30") };

            // OfficeAssignments
            var officeAssignments = new OfficeAssignment[]
            {
                new OfficeAssignment{Instructor=instructor1,Location="LLT-1A"},
                new OfficeAssignment{Instructor=instructor3, Location="LLT-2B"},
                new OfficeAssignment{Instructor=instructor4,Location="Big lab A" }
            };
            context.AddRange(officeAssignments);

            // departments
            var english = new Department
            {
                Name = "English",
                Budget = 300000,
                StartDate = DateTime.Parse("1990-02-15"),
                Administrator = instructor1
            };
            var mathematics = new Department
            {
                Name = "Mathematics",
                Budget = 250000,
                StartDate = DateTime.Parse("1960-10-25"),
                Administrator = instructor2
            };
            var cs = new Department
            {
                Name = "Computer Science",
                Budget = 500000,
                StartDate = DateTime.Parse("1990-09-22"),
                Administrator = instructor3
            };

            // courses
            var chemistry = new Course
            {
                CourseID = 1050,
                Title = "Chemistry",
                Credits = 3,
                Department = mathematics,
                Instructors = new List<Instructor> { instructor1, instructor2 }
            };
            var SE = new Course
            {
                CourseID = 5060,
                Title = "Software Engineering",
                Credits = 4,
                Department = cs,
                Instructors = new List<Instructor> { instructor2, instructor3 }
            };
            var discreteMaths = new Course
            {
                CourseID = 5090,
                Title = "Discrete Mathematics",
                Credits = 5,
                Department = cs,
                Instructors = new List<Instructor> { instructor2, instructor3, instructor4 }
            };

            // Enrollments
            var enrollments = new Enrollment[]
            {
                new Enrollment
                {
                    Student = studentCJ,
                    Course = chemistry,
                    Grade = Grade.D
                },
                new Enrollment
                {
                    Student = studentJK,
                    Course= discreteMaths
                },
                new Enrollment
                {
                    Student = studentPM,
                    Course = SE,
                    Grade = Grade.A
                },
                new Enrollment
                {
                    Student= studentJK,
                    Course = SE,
                    Grade =Grade.B
                },
                new Enrollment
                {
                    Student =studentMS,
                    Course = chemistry
                }
                // studentWS not used
            };

            context.AddRange(enrollments);

            // Save changes
            context.SaveChanges();
        }
    }
}
