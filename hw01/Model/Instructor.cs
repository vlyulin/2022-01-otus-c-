using System.ComponentModel.DataAnnotations;

namespace hw01.Model
{
    internal class Instructor
    {
        [Key]
        public int id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

        public void FormatedOutput()
        {
            Console.WriteLine("\n{0}: {1} {2}", id, FirstName, LastName);
            // Output instructor's cources
            Console.WriteLine("\tInstructor's courses:");
            Courses.ToList().ForEach(course => {
                Console.WriteLine("\t{0}", course.Title);
            });            
        }
    }
}
