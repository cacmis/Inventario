
using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthContext _context;

        public AuthRepository(AuthContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash(password,out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<User> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            // si el usuario no existe regresar null
            if (user is null)
                return null;
            // si el usuario existe verificar que el password sea el correcto
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;

        }
        
        public async Task<bool> UserExists(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
                return true;

            return false;
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

    }
}