namespace D.Auth.Domain.Dtos.Login
{
    public class RefreshTokenResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpiredToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiredRefreshToken { get; set; }
    }
}
