using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ef_core
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a database variable from AssignmentContext
            using var db = new AssignmentContext();

            //Generate Id`s for both Employees and Assignments
            var assignmentId = Guid.NewGuid();
            var employeeId = System.Guid.NewGuid();


            //Add an Assignment
            Console.Write("Adding an assignment");
            using (var ctx = new AssignmentContext())
            {
                var employee = new Employee() { Id = employeeId, FirstName = "Deniz", LastName = "Acikbas" };
                ctx.Employees.Add(employee);
                var assign = new Assignment() { Id = assignmentId, Name = "Linux Systems", StartDate = new DateTime(2022, 5, 12), EndDate = new DateTime(2022, 5, 19), EmployeeId = employeeId};
                ctx.Assignments.Add(assign);
                ctx.SaveChanges();
            }

            //Space
            Console.WriteLine();

            //Add another Assignment
            Console.Write("Adding another assignment");
            var assignmentId_2 = Guid.NewGuid();
            using (var ctx = new AssignmentContext())
            {
                var employeeId_2 = System.Guid.NewGuid();
                var employee = new Employee() { Id = employeeId_2, FirstName = "John", LastName = "Doe" };
                ctx.Employees.Add(employee);
                var assign = new Assignment() { Id = assignmentId_2, Name = "Android Development", StartDate = new DateTime(2022, 5, 12), EndDate = new DateTime(2022, 5, 19), EmployeeId = employeeId_2 };
                ctx.Assignments.Add(assign);
                ctx.SaveChanges();
            }

            //Space
            Console.WriteLine();

            //Query based on an Assignment (with keeping on mind that there might be no elements)
            Console.WriteLine("Querying for an assignment");
            var assignment = db.Assignments
                .FirstOrDefault(x => x.Id == assignmentId);

            //Edit the Assignment again
            Console.WriteLine("Updating the assignment and changing the name");
            assignment.Name = ".Net";
            assignment.EndDate = new DateTime(2022, 5, 29);
            db.SaveChanges();

            //Space
            Console.WriteLine();

            //Demonstrate Eager Loading by loading assignments
            Console.WriteLine("Eager Loading (Assignments)...");
            using (var context = new AssignmentContext())
            {
            var assignments = context.Assignments
            .Include(a => a.Employee)
            .ToList();

                foreach (var asgnment in assignments)
                {
                    Console.WriteLine(asgnment.Name + " " + asgnment.StartDate + " " + asgnment.EndDate + " " + asgnment.Id);
                }
            }


            //Space
            Console.WriteLine();

            //Demonstrate Lazy Loading by loading the employees
            Console.WriteLine("Lazy Loading (Employees)...");
            using (var context = new AssignmentContext())
            {
                //Load employees only
                IList<Employee> employees = context.Employees.ToList<Employee>();
                foreach (var employee in employees)
                {
                    Console.WriteLine(employee.FirstName + " " + employee.LastName + " " + employee.Id);
                }
            }
            Console.WriteLine();

            //Deleting an Assignment
            Console.WriteLine("Deleting the assignment");
            db.Remove(assignment);
            db.SaveChanges();

            /*
             * References:
             * https://www.entityframeworktutorial.net/code-first/simple-code-first-example.aspx
             * https://www.entityframeworktutorial.net/code-first/foreignkey-dataannotations-attribute-in-code-first.aspx
             * https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=visual-studio
             * https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/managing?tabs=vs
             * https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs
             * https://www.tutorialspoint.com/entity_framework/entity_framework_eager_loading.htm
             * https://www.tutorialspoint.com/entity_framework/entity_framework_lazy_loading.htm
             */

            /*
             * Notes
             * ######################################################
             * 1- Sort by Assignment Id 
             * Find the difference between first, find, first or default
             * Definitions:
             * First - Use it if you expect the sequence to have at least one element
             * FirstOrDefault - Checks whether there was an element or not.
             * 
             * Decision:
             * FirstOrDefault
             * 2-Demo how eager loading and lazy loading works
             * 
             * Definitions:
             * Eager Loading - Eager Loading helps you to load all your needed entities at once; i.e., all your child entities will 
             * be loaded at single database call. 
             * Lazy Loading - It is the default behavior of an Entity Framework, where a child entity is loaded only when it is accessed for the first time.
             * It simply delays the loading of the related data, until you ask for it.
             */

            /*
             * Output:
             * Adding an assignment
             * Adding another assignment
             * Querying for an assignment
             * Updating the assignment and changing the name
             * 
             * Eager Loading (Assignments)...
             * Open Source Software 5/12/2022 12:00:00 AM 5/19/2022 12:00:00 AM 8ac2acc7-2aed-4597-9df0-0f55bb6e3151
             * Closed Source Software 5/12/2022 12:00:00 AM 5/29/2022 12:00:00 AM 439b6e92-8b94-4a83-ac06-23c395727634
             * Open Source Software 5/12/2022 12:00:00 AM 5/19/2022 12:00:00 AM e4489bcb-e2d1-48b3-8745-6952aaf1f03b
             * Closed Source Software 5/12/2022 12:00:00 AM 5/29/2022 12:00:00 AM 606ff672-1db5-4ca0-84b3-d449833177a6
             * 
             * Lazy Loading (Employees)...
             * John Doe e2305a11-4013-4b8f-877d-32f46c55b85c
             * John Doe ce8fdeac-8227-46dc-9874-9be19bfcc7aa
             * Deniz Acikbas 94ffbc1e-bf1c-4616-921f-c47c952d9ea2
             * Deniz Acikbas c5f6aec0-a723-4ef6-ba2a-c8426042a90a
             * Melisa Tarpids b2069c9b-7ef8-4c11-a59c-e800f3f1d041
             * 
             * Deleting the assignment
             */
        }
    }
}