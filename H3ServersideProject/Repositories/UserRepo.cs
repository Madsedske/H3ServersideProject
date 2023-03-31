using H3ServersideProject.Models;
using H3ServersideProject.Repositories.Helpers;
using H3ServersideProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;
using System.Text;
using System.Xml.Linq;

namespace H3ServersideProject.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly DatabaseContext _context;

        public UserRepo(DatabaseContext context)
        {

            _context = context;
        }

        /// <summary>
        /// This is used to delete a user from the database.
        /// </summary>
        /// <param name="email"></param>
        public void RemoveUser(string email)
        {
            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("RemoveUSer", (SqlConnection)con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = email;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        /// <summary>
        /// This gets the users password from the database corresponding to a supplied email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserPassword GetUser(string email)
        {
            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("Get_User", (SqlConnection)con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = email;

                    UserPassword userPassword = new UserPassword();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        userPassword.PasswordHash = (byte[])dr[0];
                        userPassword.PasswordSalt = (byte[])dr[1];                        
                    }
                    con.Close();
                    return userPassword;
                }
            }
        }

        /// <summary>
        /// Gets the users information from the database corresponding to a supplied email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetUserData(string email)
        {
            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("Get_User_Data", (SqlConnection)con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = email;

                    User user = new User();
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    if (reader1.Read())
                    {
                        user.Name = reader1.GetValue(0).ToString();
                        user.Address = reader1.GetValue(1).ToString();
                        user.Email = reader1.GetValue(2).ToString();
                        user.PhoneNumber = reader1.GetValue(3).ToString();
                    }
                    else
                    {

                    }
                    con.Close();
                    Console.WriteLine(user.Password);
                    return user;
                }
            }
        }

        /// <summary>
        /// This inserts a new user into the database.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userData"></param>
        public void Insert(User user, UserPassword userData)
        {
            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("Insert_User", (SqlConnection)con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = user.Email;
                    cmd.Parameters.AddWithValue("@passwordSalt", SqlDbType.VarBinary).Value = userData.PasswordSalt;
                    cmd.Parameters.AddWithValue("@passwordHash", SqlDbType.VarBinary).Value = userData.PasswordHash;
                    cmd.Parameters.AddWithValue("@address", SqlDbType.VarChar).Value = user.Address;
                    cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = user.Name;
                    cmd.Parameters.AddWithValue("@phonenumber", SqlDbType.VarChar).Value = user.PhoneNumber;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        /// <summary>
        /// This updates the user info in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userData"></param>
        public void Update(User user, UserPassword userData)
        {
            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("UpdateUser", (SqlConnection)con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = user.Email;
                    cmd.Parameters.AddWithValue("@passwordSalt", SqlDbType.VarBinary).Value = userData.PasswordSalt;
                    cmd.Parameters.AddWithValue("@passwordHash", SqlDbType.VarBinary).Value = userData.PasswordHash;
                    cmd.Parameters.AddWithValue("@address", SqlDbType.VarChar).Value = user.Address;
                    cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = user.Name;
                    cmd.Parameters.AddWithValue("@phonenumber", SqlDbType.VarChar).Value = user.PhoneNumber;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public List<GetUserReservation> GetUserReservation(string email)
        {
            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("GetUserReservations", (SqlConnection)con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = email;

                   
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<GetUserReservation> listUserReservations = new List<GetUserReservation>();
                    //if (dr.Read())
                    //{
                    //    getUserReservation.Seat = (int)dr[0];
                    //    getUserReservation.Movie = (int)dr[1];
                    //    getUserReservation.Date = (DateTime)dr[2];
                    //}
                    //int count = GetReserveCount(email);
                    //if (dr.Read())
                    //{
                    //    for (int i = 0; i < count; i += 3)
                    //    {
                    //        getUserReservation.Seat = (int)dr[i];
                    //        getUserReservation.Movie = (string)dr[i + 1];
                    //        getUserReservation.Date = (DateTime)dr[i + 2];
                    //        listUserReservations.Add(getUserReservation);
                    //    }
                    //}
                    while (dr.Read())
                    {
                        GetUserReservation getUserReservation = new GetUserReservation();
                        getUserReservation.Seat = (int)dr[0];
                        getUserReservation.Movie = dr[1].ToString();
                        getUserReservation.Date = (DateTime)dr[2];
                        listUserReservations.Add(getUserReservation);
                    }
                    con.Close();
                    return listUserReservations;
                }
            }
        }

        public int GetReserveCount(string email)
        {
            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("GetUserReservationCount", (SqlConnection)con))
                {
                    int count = 0;
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = email;

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        count = (int)dr[0];
                    }
                    con.Close();
                    return count;
                }
            }
        }
    }
}