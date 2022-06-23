using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_core
{
    public class Employee
    {
        //The primary key is going to be the Id
        [Key]
        public Guid Id { get; set; }

        //Set maximum length of FirstName and LastName strings to 100
        [MaxLength(100)]

        //Standard constructors (FirstName and LastName) 
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Make a collection for assignments
        public ICollection<Assignment> Assignments { get; set; }

    }
}
