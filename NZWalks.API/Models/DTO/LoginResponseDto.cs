namespace NZWalks.API.Models.DTO
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
