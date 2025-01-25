namespace APPFactusFacturacion.DTOS.factus_response
{
    public class FactusMunicipalitiesResponseDTO
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<string> errors { get; set; } = new List<string>();
        public List<Municipalities> data { get; set; }
    }

    public class Municipalities
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string department { get; set; }
    }
}
