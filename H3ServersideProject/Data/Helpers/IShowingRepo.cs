using H3ServersideProject.Models;

namespace H3ServersideProject.Data.Helpers
{
    public interface IShowingRepo
    {
        List<int> GetReservation(int movieID, DateTime date);

        Showing InsertReservation();

    }
}
