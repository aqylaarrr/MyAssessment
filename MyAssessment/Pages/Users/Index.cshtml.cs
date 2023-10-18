using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyAssessment.Pages.Users
{
    public class IndexModel : PageModel
    {
        public List<UserInfo> listUsers = new List<UserInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-SPTV8UL9;Initial Catalog=userdata;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM users";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    { 
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read())
                            {
                                UserInfo userInfo = new UserInfo();
                                userInfo.id = "" + reader.GetInt32(0);
                                userInfo.name = reader.GetString(1);
                                userInfo.email = reader.GetString(2);
                                userInfo.phone = reader.GetString(3);
                                userInfo.skill = reader.GetString(4);
                                userInfo.hobby = reader.GetString(5);

                                listUsers.Add(userInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class UserInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String skill;
        public String hobby;
    }
}
