using HRApi.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.Domain.Models
{
    public class WorkerFullDto
    {
        public WorkerDto WorkerDto { get; set; }
        public List<SalaryRevisionDto> SalaryRevisions { get; set;}
        public List<SalaryIncreaseDto> SalaryIncreases { get; set; }
    }
}
