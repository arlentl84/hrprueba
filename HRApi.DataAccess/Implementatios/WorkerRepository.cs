using HRApi.DataAccess.Contracts;
using HRApi.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using HRApi.DataAccess.Maps;

namespace HRApi.DataAccess.Implementatios
{
    public class WorkerRepository : IWorkerRepository
    {
        private ApplicationContext context;
        private DbSet<Worker> workersContext;
        private DbSet<WorkerSP> workerSPContext;
        private DbSet<SalaryIncreaseSP> salaryIncreaseContext;
        

        public WorkerRepository(ApplicationContext context)
        {
            this.context = context;
            workersContext = context.Set<Worker>();
            workerSPContext = context.Set<WorkerSP>();
            salaryIncreaseContext = context.Set<SalaryIncreaseSP>();
        }

        public Worker AddWorker(Worker worker)
        {
            workersContext.Add(worker);
            context.SaveChanges();            
            return worker;
        }

        public Worker Find(Expression<Func<Worker, bool>> filter)
        {
            return workersContext.Include(p => p.Roles).FirstOrDefault(filter);
        }

        public List<Worker> FindAll(Expression<Func<Worker, bool>> filter)
        {
            return workersContext.Include(p => p.SalaryIncreases).Include(p => p.SalaryRevisions).Where(filter).ToList();
        }

        public IQueryable<Worker> GetAll()
        {
            return context.Set<Worker>().Include(p => p.SalaryIncreases).Include(p => p.SalaryRevisions).AsNoTracking();
        }

        public IEnumerable<WorkerSP> GetAllWorkerRoleFromSP()
        {
           var result = workerSPContext.FromSqlInterpolated($"[dbo].[GetWorkerWithRoles]").ToArray();
           return result;
            /*return context.Database.Students?.FromSqlRaw("FindStudents @searchFor",
                new SqlParameter("@searchFor", searchFor)).ToList();*/
        }

        public IEnumerable<SalaryIncreaseSP> GetAllSalaryIncreasesByWorkerId(Guid workerId)
        {
            var result = salaryIncreaseContext.FromSqlInterpolated($"[dbo].[GetSalaryIncreaseByWorkerId] {workerId}").ToArray();
            return result;
            /*return context.Database.Students?.FromSqlRaw("FindStudents @searchFor",
                new SqlParameter("@searchFor", searchFor)).ToList();*/
        }

        public void Remove(Worker worker)
        {
            Worker toRemove = workersContext.FirstOrDefault(c => c.Id == worker.Id);
            if (toRemove != null)
            {
                workersContext.Remove(toRemove);
                context.SaveChanges();
            }
        }

        public void Update(Worker modifiedWorker)
        {
            Worker worker = workersContext.Find(modifiedWorker.Id);
            if (worker != null)
            {
                worker = modifiedWorker;
                context.SaveChanges();
            }
        }
    }
}
