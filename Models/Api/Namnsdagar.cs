namespace Fantastic4News.Models.Api
{
    public class Namnsdagar
    {
        public string cachetid { get; set; }
        public string version { get; set; }
        public string uri { get; set; }
        public string startdatum { get; set; }
        public string slutdatum { get; set; }
        public Dagar[] dagar { get; set; }

    }

        public class Dagar
    {
        public string datum { get; set; }
        public string veckodag { get; set; }
        public string arbetsfridag { get; set; }
        public string röddag { get; set; }
        public string vecka { get; set; }
        public string dagivecka { get; set; }
        public string[] namnsdag { get; set; }
        public string flaggdag { get; set; }
    }
    }
