namespace MagicVilla_VillaAPI.Models.Authentication.Dto
{
    public class LoginResponseDTO
    {
        public LocalUserModel User { get; set; }
        public string Token { get; set; }
    }
}
