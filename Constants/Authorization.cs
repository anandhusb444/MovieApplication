namespace MovieApplication.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            User,
            Admin,
        }

        public const string defult_username = "user";
        public const string defult_email = "user@movie.com";
        public const string defult_password = "pa$$word";
        public const Roles defult_role = Roles.User;
    }
}
