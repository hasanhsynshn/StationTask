using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;
using StationTask.Common;
using StationTask.Dtos;
using System.IO;
using static StationTask.Dtos.BikeDto;

namespace StationTask.Services
{
    public class OperationService : IOperationService
    {
        public async Task<bool> AddBikeAsync(Bike args)
        {
            try
            {
                if (!string.IsNullOrEmpty(args.name) && !string.IsNullOrEmpty(args.station_id))
                {
                    var appendJson = string.Empty;
                    using (StreamReader sr = new StreamReader($"{Directory.GetCurrentDirectory()}{Constants.Constants.PATH_BIKE}"))
                    {
                        string json = await sr.ReadToEndAsync();
                        var bikes = JsonConvert.DeserializeObject<Rootobject>(json).data.bikes;
                        var stations = JsonConvert.DeserializeObject<Rootobject>(json).data.bikes;
                        var checkData = stations.SingleOrDefault(x => x.station_id.Equals(args.station_id));
                        if (checkData is null) return false;

                        appendJson = JsonConvert.SerializeObject(bikes.Append(args));

                    }

                    if (!string.IsNullOrEmpty(appendJson)) File.WriteAllText($"{Directory.GetCurrentDirectory()}{Constants.Constants.PATH_BIKE}", appendJson);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }
        }

        public async Task<List<Bike>> SearchAsync(string query)
        {
            try
            {

                string json = string.Empty;
                using (StreamReader sr = new StreamReader($"{Directory.GetCurrentDirectory()}{Constants.Constants.PATH_BIKE}"))
                {
                    json = await sr.ReadToEndAsync();

                }
                if (!string.IsNullOrEmpty(query))
                {
                    var bikes = JsonConvert.DeserializeObject<List<Bike>>(json);
                    var result = bikes
                        .Where(x => x.name
                        .Contains(query))
                        .ToList();
                    return result;
                }
                return new List<Bike>();
            }
            catch
            {

                return null;
            }
        }

        public List<StationReportModel> StationReport()
        {
            try
            {
                var stationJson = File.ReadAllText($"{Directory.GetCurrentDirectory()}{Constants.Constants.PATH_STATION}");
                var stations = JsonConvert.DeserializeObject<StationDto.Rootobject>(stationJson).data.stations;

                var bikeJson = File.ReadAllText($"{Directory.GetCurrentDirectory()}{Constants.Constants.PATH_BIKE}");
                var bikes = JsonConvert.DeserializeObject<List<Bike>>(bikeJson);

                var bikeCountByStation = bikes
                    .GroupBy(x => x.station_id)
                    .ToDictionary(x => x.Key, x => x.Count());

                var joinData = stations
                    .Where(x => bikeCountByStation.ContainsKey(x.station_id))
                    .Select(x => new StationReportModel
                    {
                        StationName = x.name,
                        BikeCount = bikeCountByStation[x.station_id]
                    })
                    .ToList();

                return joinData;
            }
            catch 
            {

                return null;
            }
        }
       
    }
}
