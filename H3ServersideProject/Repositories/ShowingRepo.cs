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

        /// <summary>
        /// This gets the current reservations for the selected movie and date from the database.
        /// </summary>
        /// <param name="movieID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<int> GetReservation(int movieID, DateTime date)
        {
            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("GetRerservedSeats", (SqlConnection)con))
                {
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

        /// <summary>
        /// Inserts a reservation into the database.
        /// </summary>
        /// <param name="movieID"></param>
        /// <param name="date"></param>
        /// <param name="email"></param>
        /// <param name="seatID"></param>
        public void InsertReservation(int movieID, DateTime date, string email, int seatID)
        {
            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("Reserve", (SqlConnection)con))
                {
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
