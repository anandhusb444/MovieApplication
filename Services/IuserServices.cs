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
        Task<bool> UserRegister(UserDTO userDto);
        Task<bool> UserLogin(UserDTO userDto);
        //Task<bool> Block_User();
        //Task<bool> Unblock_User();
        //Task<bool> Delete_User();
    }

    public class UserServies:IuserServices
    {
        private readonly MovieDbContext _dbContext;

        public UserServies(MovieDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> UserRegister(UserDTO usersDto)
        {
            try
            {
                if (usersDto == null)
                    throw new ArgumentNullException(nameof(usersDto));

                var isUserExist = await _dbContext.Users.FirstOrDefaultAsync(u => u.email == usersDto.userEmail);

                if (isUserExist == null)
                {
                    var salt = BCrypt.Net.BCrypt.GenerateSalt();
                    var hashPassword = BCrypt.Net.BCrypt.HashPassword(usersDto.password, salt);

                    var addUser = new Users()
                    {
                        userName = usersDto.userName,
                        email = usersDto.userEmail,
                        passwordHash = hashPassword,
                        role = "user", // or from usersDto
                        createdAt = DateTimeOffset.UtcNow,
                        updateAt = DateTimeOffset.UtcNow
                    };

                    await _dbContext.Users.AddAsync(addUser);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                if (ex.InnerException != null)
                    Console.WriteLine("Inner: " + ex.InnerException.Message);

                return false;
            }
        }

        public async Task<bool> UserLogin(UserDTO userDto)
        {
            try
            {
                var userDetails = await _dbContext.Users.FirstOrDefaultAsync(user => user.email == userDto.userEmail);

                if (userDetails == null)
                    return false;

                bool userPassword = BCrypt.Net.BCrypt.Verify(userDto.password, userDetails.passwordHash);

                if (!userPassword)
                    return false;
                      
                //do the rest of the thing...
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


    }

}
