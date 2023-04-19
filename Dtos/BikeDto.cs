namespace StationTask.Dtos
{
    public class BikeDto
    {
        public class Rootobject
        {
            public int last_updated { get; set; }
            public int ttl { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public Bike[] bikes { get; set; }
        }

        public class Bike
        {
            public string bike_id { get; set; }
            public string station_id { get; set; }
            public string name { get; set; }
            public float lon { get; set; }
            public float lat { get; set; }
            public int is_reserved { get; set; }
            public int is_disabled { get; set; }
        }
    }
}
