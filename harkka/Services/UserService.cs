using harkka.models;
using harkka.Reporsitories;
using System.Runtime.CompilerServices;

namespace harkka.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> DeleteUserAsync(string username)
        {
            User? user = await _repository.GetUserAsync(username);
            if (user != null) 
            {
                return await _repository.DeleteUserAsync(user);

            }
            return false;
        }

        public async Task<UserDTO> GetuserAsync(string username)
        {
            User? user = await _repository.GetUserAsync(username);
            if (user == null)
            {
                return null;
            }
            return UserToDto(user);

        }

        public async Task<IEnumerable<UserDTO>> GetusersAsync()
        {
            IEnumerable<User> users = await _repository.GetUsersAsync();
            List<UserDTO> result = new List<UserDTO>();
            foreach (User user in users)
            {
                result.Add(UserToDto(user));
            }
            return result;
        }

        public async Task<UserDTO> NewuserAsync(User user)
        {
            User? dbUser = await _repository.GetUserAsync(user.username);
            if (dbUser != null) 
            {
                return null;
            }
            User? newUser = await _repository.NewUserAsync(user);
            if (newUser != null) 
            {
                return UserToDto(newUser);
            }
            return null;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            User? dbUser = await _repository.GetUserAsync(user.username);
            if (dbUser != null)
            {
                dbUser.firstName = user.firstName;
                dbUser.lastName = user.lastName;
                dbUser.email = user.email;
                dbUser.password = user.password;
                return await _repository.UpdateUserAsync(dbUser);
            }
            return false;

        }
        private UserDTO UserToDto(User user)
        {
            UserDTO dto = new UserDTO();
            dto.username = user.username;
            dto.firstName = user.firstName;
            dto.lastName = user.lastName;
            dto.email = user.email;
            dto.joindate = user.joindate;
            dto.lastlogin = user.lastlogin;

            return dto;
        }
    }
}