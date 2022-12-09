namespace WebAPI.Models
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
        public string AdditionalMessages { get; set; }
        public string UserType { get; set; }
    }
}
