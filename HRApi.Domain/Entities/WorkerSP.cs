using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.Domain.Entities
{
    public class WorkerSP
    {
        public Guid WorkerId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public DateTime StartDate { get; set; }
        public int RolId { get; set; }
        public string RoleName { get; set; }
    }
}
