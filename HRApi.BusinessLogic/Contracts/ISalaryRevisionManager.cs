using HRApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.BusinessLogic.Contracts
{
    public interface ISalaryRevisionManager
    {
        AddSalaryRevisionResponse RegisterSalaryRevision(AddSalaryRevisionRequest request);
        GetLastRevisionWorkerIdResponse GetLastRevision(GetLastRevisionWorkerIdRequest request);
    }
}
