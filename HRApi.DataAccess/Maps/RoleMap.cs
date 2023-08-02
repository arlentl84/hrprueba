using HRApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace HRApi.DataAccess.Maps
{
    public class RoleMap
    {
        public RoleMap(EntityTypeBuilder<Role> entityBuilder)
        {
            entityBuilder.HasMany<Worker>(s => s.Workers)
                .WithMany(c => c.Roles).UsingEntity("RoleWorker",
                l => l.HasOne(typeof(Role)).WithMany().HasForeignKey("RoleId").HasPrincipalKey(nameof(Role.Id)).OnDelete(DeleteBehavior.ClientNoAction),
            r => r.HasOne(typeof(Worker)).WithMany().HasForeignKey("WorkerId").HasPrincipalKey(nameof(Worker.Id)).OnDelete(DeleteBehavior.ClientNoAction),
            j => j.HasKey("RoleId", "WorkerId"));
               
            //entityBuilder.HasOne(d => d.Worker).WithMany(p => p.Roles).HasForeignKey(d => d.WorkerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
