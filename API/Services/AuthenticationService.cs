using System.Security.Cryptography;
using System.Text;
using API.DataTransferObjects;
using API.Enums;
using API.Models;
using API.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SharedContext _sharedContext;
        private readonly ITokenService _tokenService;

        const int KeySize = 64;
        const int Iterations = 350000;

        public AuthenticationService(SharedContext dbSharedContext, ITokenService tokenService)
        {
            _sharedContext = dbSharedContext;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Finds a user in the database and generates a token for them
        /// </summary>
        /// <param name="loginDto">The login data</param>
        /// <returns>An authentication pas for the users</returns>
        /// <exception cref="Exception"></exception>
        public async Task<AuthPas> Login(LoginDto loginDto)
        {
            var dbUser = await _sharedContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (dbUser == null)
            {
                throw new Exception("Incorrect email or password");
            }

            if (dbUser.SignedUp == false || dbUser.Salt == null || dbUser.Password == null)
            {
                throw new Exception("User has not finished sign up");
            }

            if(HashPassword(loginDto.Password, dbUser.Salt) != dbUser.Password)
            {
                throw new Exception("Incorrect email or password");
            }

            var token = _tokenService.GenerateToken(dbUser);

            dbUser.SetToken(token, DateTime.Now.AddHours(24));

            await _sharedContext.SaveChangesAsync();

            return dbUser.GetTokenAuthPas();
        }

        /// <summary>
        /// Gives a random password if the user has forgotten
        /// </summary>
        /// <param name="forgotPasswordDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var dbUser = await _sharedContext.Users.FirstOrDefaultAsync(u => u.Email == forgotPasswordDto.Email);

            if (dbUser == null)
            {
                throw new Exception("Incorrect email");
            }

            var salt = GenerateSalt();

            var randomPassword = GenerateRandomPassword();

            var password = HashPassword(randomPassword, salt);

            dbUser.ChangePassword(password, salt);
            dbUser.SetSignedUp(true);

            await _sharedContext.SaveChangesAsync();

            return randomPassword;
        }

        /// <summary>
        /// https://code-maze.com/csharp-hashing-salting-passwords-best-practices/
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string HashPassword(string password, byte[] salt)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                Iterations,
                HashAlgorithmName.SHA512,
                KeySize);
            return Convert.ToHexString(hash);
        }

        /// <summary>
        /// Logs out a user given their token
        /// </summary>
        /// <param name="authPas">The pas with the token to logout</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> LogOut(AuthPas authPas)
        {
            var dbUser = await _sharedContext.Users.FirstOrDefaultAsync(u => u.Token == authPas.Token);

            if (dbUser == null)
            {
                throw new Exception("Failed trying to logout incorrect token");
            }

            dbUser.SetToken("", null);

            await _sharedContext.SaveChangesAsync();

            return true;
        }



        /// <summary>
        /// Verifies a user by a given token
        /// </summary>
        /// <param name="token">The token to verify</param>
        /// <returns>Success boolean</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> VerifyToken(string token)
        {
            var dbUser = await _sharedContext.Users.FirstOrDefaultAsync(u => u.Token == token);

            if (dbUser == null)
            {
                throw new Exception("Failed trying to verify incorrect token");
            }

            if (dbUser.SignedUp == false)
            {
                throw new Exception("User has not finished sign up");
            }

            if (dbUser.TokenExpiration == null || dbUser.TokenExpiration <= DateTime.Now)
            {
                dbUser.SetToken("", null);
                await _sharedContext.SaveChangesAsync();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Verifies a user by a given token and role
        /// </summary>
        /// <param name="token"></param>
        /// <param name="expectedRole"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Role> VerifyRole(string token, Role expectedRole)
        {
            var dbUser = await _sharedContext.Users.FirstOrDefaultAsync(u => u.Token == token);

            if (dbUser == null)
            {
                throw new Exception("Failed trying to verify incorrect token");
            }

            if (dbUser.SignedUp == false)
            {
                throw new Exception("User has not finished signup");
            }

            if((int) dbUser.Role <= (int) expectedRole)
            {
                throw new Exception("Incorrect role");
            }

            return dbUser.Role;
        }

        /// <summary>
        /// Creates a new user with a signupDto
        /// </summary>
        /// <param name="signupDto">The sign up data</param>
        /// <returns>A success boolean</returns>
        public async Task<Customer> SignupNewCustomer(SignupDto signupDto)
        {
            if(signupDto.Password.Length <= 6 || signupDto.RepeatPassword.Length <= 6)
            {
                throw new Exception("Password must be above 7 characters");
            }

            if (signupDto.Password != signupDto.RepeatPassword )
            {
                throw new Exception("Passwords do not match");
            }

            var dbUser = await _sharedContext.Users.FirstOrDefaultAsync(u => u.Email == signupDto.Email);

            if (dbUser != null)
            {
                throw new Exception("Email already exists");
            }

            var customer = new Customer(signupDto);

            _sharedContext.Customers.Add(customer);
            await _sharedContext.SaveChangesAsync();

            return customer;
        }

        public static byte[] GenerateSalt()
        {
            var salt = RandomNumberGenerator.GetBytes(KeySize);
            return salt;
        }

        public static string GenerateRandomPassword()
        {
            var random = new Random();
            var password = new StringBuilder();

            for (int i = 0; i < 8; i++)
            {
                password.Append((char)random.Next(33, 126));
            }

            return password.ToString();
        }
    }
}
