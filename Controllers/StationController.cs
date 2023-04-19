using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StationTask.Common;
using StationTask.Dtos;
using StationTask.Services;
using static StationTask.Dtos.BikeDto;
using static StationTask.Dtos.StationDto;

namespace StationTask.Controllers
{
    [ApiController]
    [Route("stations")]
    public class StationController : ControllerBase
    {
        private readonly IOperationService _operationService;
        public StationController(IOperationService operationService)
        {
            _operationService = operationService;
        }
        /// <summary>
        /// This method creating new station with incoming arguments.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Http Status</returns>
     
        [HttpPost]
        public  Result<bool> AddStationAsync([FromBody] Bike args)
        {
            var serviceResult = _operationService.AddBikeAsync(args);
            if (serviceResult is not null) return new Result<bool>{ Data= true, IsSuccess=true, Message="Successfully created"};

            return new Result<bool> { Data = false, IsSuccess = false, Message = "Bad Request" };
        }
        /// <summary>
        /// This method returns filtered list.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<Result<List<Bike>>> SearchAsync([FromQuery] string query)
        {
            var serviceResult = await _operationService.SearchAsync(query) as List<Bike>;

            if(serviceResult is not null)
            {
                return new Result<List<Bike>> { Data = serviceResult, IsSuccess = true, Message = "Success" };
            }
            return new Result<List<Bike>> { Data = serviceResult, IsSuccess = false, Message = "Bad Request" };

        }
        /// <summary>
        /// Returns station list with bicyle count
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("report")]
        public Result<List<StationReportModel>> StationReportAsync()
        {
            var serviceResult =  _operationService.StationReport();

            if (serviceResult is not null)
            {
                return new Result<List<StationReportModel>> { Data = serviceResult, IsSuccess = true, Message = "Success" };
            }
            return new Result<List<StationReportModel>> { Data = serviceResult, IsSuccess = false, Message = "Bad Request" };

        }
    }
}
