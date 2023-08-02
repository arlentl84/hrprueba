using HRApi.DataAccess.Contracts;
using HRApi.Domain.Dto;
using HRApi.Domain.Entities;
using HRApi.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using HRApi.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRApi.DataAccess.Implementatios;
using Azure;
using HRApi.BusinessLogic.Contracts;

namespace HRApi.BusinessLogic.Implementations
{
    public class WorkerManager : IWorkerManager
    {
        private readonly IWorkerRepository _workerReporitory;
        private readonly ISalaryRevisionRepository _salaryRevisionReporitory;
        private readonly ISalaryIncreaseRepository _salaryIncreaseReporitory;
        private readonly IRoleRepository _roleReporitory;

        public WorkerManager(ISalaryRevisionRepository salaryRevisionReporitory, ISalaryIncreaseRepository salaryIncreaseReporitory, IWorkerRepository workerReporitory, IRoleRepository roleReporitory)
        {
            _workerReporitory = workerReporitory;
            _salaryRevisionReporitory = salaryRevisionReporitory;
            _salaryIncreaseReporitory = salaryIncreaseReporitory;
            _roleReporitory = roleReporitory;
        }

        private WorkerDto ModelToModelView(Worker c)
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

        private SalaryRevisionDto SalaryToSalaryRevisionDto(SalaryRevision c)
        {
            return new SalaryRevisionDto
            {
                Id = c.Id,
                Date = c.Date,
                WorkerDto = c.Worker != null ? ModelToModelView(c.Worker) : null
            };
        }

        private SalaryIncreaseDto SalaryIncreaseToSalaryIncreaseDto(SalaryIncrease c)
        {
            return new SalaryIncreaseDto
            {
                Id = c.Id,
                Date = c.Date,
                WorkerDto = c.Worker != null ? ModelToModelView(c.Worker) : null,
                Amount = c.Amount
            };
        }

        public AddWorkerResponse RegisterWorker(AddWorkerRequest request)
        {
            #region Declarations

            AddWorkerResponse response = null;

            #endregion

            #region Process
            try
            {
                var exist_worker_name = _workerReporitory.Find(p => p.Name == request.Name && p.LastName == request.LastName);

                if (exist_worker_name == null)
                {
                    var exist_worker_email = _workerReporitory.Find(p => p.Email == request.Email);
                    if (exist_worker_email == null)
                    {
                        if (Domain.Utilities.Validators.Instance.IsValidEmail(request.Email))
                        {
                            var new_worker = new Worker
                            {
                                Name = request.Name,
                                LastName = request.LastName,
                                Email = request.Email,
                                Phone = request.Phone,
                                StartWorkingDate = request.StartWorkingDate,
                                Address = request.Address,
                                Salary = request.BasicSalary
                            };

                            if (request.RolesId.Any())
                            {
                                List<Role> roles = new List<Role>();
                                request.RolesId.ForEach(idRol =>
                                {
                                    var role = _roleReporitory.Find(p => p.Id == idRol);
                                    //role.Workers.Add(new_worker);
                                    roles.Add(role);                                    
                                });
                                new_worker.Roles = roles;
                            }

                            var worker_result = _workerReporitory.AddWorker(new_worker);

                            var new_increase = _salaryIncreaseReporitory.Add(new SalaryIncrease
                            {
                                Date = DateTime.Now,
                                Worker = worker_result,
                                Amount = worker_result.Salary
                            });

                            response = new AddWorkerResponse
                            {
                                Status = 200,
                                Message = "Ejecutado correctamente",
                                Record = ModelToModelView(new_worker)
                            };
                        }
                        else
                        {
                            response = new AddWorkerResponse
                            {
                                Status = 400,
                                Message = "El formato del email suministrado no es correcto",
                                Record = null
                            };
                        }
                    }
                    else
                    {
                        response = new AddWorkerResponse
                        {
                            Status = 400,
                            Message = "The email is already registered ",
                            Record = null
                        };
                    }
                }
                else
                {
                    response = new AddWorkerResponse
                    {
                        Status = 400,
                        Message = "There is a worker registered with the same name",
                        Record = null
                    };
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                response = new AddWorkerResponse
                {
                    Status = 500,
                    Message =
                        "Se ha presentado un error indeterminado."
                };
            }


            #endregion

            return response;
        }

        private Tuple<Worker, decimal> IncreaseSalary(Worker c)
        {
            decimal increase = 0;
            if (c.Roles.Any(p => p.Name == "Worker"))
            {
                increase = c.Salary * 5 / 100;
                c.Salary += increase;
            }
            
            if (c.Roles.Any(p => p.Name == "Specialist"))
            {
                increase = (c.Salary * 8 / 100) + increase;
                c.Salary += increase;
            }
            
            if (c.Roles.Any(p => p.Name == "Manager"))
            {
                increase = (c.Salary * 12 / 100) + increase;
                c.Salary += increase;
            }
            return Tuple.Create(c, increase);
        }

        public GetSalaryIncreaseByWorkerIdResponse GetSalaryIncreaseByWorkerId(GetSalaryIncreaseByWorkerIdRequest request)
        {
            GetSalaryIncreaseByWorkerIdResponse response = null;
            try 
            {
                var all_worker = _workerReporitory.GetAllSalaryIncreasesByWorkerId(request.WorkerId).ToArray();
                var lista = all_worker.Select(o => new SalaryIncreaseSP { WorkerId = o.WorkerId, Amount = o.Amount, Date = o.Date, Id = o.Id }).ToList();

                response = new GetSalaryIncreaseByWorkerIdResponse
                {
                    Status = 200,
                    Message = "Success.",
                    Record = lista
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                response = new GetSalaryIncreaseByWorkerIdResponse
                {
                    Status = 500,
                    Message =
                        "Se ha presentado un error indeterminado."
                };
            }

            return response;
        }

        public SalaryRevisionResponse SalaryRevision(SalaryRevisionRequest request)
        {
            #region Declarations

            SalaryRevisionResponse response = null;

            #endregion

            try
            {
                #region Process
                var current_date = DateTime.Now;
                var last_revision = _salaryRevisionReporitory.FindAll(p => p.Worker.Id.ToString() == request.WorkerId).OrderByDescending(p => p.Date).FirstOrDefault();
                if (last_revision != null)
                {
                    /*var a1 = last_revision.Date.Date.AddMonths(3);
                    var a2 = current_date.Date;
                    var a3 = a1.CompareTo(a2);
                    var a4 = a2.CompareTo(a1);  */

                    if (last_revision.Date.Date.AddMonths(3).CompareTo(current_date.Date) < 0)
                    {
                        //crear revision
                        var worker = _workerReporitory.Find(p => p.Id.ToString() == request.WorkerId);

                        if (worker != null)
                        {
                            var new_revision = _salaryRevisionReporitory.Add(new SalaryRevision
                            {
                                Date = current_date,
                                Worker = worker
                            });

                            var result_increase = IncreaseSalary(worker);

                            worker = result_increase.Item1;

                            var new_increase = _salaryIncreaseReporitory.Add(new SalaryIncrease
                            {
                                Date = current_date,
                                Worker = worker,
                                Amount = result_increase.Item2
                            });

                            _workerReporitory.Update(worker);

                            response = new SalaryRevisionResponse
                            {
                                Status = 200,
                                Message = "Success",
                                Record = SalaryToSalaryRevisionDto(new_revision)
                            };
                        }
                        else
                        {
                            response = new SalaryRevisionResponse
                            {
                                Status = 400,
                                Message = "The Id value is not assigned to any worker",
                                Record = null
                            };
                        }
                    }
                    else
                    {
                        response = new SalaryRevisionResponse
                        {
                            Status = 200,
                            Message = "Success",
                            Record = SalaryToSalaryRevisionDto(last_revision)
                        };
                    }
                }
                else
                {
                    var worker = _workerReporitory.Find(p => p.Id.ToString() == request.WorkerId);

                    if (worker != null)
                    {

                        var new_revision = _salaryRevisionReporitory.Add(new SalaryRevision
                        {
                            Date = current_date,
                            Worker = worker
                        });

                        response = new SalaryRevisionResponse
                        {
                            Status = 200,
                            Message = "Success",
                            Record = SalaryToSalaryRevisionDto(new_revision)
                        };
                    }
                    else
                    {
                        response = new SalaryRevisionResponse
                        {
                            Status = 400,
                            Message = "The Id value is not assigned to any worker",
                            Record = null
                        };
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                response = new SalaryRevisionResponse
                {
                    Status = 500,
                    Message =
                        "Se ha presentado un error indeterminado."
                };
            }
            #endregion

            return response;

        }

        public UpdateWorkerResponse UpdateWorker(UpdateWorkerRequest request)
        {
            #region Declarations

            UpdateWorkerResponse response = null;

            #endregion

            try
            {
                var exist_worker = _workerReporitory.Find(p => p.Id.ToString() == request.WorkerId);
                if (exist_worker != null)
                {
                    if (request.RolesId != null)
                    {
                        if (request.RolesId.Any())
                        {
                            List<Role> roles = new List<Role>();

                            request.RolesId.ForEach(idRol =>
                            {
                                var role = _roleReporitory.Find(p => p.Id == idRol);
                                roles.Add(role);
                                role.Workers.Add(exist_worker);
                            });
                            exist_worker.Roles = roles;
                        }
                    }
                    if (!string.IsNullOrEmpty(request.Name))
                    {
                        exist_worker.Name = request.Name;
                    }
                    if (!string.IsNullOrEmpty(request.LastName))
                    {
                        exist_worker.LastName = request.LastName;
                    }
                    if (!string.IsNullOrEmpty(request.Email))
                    {
                        exist_worker.Email = request.Email;
                    }
                    if (!string.IsNullOrEmpty(request.Phone))
                    {
                        exist_worker.Phone = request.Phone;
                    }
                    if (!string.IsNullOrEmpty(request.Address))
                    {
                        exist_worker.Address = request.Address;
                    }
                    if (request.StartWorkingDate != default)
                    {
                        exist_worker.StartWorkingDate = request.StartWorkingDate;
                    }

                    _workerReporitory.Update(exist_worker);

                    response = new UpdateWorkerResponse
                    {
                        Status = 200,
                        Message = "Success",
                        Record = ModelToModelView(exist_worker)
                    };

                }
                else
                {
                    response = new UpdateWorkerResponse
                    {
                        Status = 400,
                        Message = "The Id value is not assigned to any worker",
                        Record = null
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                response = new UpdateWorkerResponse
                {
                    Status = 500,
                    Message =
                        "Se ha presentado un error indeterminado."
                };
            }

            return response;
        }

        public GetAllWorkersSPResponse GetAllWorkersRolesFromSP(GetAllWorkersSPRequest request)
        {
            GetAllWorkersSPResponse response = null;

            try
            {
                var all_worker = _workerReporitory.GetAllWorkerRoleFromSP().ToArray();
                //var array = new WorkerSP[all_worker.Length];
                
                var list = all_worker
                .GroupBy(o => o.WorkerId)
                  .Select(group => new
                {
                  workerId = group.Key,
                  roles = group.Select(o => new RoleSPDto{ Id = o.RolId, Name = o.RoleName }).ToList()
                    })
               .ToList();
                List<WorkerSPDto> listResult = new List<WorkerSPDto>();
                foreach (var item in list)
                {
                    var worker = all_worker.Where(o => o.WorkerId == item.workerId).Select(o => new WorkerSPDto { WorkerId = o.WorkerId, Name = o.Name, LastName = o.LastName, Email = o.Email, Phone = o.Phone, Address = o.Address, Salary = o.Salary, StartDate = o.StartDate }).First();
                    worker.Roles = item.roles;
                    listResult.Add(worker);
                }

                

                response = new GetAllWorkersSPResponse
                {
                    Status = 200,
                    Message = "Success.",
                    Record = listResult
                };

            }
            catch (Exception ex)                
            {
                response = new GetAllWorkersSPResponse
                {
                    Status = 500,
                    Message =
                        "Se ha presentado un error indeterminado."
                };
            }

            return response;
        }

        public GetAllWorkersResponse GetAllWorkers(GetAllWorkersRequest request)
        {
            #region Declarations

            GetAllWorkersResponse response = null;

            #endregion

            try
            {
                List<WorkerFullDto> listWorkers = new List<WorkerFullDto>();

                var all_worker = _workerReporitory.GetAll();

                foreach (var worker in all_worker)
                {
                    List<SalaryRevisionDto> revisions = new List<SalaryRevisionDto>();
                    List<SalaryIncreaseDto> increases = new List<SalaryIncreaseDto>();

                    foreach (var revision in worker.SalaryRevisions)
                    {
                        revisions.Add(SalaryToSalaryRevisionDto(revision));
                    }

                    foreach (var increase in worker.SalaryIncreases)
                    {
                        increases.Add(SalaryIncreaseToSalaryIncreaseDto(increase));
                    }

                    listWorkers.Add(new WorkerFullDto
                    {
                        WorkerDto = ModelToModelView(worker),
                        SalaryIncreases = increases,
                        SalaryRevisions = revisions
                    }
                    );
                }
                response = new GetAllWorkersResponse
                {
                    Status = 200,
                    Message = "Success.",
                    Record = listWorkers
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                response = new GetAllWorkersResponse
                {
                    Status = 500,
                    Message =
                        "Se ha presentado un error indeterminado."
                };
            }
            return response;
        }
    }
}
