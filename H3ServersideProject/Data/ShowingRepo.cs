using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using H3ServersideProject.Data.Helpers;
using H3ServersideProject.Models;

namespace H3ServersideProject.Data
{
    public class ShowingRepo : IShowingRepo
    {
        private readonly DatabaseContext _context;

        public ShowingRepo(DatabaseContext context)
        {

            _context = context;
        }

        public List<int> GetReservation(int movieID, DateTime date)
        {
            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("GetReservedSeats", (SqlConnection)con))
                {
                    // A stored procedure that finds the column shown as a string with an @, the type
                    // and sets it to the input value of the user - Customers
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MovieID", SqlDbType.Int).Value = movieID;
                    cmd.Parameters.AddWithValue("@Date", SqlDbType.Date).Value = date;

                    SqlDataReader reader1 = cmd.ExecuteReader();
                    List<int> seats = new List<int>();
                    while (reader1.Read())
                    {
                        int id = (int)reader1.GetValue(0);
                        seats.Add(id);
                    }
                    con.Close();
                    return seats;
                }
            }
        }

        public Showing InsertReservation()
        {
            throw new NotImplementedException();
        }
    }
}
