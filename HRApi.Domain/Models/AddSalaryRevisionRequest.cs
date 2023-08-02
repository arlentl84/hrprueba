using HRApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.Domain.Models
{
    public class AddSalaryRevisionRequest
    {
        public Guid WorkerId { get; set; }
    }
}
