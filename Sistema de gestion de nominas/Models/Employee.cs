using System;
using System.Collections.Generic;

namespace Sistema_de_gestion_de_nominas.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Payroll = new HashSet<Payroll>();
        }

        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Display(Name = "Sexo")]
        public string Genre { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Count must be a natural number")]
        [Display(Name = "Salario bruto")]
        public decimal? GrossSalary { get; set; }
        [Display(Name = "Activo")]
        public bool Active { get; set; }

        public virtual ICollection<Payroll> Payroll { get; set; }
    }
}
