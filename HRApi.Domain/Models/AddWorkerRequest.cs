using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.Domain.Models
{
    public class AddWorkerRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        public decimal BasicSalary { get; set; }

        public string Address { get; set; }

        [Required]
        public List<int> RolesId { get; set; }

        [Required]
        public DateTime StartWorkingDate { get; set; }
    }
}
