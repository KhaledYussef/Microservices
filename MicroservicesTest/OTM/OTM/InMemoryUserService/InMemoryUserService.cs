namespace OTM.InMemoryUserService
{
    public interface IInMemoryUserService
    {
        List<User> GetUsers();
        bool IsValidUser(User user);
    }

    public class InMemoryUserService : IInMemoryUserService
    {
        private List<User> _users = new List<User>
        {
            new User { Username = "admin", Password = "admin" },
            new User { Username = "user", Password = "user" }
        };

        public bool IsValidUser(User user)
        {
            return _users.Any(u => u.Username == user.Username && u.Password == user.Password);
        }

        // get list of users
        public List<User> GetUsers()
        {
            return _users;
        }


    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
