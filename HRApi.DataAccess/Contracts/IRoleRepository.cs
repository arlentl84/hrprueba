using HRApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.DataAccess.Contracts
{
    public interface IRoleRepository
    {
        Role Add(Role role);

        Role Find(Expression<Func<Role, bool>> filter);

        List<Role> FindAll(Expression<Func<Role, bool>> filter);

        IQueryable<Role> GetAll();
    }
}
