using hw01.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw01.DAL
{
    internal class DBInitialize
    {
        public static void Initialize(ApplicationContext context)
        {
            if (context == null) return;

            if (context.Instructors.Any())
            {
                return; // DB has been initialized
            }

            Instructor instructor1 = new Instructor {
                id = 1, 
                FirstName = "Inst 1", 
                LastName = "Inst 1"
            };

            Instructor instructor2 = new Instructor
            {
                id = 2,
                FirstName = "Inst 2",
                LastName = "Inst 2"
            };

            var instructors = new List<Instructor>
            {
                instructor1,
                instructor2
            };

            Course course1 = new Course
            {
                id = 1,
                Title = "Course 1"
            };

            Course course2 = new Course
            {
                id = 2,
                Title = "Course 2"
            };
            var courses = new List<Course>
            {
                course1,
                course2
            };

            instructor1.Courses = courses;
            instructor2.Courses = courses;

            instructors.ForEach(i => context.Instructors.Add(i));

            OfficeAssignment office1 = new OfficeAssignment { Location = "office 1", Instructor = instructor1 };
            OfficeAssignment office2 = new OfficeAssignment { Location = "office 2", Instructor = instructor2 };
            var offices = new List<OfficeAssignment> { office1, office2 };
            offices.ForEach(o => context.OfficeAssignments.Add(o));

            context.SaveChanges();
        }
    }
}
