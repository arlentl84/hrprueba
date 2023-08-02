using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRApi.Domain.Models;

namespace HRApi.BusinessLogic.Contracts
{
    public interface IWorkerManager
    {
        AddWorkerResponse RegisterWorker(AddWorkerRequest request);
        /*GetWorkertByIdResponse GetWorkerDetails(GetWorkerByIdRequest request);
        GetWorkersResponse GetAllWorker(GetWorkersRequest request);
        DeleteWorkerResponse DeleteWorker(DeleteWorkerRequest request);*/

        GetAllWorkersSPResponse GetAllWorkersRolesFromSP(GetAllWorkersSPRequest request);
        GetSalaryIncreaseByWorkerIdResponse GetSalaryIncreaseByWorkerId(GetSalaryIncreaseByWorkerIdRequest request);
        SalaryRevisionResponse SalaryRevision(SalaryRevisionRequest request);
        GetAllWorkersResponse GetAllWorkers(GetAllWorkersRequest request);
        UpdateWorkerResponse UpdateWorker(UpdateWorkerRequest request);
    }
}
