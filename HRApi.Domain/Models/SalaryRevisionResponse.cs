using HRApi.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.Domain.Models
{
    public class SalaryRevisionResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public SalaryRevisionDto Record { get; set; }
    }
}
