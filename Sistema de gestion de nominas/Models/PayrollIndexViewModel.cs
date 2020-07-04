using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_gestion_de_nominas.Models
{
    public class PayrollIndexViewModel
    {
        public IEnumerable<Payroll> payrollList { get; set; }
        public bool IsReport { get; set; }
    }
}
