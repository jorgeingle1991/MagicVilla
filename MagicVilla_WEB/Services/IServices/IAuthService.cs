using MagicVilla_WEB.Models.Authentication.Dto;

namespace MagicVilla_WEB.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LogingAsync<T>(LoginRequestDTO objToCreate);
        Task<T> RegisterAsync<T> (RegistrationRequestDTO objToCreate);
    }
}
