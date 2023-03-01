using H3ServersideProject.Models;

namespace H3ServersideProject.Repositories.Interfaces
{
    public interface IShowingRepo
    {
        List<int> GetReservation(int movieID, DateTime date);

        Showing InsertReservation();

    }
}
