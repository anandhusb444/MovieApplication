using Microsoft.EntityFrameworkCore;
using MovieApplication.DTOs;
using MovieApplication.Models;
using BCrypt.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using MovieApplication.Respones;


namespace MovieApplication.Services
{
    public interface IuserServices
    {
        //Task<IEnumerable<Users>> GetAlluser();
        //Task<Users> GetUserById(Guid id);
        Task<GenericRespones<bool>> UserRegister(UserDTO userDto);
        Task<AuthanicationModel> UserLogin(UserDTO userDto);
        //Task<bool> Block_User();
        //Task<bool> Unblock_User();
        //Task<bool> Delete_User();
    }

    public class UserServies:IuserServices
    {
        private readonly MovieDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IjwtTokenServices _jwtTokenServices;

        public UserServies(MovieDbContext context,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, IConfiguration config,IjwtTokenServices jwtTokenServies)
        {
            _dbContext = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = config;
            _jwtTokenServices = jwtTokenServies;
        }

        public async Task<GenericRespones<bool>> UserRegister(UserDTO usersDto)
        {
            try
            {
                if (usersDto == null)
                    throw new ArgumentNullException(nameof(usersDto));

                var isUserExists = await _userManager.FindByEmailAsync(usersDto.userEmail);


                if (isUserExists != null)
                    return new GenericRespones<bool>(400,"user alreay exist",false,null);

                var salt = BCrypt.Net.BCrypt.GenerateSalt();
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(usersDto.password, salt);

                var addUser = new ApplicationUser
                {
                    UserName = usersDto.userName,
                    Email = usersDto.userEmail,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                };

                var result = await _userManager.CreateAsync(addUser, usersDto.password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(addUser, "User");
                    return new GenericRespones<bool>(200, "User Register Done", true, null);
                }
                else
                {
                    string errorDescription = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        errorDescription = error.Description;// <- this includes password policy errors
                    }

                    return new GenericRespones<bool>(400, "User Registeration Failed", false, errorDescription);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                if (ex.InnerException != null)
                    Console.WriteLine("Inner: " + ex.InnerException.Message);

                return new GenericRespones<bool>(500,"Internal server issue",false,"Failed");
            }
        }

        public async Task<AuthanicationModel> UserLogin(UserDTO userDto)
        {
            try
            {
                
                AuthanicationModel authModel = new AuthanicationModel();

                var userDetails = await _userManager.FindByEmailAsync(userDto.userEmail);

                if (userDetails == null)
                {
                    authModel.IsAuthenticated = false;
                    authModel.Message = "No user in this mail is registerd";
                    return authModel;
                }

                var result = await _signInManager.CheckPasswordSignInAsync(userDetails, userDto.password,false);

                if (!result.Succeeded)
                {
                    authModel.IsAuthenticated = false;
                    authModel.Message = "Invalid password";
                    return authModel;
                }

                var roles = await _userManager.GetRolesAsync(userDetails);
                var token = _jwtTokenServices.GenerateToken(userDetails, roles);

                authModel.IsAuthenticated = true;
                authModel.Token = token;
                authModel.Email = userDetails.Email;
                authModel.UserName = userDetails.UserName;
                authModel.Roles = roles.ToList();

                return authModel;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
                
            }
        }


    }

}
