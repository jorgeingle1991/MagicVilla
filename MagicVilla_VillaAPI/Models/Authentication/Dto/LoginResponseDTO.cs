namespace MagicVilla_VillaAPI.Models.Authentication.Dto
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
