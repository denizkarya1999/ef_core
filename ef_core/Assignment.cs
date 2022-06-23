using System;
using System.Collections.Generic;
//Implement DataAnnotations to create Keys
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_core
{
    public class Assignment
    {
        //Constructors of the Assignment Class

        //Id is going to be the primary key
        [Key]
        public Guid Id { get; set; }

        //Standard constructors (Name, StartDate and EndDate)
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //EmployeeId is going to be the foreign key and navigational property is Employee
        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }


    }
}
