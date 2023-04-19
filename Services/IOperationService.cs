using StationTask.Common;
using StationTask.Dtos;
using static StationTask.Dtos.BikeDto;
using static StationTask.Dtos.StationDto;
using static StationTask.Services.OperationService;

namespace StationTask.Services
{
    public interface IOperationService
    {
        Task<bool> AddBikeAsync(Bike args);
        Task<List<Bike>> SearchAsync(string query);
        List<StationReportModel> StationReport();
    }
}
