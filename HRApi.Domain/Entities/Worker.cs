using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HRApi.Domain.Entities
{
    public class Worker
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MaxLength(128)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(128)]
        public string Email { get; set; }
        
        public string Phone { get; set; }

        public decimal Salary { get; set; }

        public string Address { get; set; }
        
        [Required]
        public DateTime StartWorkingDate { get; set; }

        public List<Role> Roles { get; set; }

        public List<SalaryIncrease> SalaryIncreases { get; set; }

        public List<SalaryRevision> SalaryRevisions { get; set; }
    }
}
