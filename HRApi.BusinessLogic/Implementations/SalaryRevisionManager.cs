using HRApi.BusinessLogic.Contracts;
using HRApi.DataAccess.Contracts;
using HRApi.Domain.Dto;
using HRApi.Domain.Entities;
using HRApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.BusinessLogic.Implementations
{
    public class SalaryRevisionManager : ISalaryRevisionManager
    {
        private readonly ISalaryRevisionRepository _salaryRevisionReporitory;
        private readonly IWorkerRepository _workerReporitory;

        private SalaryRevisionDto ModelToModelView(SalaryRevision c)
        {
            return new SalaryRevisionDto
            {
                Id = c.Id,
                Date = c.Date,
                WorkerDto = c.Worker != null ? WorkerToWorkerDto(c.Worker) : null
            };
        }

        private WorkerDto WorkerToWorkerDto(Worker c)
        {
            return new WorkerDto
            {
                Id = c.Id,
                Name = c.Name,
                LastName = c.LastName,
                FullName = c.Name + " " + c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                StartWorkingDate = c.StartWorkingDate,
                Salary = c.Salary
            };
        }

        public SalaryRevisionManager(ISalaryRevisionRepository salaryRevisionReporitory, IWorkerRepository workerReporitory)
        {
            _salaryRevisionReporitory = salaryRevisionReporitory;
            _workerReporitory = workerReporitory;
        }

        public GetLastRevisionWorkerIdResponse GetLastRevision(GetLastRevisionWorkerIdRequest request)
        {
            GetLastRevisionWorkerIdResponse response = null;
            var last_revision = _salaryRevisionReporitory.FindAll(p => p.Worker.Id == request.WorkerId).OrderByDescending(p => p.Date).FirstOrDefault();
            if (last_revision != null)
            {
                response = new GetLastRevisionWorkerIdResponse
                {
                    Status = 200,
                    Message = "Success",
                    Record = ModelToModelView(last_revision)
                };
            }
            else
            {
                response = new GetLastRevisionWorkerIdResponse
                {
                    Status = 400,
                    Message = "There is not revision assigned to the worker",
                    Record = null
                };
            }

            return response;
        }

        public AddSalaryRevisionResponse RegisterSalaryRevision(AddSalaryRevisionRequest request)
        {
            AddSalaryRevisionResponse response = null;
            var worker = _workerReporitory.Find(p => p.Id == request.WorkerId);
            if (worker != null)
            {
                var new_revision = new SalaryRevision
                {
                    Date = DateTime.Now,
                    Worker = worker
                };
                SalaryRevision result = _salaryRevisionReporitory.Add(new_revision);

                response = new AddSalaryRevisionResponse
                {
                    Status = 200,
                    Message = "Success",
                    Record = ModelToModelView(result)
                };

            }
            else
            {
                response = new AddSalaryRevisionResponse
                {
                    Status = 400,
                    Message = "There is not worker registered with the id",
                    Record = null
                };
            }

            return response;
        }
    }
}
