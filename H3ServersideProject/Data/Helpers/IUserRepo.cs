using H3ServersideProject.Models;

namespace H3ServersideProject.Data.Helpers
{
    public interface IUserRepo
    {
        IEnumerable<User> GetUsers();

        User GetUser(string email);

        User GetUserData(string email);

        void Insert(User user);

        void Update(User user);

        void Delete(User user);

        void save();
    }
}
