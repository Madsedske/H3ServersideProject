using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using H3ServersideProject.Models;
using H3ServersideProject.Repositories.Interfaces;

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
                using (SqlCommand cmd = new SqlCommand("GetRerservedSeats", (SqlConnection)con))
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

        public void InsertReservation(int movieID, DateTime date, string email, int seatID)
        {
            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("Reserve", (SqlConnection)con))
                {
                    // A stored procedure that finds the column shown as a string with an @, the type
                    // and sets it to the input value of the user - Customers
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MovieID", SqlDbType.Int).Value = movieID;
                    cmd.Parameters.AddWithValue("@Date", SqlDbType.Date).Value = date;
                    cmd.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = email;
                    cmd.Parameters.AddWithValue("@SeatID", SqlDbType.Int).Value = seatID;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
