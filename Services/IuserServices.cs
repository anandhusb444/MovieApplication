using Microsoft.EntityFrameworkCore;
using MovieApplication.DTOs;
using MovieApplication.Models;
using BCrypt.Net;


namespace MovieApplication.Services
{
    public interface IuserServices
    {
        //Task<IEnumerable<Users>> GetAlluser();
        //Task<Users> GetUserById(Guid id);
        Task<bool> UserRegister();
        //Task<bool> UserLogin();
        //Task<bool> Block_User();
        //Task<bool> Unblock_User();
        //Task<bool> Delete_User();
    }

    public class UserServies:IuserServices
    {
        private readonly MovieDbContext _dbContext;

        public async Task<bool> UserRegister(UserDTO usersDto)
        {
            try
            {
                var isUserExit = await _dbContext.Users.FirstOrDefaultAsync(u => u.email == usersDto.userEmail);

                if(isUserExit == null)
                {
                    var salt = BCrypt.Net.BCrypt.GenerateSalt();
                    var hashPassword = BCrypt.Net.BCrypt.HashPassword(usersDto.password, salt);
                    var addUser = new Users() { userName = usersDto.userName, email = usersDto.userEmail, passwordHash = usersDto.password };

                }
            }
            catch(Exception ex)
            {

                throw;
            }
        }


    }

}
