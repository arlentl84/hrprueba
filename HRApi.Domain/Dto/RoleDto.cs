using HRApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.Domain.Dto
{
    public class RoleDto
    {
       public int Id { get; set; }

       public string Name { get; set; }

       public WorkerDto WorkerDto { get; set; }
    }
}
