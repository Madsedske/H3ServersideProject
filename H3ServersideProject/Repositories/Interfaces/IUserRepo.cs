using H3ServersideProject.Models;

namespace H3ServersideProject.Repositories.Interfaces
{
    public interface IUserRepo
    {
        UserPassword GetUser(string email);
        User GetUserData(string email);
        void Insert(User user, UserPassword userData);
        void Update(User user, UserPassword userData);
        void RemoveUser(string email);
    }
}
