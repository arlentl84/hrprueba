using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.Domain.Entities
{
    public class SalaryRevision
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Worker Worker { get; set; }

        public Guid WorkerId { get; set; }
    }
}
