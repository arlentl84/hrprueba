using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRApi.Domain.Entities;

namespace HRApi.DataAccess.Maps
{
    public class SalaryIncreaseMap
    {
        public SalaryIncreaseMap(EntityTypeBuilder<SalaryIncrease> entityBuilder)
        {           
            entityBuilder.HasOne(d => d.Worker).WithMany(p => p.SalaryIncreases).HasForeignKey(d => d.WorkerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
