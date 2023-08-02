using HRApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.DataAccess.Contracts
{
    public interface IWorkerRepository
    {
        Worker AddWorker(Worker worker);

        Worker Find(Expression<Func<Worker, bool>> filter);

        List<Worker> FindAll(Expression<Func<Worker, bool>> filter);

        IEnumerable<WorkerSP> GetAllWorkerRoleFromSP();

        IEnumerable<SalaryIncreaseSP> GetAllSalaryIncreasesByWorkerId(Guid workerId);

        IQueryable<Worker> GetAll();

        void Update(Worker modifiedWorker);

        void Remove(Worker worker);
    }
}
