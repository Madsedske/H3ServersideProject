using H3ServersideProject.Data.Helpers;
using H3ServersideProject.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web.Helpers;

namespace H3ServersideProject.Data
{
    public class LoginRepo : ILogin
    {
        private readonly DatabaseContext _context;

        public LoginRepo(DatabaseContext context)
        {

            _context = context;
        }

        public Login GetuserLogin(Login login)
        {            
            using (IDbConnection con = _context.Connection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Get_UserLogin", (SqlConnection)con))
                {
                    // A stored procedure that finds the column shown as a string with an @, the type
                    // and sets it to the input value of the user - Customers
                   
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = login.Email;
                    cmd.Parameters.AddWithValue("@password", SqlDbType.Text).Value = login.Password;
                    

                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    if (dr.Read())
                    {
                        login.Password = dr.GetValue(0).ToString();
                        Console.WriteLine(login.Password);

                        return login;
                    }

                    con.Close();
                    return login;

                }
            }
        }
    }
}
