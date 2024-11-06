using harkka.models;

namespace harkka.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetusersAsync();
        Task<UserDTO> GetuserAsync(string username);
        Task<UserDTO> NewuserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(string username);
    }
}
