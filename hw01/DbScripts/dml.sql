
-- Instructors
delete from public."Instructors" where id between 1 and 5;
INSERT INTO public."Instructors" (id, "LastName", "FirstName") VALUES(1, 'LastName 1', 'FirstName 1');
INSERT INTO public."Instructors" (id, "LastName", "FirstName") VALUES(2, 'LastName 2', 'FirstName 2');
INSERT INTO public."Instructors" (id, "LastName", "FirstName") VALUES(3, 'LastName 3', 'FirstName 3');
INSERT INTO public."Instructors" (id, "LastName", "FirstName") VALUES(4, 'LastName 4', 'FirstName 4');
INSERT INTO public."Instructors" (id, "LastName", "FirstName") VALUES(5, 'LastName 5', 'FirstName 5');

-- Cources
delete from public."Courses" where id between 1 and 5;
INSERT INTO public."Courses" (id, "Title") VALUES(1, 'Title 1');
INSERT INTO public."Courses" (id, "Title") VALUES(2, 'Title 2');
INSERT INTO public."Courses" (id, "Title") VALUES(3, 'Title 3');
INSERT INTO public."Courses" (id, "Title") VALUES(4, 'Title 4');
INSERT INTO public."Courses" (id, "Title") VALUES(5, 'Title 5');

-- CourseInstructor
delete from public."CourseInstructor" where "Instructorsid" between 1 and 5;
INSERT INTO public."CourseInstructor" ("Coursesid", "Instructorsid") VALUES(1, 1);
INSERT INTO public."CourseInstructor" ("Coursesid", "Instructorsid") VALUES(2, 2);
INSERT INTO public."CourseInstructor" ("Coursesid", "Instructorsid") VALUES(3, 3);
INSERT INTO public."CourseInstructor" ("Coursesid", "Instructorsid") VALUES(4, 4);
INSERT INTO public."CourseInstructor" ("Coursesid", "Instructorsid") VALUES(5, 5);

-- OfficeAssignments
delete from public."OfficeAssignments" where "InstructorID" between 1 and 5;
INSERT INTO public."OfficeAssignments" ("InstructorID", "Location") VALUES(1, 'Location 1');
INSERT INTO public."OfficeAssignments" ("InstructorID", "Location") VALUES(2, 'Location 2');
INSERT INTO public."OfficeAssignments" ("InstructorID", "Location") VALUES(3, 'Location 3');
INSERT INTO public."OfficeAssignments" ("InstructorID", "Location") VALUES(4, 'Location 4');
INSERT INTO public."OfficeAssignments" ("InstructorID", "Location") VALUES(5, 'Location 5');

