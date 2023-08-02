using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.Domain.Entities
{
    public class SalaryIncreaseSP
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid WorkerId { get; set; }

        public decimal Amount { get; set; }
    }
}
