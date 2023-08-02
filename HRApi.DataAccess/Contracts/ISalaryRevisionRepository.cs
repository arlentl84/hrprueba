using HRApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.DataAccess.Contracts
{
    public interface ISalaryRevisionRepository
    {
        SalaryRevision Add(SalaryRevision salaryRevision);

        SalaryRevision Find(Expression<Func<SalaryRevision, bool>> filter);

        List<SalaryRevision> FindAll(Expression<Func<SalaryRevision, bool>> filter);

        IQueryable<SalaryRevision> GetAll();
        
    }
}
