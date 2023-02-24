using H3ServersideProject.Models;

namespace H3ServersideProject.Data.Helpers
{
    public interface IUserRepo
    {
        IEnumerable<User> GetUsers();

        User GetUser(string username);

        void Insert(User user);

        void Update(User user);

        void Delete(User user);

        void save();
    }
}
