using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_gestion_de_nominas.Models
{
    public class EmployeeViewModel
    {
        public IEnumerable<Employee> employeeList { get; set; }
        public bool isReport { get; set; }
    }
}
