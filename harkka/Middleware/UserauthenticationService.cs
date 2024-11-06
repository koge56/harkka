using harkka.models;
using harkka.Reporsitories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace harkka.Middleware
{
    public interface IuserAuthenticationService
    {
        Task<User?> Authenticate(string username, string password);
        public User CreateUserCredentials(User user);
        Task<bool> isMyMessage(string username, long messageid);
    }
    public class UserauthenticationService : IuserAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        public UserauthenticationService(IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public async Task<User?> Authenticate(string username, string password)
        {
            User? user;

            user = await _userRepository.GetUserAsync(username);
            if (user == null)
            {
                return null;
            }
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: user.Salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 258 / 8));


            if (hashedPassword != user.password)
            {
                return null;
            }
            return user;
        }
        public User CreateUserCredentials(User user)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 258 / 8));

            user.password = hashedPassword;
            user.Salt = salt;
            user.joindate = user.joindate != null ? user.joindate : DateTime.Now;
            user.lastlogin = DateTime.Now;


            return user;
        }
        public async Task<bool> isMyMessage(string username, long messageid)
        {
            User? user = await _userRepository.GetUserAsync(username);
            if (user == null)
            {
                return false;
            }
            Message? message = await _messageRepository.GetMessageAsync(messageid);
            if (message == null)
            {
                return false;
            }
            if (message.sender == user)
            {
                return true;
            }
            return false;
        }
    }
}
