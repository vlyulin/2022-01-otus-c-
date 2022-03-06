using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw01.Model
{
    internal class Course
    {
        [Key]
        public int id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}
