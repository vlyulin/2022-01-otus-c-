using hw01.DAL;
using hw01.Model;

using (ApplicationContext db = new ApplicationContext())
{
    // Instructors
    Console.WriteLine("Instructors:");
    Console.WriteLine("------------");
    var instructors = db.Instructors.ToList<Instructor>();

    // Data output
    instructors.ForEach(instructor => {
        db.Entry(instructor).Collection(i => i.Courses).Load();
        instructor.FormatedOutput();

        // Locations
        Console.WriteLine("\tInstructor's locations:");
        var officeAssignments = db.OfficeAssignments.Where(oa => oa.InstructorID == instructor.id);
        officeAssignments.ToList().ForEach(officeAssignment => {
            Console.WriteLine("\t{0}", officeAssignment.Location);
        });
    });

    // Entering instructor
    Console.WriteLine("\n");
    Instructor newInstructor = InstructorAddition();
    if (newInstructor != null)
    {
        db.Instructors.Add(newInstructor);
        db.SaveChanges();
    }

    var createdInstructor = db.Instructors
        .OrderByDescending(Instructor => Instructor.id)
        .FirstOrDefault();

    if (createdInstructor != null) {
        Console.WriteLine("Created instructor:");
        createdInstructor.FormatedOutput();
    }
}

Instructor InstructorAddition()
{
    Instructor instructor = new Instructor();
    Console.WriteLine("Enter instructor first name:");
    instructor.FirstName = Console.ReadLine();
    Console.WriteLine("Enter instructor last name:");
    instructor.LastName = Console.ReadLine();

    return instructor;
}
