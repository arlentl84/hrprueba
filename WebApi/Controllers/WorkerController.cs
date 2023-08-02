using Azure.Core;
using HRApi.BusinessLogic.Contracts;
using HRApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

namespace WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    [Produces("application/json")]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerManager _workersManager;
        private readonly ILogger<WorkerController> _logger;

        public WorkerController(IWorkerManager workersManager, ILogger<WorkerController> logger)
        {
            _workersManager = workersManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("workers")]
        public async Task<IActionResult> GetAllWorkers()
        {
            var response = await Task.Run(() =>
            {
                return this._workersManager.GetAllWorkers(new GetAllWorkersRequest());
            });

            if (response.Status == 500)
            {
                return Problem(detail: response.Message);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("workersp")]
        public async Task<IActionResult> GetAllWorkersFromSP()
        {
            var response = await Task.Run(() =>
            {
                return this._workersManager.GetAllWorkersRolesFromSP(new GetAllWorkersSPRequest());
            });

            if (response.Status == 500)
            {
                return Problem(detail: response.Message);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("workers")]
        public async Task<IActionResult> AddWorker([FromBody] AddWorkerRequest request)
        {
            if (string.IsNullOrEmpty(request?.Name))
            {
                return BadRequest("Name is required.");
            }

            if (string.IsNullOrEmpty(request?.LastName))
            {
                return BadRequest("Last Name is required.");
            }

            if (string.IsNullOrEmpty(request?.Email))
            {
                return BadRequest("Email is required.");
            }

            DateTime dateFromTime;
            if (!DateTime.TryParseExact(request.StartWorkingDate.ToString("dd/mm/yyyy"), "dd/mm/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFromTime))
            {
                return BadRequest("invalid date/time format.");
            }

            var response = await Task.Run(() =>
            {
                return _workersManager.RegisterWorker(request);
            });

            if (response.Status == 500)
            {
                return Problem(detail: response.Message);
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("workers")]
        public async Task<IActionResult> UpdateWorker([FromBody] UpdateWorkerRequest request)
        {            
            DateTime dateFromTime;
            if (!DateTime.TryParseExact(request.StartWorkingDate.ToString("dd/mm/yyyy"), "dd/mm/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFromTime))
            {
                return BadRequest("Wrong date/time format.");
            }

            var response = await Task.Run(() =>
            {
                return this._workersManager.UpdateWorker(request);
            });

            if (response.Status == 500)
            {
                return Problem(detail: response.Message);
            }

            return Ok(response);
        }

        [HttpPost]        
        [Route("workers/revision")]
        public async Task<IActionResult> WorkerRevision([FromBody] SalaryRevisionRequest request)
        {
            if (string.IsNullOrEmpty(request.WorkerId))
            {
                return BadRequest("Worker Id is required.");
            }

            var response = await Task.Run(() =>
            {
                return this._workersManager.SalaryRevision(new SalaryRevisionRequest { WorkerId = request.WorkerId });
            });

            if (response.Status == 500)
            {
                return Problem(detail: response.Message);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("workers/increase/{id}")]
        public async Task<IActionResult> GetIncreasesByWorkerId(Guid id)
        {
            var response = await Task.Run(() =>
            {
                return this._workersManager.GetSalaryIncreaseByWorkerId(new GetSalaryIncreaseByWorkerIdRequest { WorkerId = id});
            });

            if (response.Status == 500)
            {
                return Problem(detail: response.Message);
            }

            return Ok(response);
        }
    }
}
