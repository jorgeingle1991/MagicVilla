using MagicVilla_VillaAPI.Models.Authentication;
using MagicVilla_VillaAPI.Models.Authentication.Dto;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUserModel> Register(RegistrationRequestDTO registrationRequestDTO); 
    }
}
