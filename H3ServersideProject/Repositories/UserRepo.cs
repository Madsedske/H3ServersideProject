using H3ServersideProject.Models;
using H3ServersideProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;
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

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string email)
        {
            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("Get_User", (SqlConnection)con))
                {
                    // A stored procedure that finds the column shown as a string with an @, the type
                    // and sets it to the input value of the user - Customers
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = email;

                    User user = new User();
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    if (dr.Read())
                    {
                        user.Password = dr.GetValue(0).ToString();
                    }
                    con.Close();
                    return user;

                }
            }
        }
        public User GetUserData(string email)
        {
            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("Get_User_Data", (SqlConnection)con))
                {
                    // A stored procedure that finds the column shown as a string with an @, the type
                    // and sets it to the input value of the user - Customers
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

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void Insert(User user)
        {
            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("Insert_User", (SqlConnection)con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = user.Email;
                    cmd.Parameters.AddWithValue("@passwordSalt", SqlDbType.VarChar).Value = user.PasswordSalt;
                    cmd.Parameters.AddWithValue("@passwordHash", SqlDbType.VarChar).Value = user.PasswordHash;
                    cmd.Parameters.AddWithValue("@address", SqlDbType.VarChar).Value = user.Address;
                    cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = user.Name;
                    cmd.Parameters.AddWithValue("@phonenumber", SqlDbType.VarChar).Value = user.PhoneNumber;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void save()
        {
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
