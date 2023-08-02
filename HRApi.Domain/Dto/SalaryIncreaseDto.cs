using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.Domain.Dto
{
    public class SalaryIncreaseDto
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public WorkerDto WorkerDto { get; set; }
    }
}
