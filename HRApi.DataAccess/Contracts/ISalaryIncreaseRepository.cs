using HRApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.DataAccess.Contracts
{
    public interface ISalaryIncreaseRepository
    {
        SalaryIncrease Add(SalaryIncrease salaryRevision);

        SalaryIncrease Find(Expression<Func<SalaryIncrease, bool>> filter);

        List<SalaryIncrease> FindAll(Expression<Func<SalaryIncrease, bool>> filter);

        IQueryable<SalaryIncrease> GetAll();
    }
}
