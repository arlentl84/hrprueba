using HRApi.Domain.Dto;
using HRApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.Domain.Models
{
    public class GetSalaryIncreaseByWorkerIdResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public List<SalaryIncreaseSP> Record { get; set; }
    }
}
