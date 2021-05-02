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
            if(context.Students.Any())
            {
                // DB has data or has been seeded
                return;
            }
            var students = new Student[]
            {
                new Student {FirstMidName="Peter", LastName="Mayaga", EnrollmentDate=DateTime.Parse("2015-05-03")},
                new Student {FirstMidName="John", LastName="Kampulunguse", EnrollmentDate=DateTime.Parse("2018-12-30")},
                new Student {FirstMidName="Were", LastName="Simon", EnrollmentDate=DateTime.Parse("2020-10-21")},
                new Student {FirstMidName="Collins", LastName="Jackson", EnrollmentDate=DateTime.Parse("2009-01-02")},
                new Student {FirstMidName="Matthew", LastName="Sekitoleko", EnrollmentDate=DateTime.Parse("2021-3-23")}
            };
            context.Students.AddRange(students);
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course { CourseID=1000, Title="Graph Theory", Credits=3},
                new Course { CourseID=1010, Title="Theory of Computation", Credits=5},
                new Course { CourseID=1020, Title="Data Structures and Algorithms", Credits=5},
                new Course { CourseID=1030, Title="Probability Theory", Credits=3},
                new Course { CourseID=1040, Title="Linear Algebra", Credits=3},
                new Course { CourseID=1050, Title="Calculus", Credits=3}
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentID=1,CourseID=1000,Grade=Grade.A},
                new Enrollment {StudentID=2, CourseID=1000, Grade=Grade.B},
                new Enrollment{StudentID=1,CourseID=1020, Grade=Grade.D},
                new Enrollment{StudentID=3, CourseID=1030},
                new Enrollment{StudentID=3, CourseID=1020},
                new Enrollment{StudentID=2, CourseID=1030},
                new Enrollment{StudentID=5, CourseID=1040}
            };
            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();
        }
    }
}
