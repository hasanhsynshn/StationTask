namespace StationTask.Dtos;

public class StationDto
{
    public class Rootobject
    {
        public int last_updated { get; set; }
        public int ttl { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public Station[] stations { get; set; }
    }

    public class Station
    {
        public string station_id { get; set; }
        public string name { get; set; }
        public string region_id { get; set; }
        public float lon { get; set; }
        public float lat { get; set; }
        public string address { get; set; }
        public string[] rental_methods { get; set; }
    }


}
