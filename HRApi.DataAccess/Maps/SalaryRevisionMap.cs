using HRApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.DataAccess.Maps
{
    public class SalaryRevisionMap
    {
        public SalaryRevisionMap(EntityTypeBuilder<SalaryRevision> entityBuilder)
        {
            entityBuilder.HasOne(d => d.Worker).WithMany(p => p.SalaryRevisions).HasForeignKey(d => d.WorkerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
