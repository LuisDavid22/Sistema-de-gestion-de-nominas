using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_de_gestion_de_nominas.Models
{
    public partial class Payroll
    {
        public int Id { get; set; }
        [Display(Name ="Empleado")]
        [Required]
        public int EmployeeId { get; set; }
        public decimal? GrossSalary { get; set; }
        public double? RetentionAfp { get; set; }
        public double? RetentionArs { get; set; }
        public decimal? TaxableSalary { get; set; }
        public double? RetentionIsr { get; set; }
        public double? RetentionTotal { get; set; }
        public decimal? NetIncome { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
