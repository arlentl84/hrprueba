using HRApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.Domain.Dto
{
    public class WorkerDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public decimal Salary { get; set; }

        public string Address { get; set; }

        public DateTime StartWorkingDate { get; set; }

        public decimal FullSalary()
        {
            return 1;
        }
        
    }
}
