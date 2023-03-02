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

        public UserPassword GetUser(string email)
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

        public void save()
        {
            _context.SaveChanges();
        }

        public void Update(User user, UserPassword userData)
        {
            PasswordService pswService = new PasswordService();
            User tempUser = GetUserData(user.Email);
            UserPassword tempUserPassword = GetUser(user.Email);

            if (user.Address is not null)
                tempUser.Address = user.Address;
            if (user.Name is not null)
                tempUser.Name = user.Name;
            if (user.PhoneNumber is not null)
                tempUser.PhoneNumber = user.PhoneNumber;
            if (user.Password is not null)
            {
                pswService.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                tempUserPassword.PasswordHash = passwordHash;
                tempUserPassword.PasswordSalt = passwordSalt;
            }

            using (IDbConnection con = _context.Connection())
            {
                using (SqlCommand cmd = new SqlCommand("UpdateUser", (SqlConnection)con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = tempUser.Email;
                    cmd.Parameters.AddWithValue("@passwordSalt", SqlDbType.VarBinary).Value = tempUserPassword.PasswordSalt;
                    cmd.Parameters.AddWithValue("@passwordHash", SqlDbType.VarBinary).Value = tempUserPassword.PasswordHash;
                    cmd.Parameters.AddWithValue("@address", SqlDbType.VarChar).Value = tempUser.Address;
                    cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = tempUser.Name;
                    cmd.Parameters.AddWithValue("@phonenumber", SqlDbType.VarChar).Value = tempUser.PhoneNumber;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
