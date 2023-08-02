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
    public class RoleRepository : IRoleRepository
    {
        private ApplicationContext context;
        private DbSet<Role> rolesContext;

        public RoleRepository(ApplicationContext context)
        {
            this.context = context;
            rolesContext = context.Set<Role>();
        }

        public Role Add(Role role)
        {
            rolesContext.Add(role);
            context.SaveChanges();
            return role;
        }

        public Role Find(Expression<Func<Role, bool>> filter)
        {
            return rolesContext.FirstOrDefault(filter);
        }

        public List<Role> FindAll(Expression<Func<Role, bool>> filter)
        {
            return rolesContext.Where(filter).ToList();
        }

        public IQueryable<Role> GetAll()
        {
            return context.Set<Role>();
        }
    }
}
