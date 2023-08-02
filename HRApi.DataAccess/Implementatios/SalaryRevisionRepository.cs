using HRApi.DataAccess.Contracts;
using HRApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.DataAccess.Implementatios
{
    public class SalaryRevisionRepository : ISalaryRevisionRepository
    {
        private ApplicationContext context;
        private DbSet<SalaryRevision> salaryRevisionContext;

        public SalaryRevisionRepository(ApplicationContext context)
        {
            this.context = context;
            salaryRevisionContext = context.Set<SalaryRevision>();
        }

        public SalaryRevision Add(SalaryRevision salaryRevision)
        {
            salaryRevisionContext.Add(salaryRevision);
            context.SaveChanges();
            return salaryRevision;
        }

        public SalaryRevision Find(Expression<Func<SalaryRevision, bool>> filter)
        {
            return salaryRevisionContext.Include(u => u.Worker).FirstOrDefault(filter);
        }    

        public List<SalaryRevision> FindAll(Expression<Func<SalaryRevision, bool>> filter)
        {
            return salaryRevisionContext.Where(filter).ToList();
        }

        public IQueryable<SalaryRevision> GetAll()
        {
            return context.Set<SalaryRevision>();
        }
    }
}
