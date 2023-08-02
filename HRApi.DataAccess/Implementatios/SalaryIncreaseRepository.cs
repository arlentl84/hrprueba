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
    public class SalaryIncreaseRepository : ISalaryIncreaseRepository
    {
        private ApplicationContext context;
        private DbSet<SalaryIncrease> salaryIncreaseContext;

        public SalaryIncreaseRepository(ApplicationContext context)
        {
            this.context = context;
            salaryIncreaseContext = context.Set<SalaryIncrease>();
        }

        public SalaryIncrease Add(SalaryIncrease salaryIncrease)
        {
            salaryIncreaseContext.Add(salaryIncrease);
            context.SaveChanges();
            return salaryIncrease;
        }

        SalaryIncrease ISalaryIncreaseRepository.Find(Expression<Func<SalaryIncrease, bool>> filter)
        {
            return salaryIncreaseContext.Include(u => u.Worker).FirstOrDefault(filter);
        }

        List<SalaryIncrease> ISalaryIncreaseRepository.FindAll(Expression<Func<SalaryIncrease, bool>> filter)
        {
            return salaryIncreaseContext.Where(filter).ToList();
        }

        IQueryable<SalaryIncrease> ISalaryIncreaseRepository.GetAll()
        {
            return context.Set<SalaryIncrease>();
        }
    }
}
