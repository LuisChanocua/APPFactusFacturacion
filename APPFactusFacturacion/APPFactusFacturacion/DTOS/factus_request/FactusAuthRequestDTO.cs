namespace APPFactusFacturacion.DTOS.factus_request
{
    public class FactusAuthRequestDTO
    {
        public string GrantType { get; set; } = "password";
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
