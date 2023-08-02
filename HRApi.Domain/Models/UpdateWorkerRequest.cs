using HRApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.Domain.Models
{
    public class UpdateWorkerRequest
    {
        public string WorkerId { get; set; }

        public string Name { get; set; }
       
        public string LastName { get; set; }
        
        public string Email { get; set; }

        public string Phone { get; set; }

        public decimal BasicSalary { get; set; }

        public string Address { get; set; }
       
        public DateTime StartWorkingDate { get; set; }

        public List<int> RolesId { get; set; }
    }
}
