using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;

namespace MyAssessment.Pages.Users
{
    public class EditModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=LAPTOP-SPTV8UL9;Initial Catalog=userdata;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM users WHERE id=@id ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userInfo.id = "" + reader.GetInt32(0);
                                userInfo.name = reader.GetString(1);
                                userInfo.email = reader.GetString(2);
                                userInfo.phone = reader.GetString(3);
                                userInfo.skill = reader.GetString(4);
                                userInfo.hobby = reader.GetString(5);
                            }
                        }
           
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost() 
        {
            userInfo.id = Request.Form["id"];
            userInfo.name = Request.Form["name"];
            userInfo.email = Request.Form["email"];
            userInfo.phone = Request.Form["phone"];
            userInfo.skill = Request.Form["skill"];
            userInfo.hobby = Request.Form["hobby"];

            if (userInfo.id.Length == 0 || userInfo.name.Length == 0 || userInfo.email.Length == 0 || userInfo.phone.Length == 0)
            {
                errorMessage = "The fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=LAPTOP-SPTV8UL9;Initial Catalog=userdata;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE users " +
                        "SET name=@name, email=@email, phone=@phone, skill=@skill, hobby=@hobby " +
                        "WHERE id=@id";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userInfo.name);
                        command.Parameters.AddWithValue("@email", userInfo.email);
                        command.Parameters.AddWithValue("@phone", userInfo.phone);
                        command.Parameters.AddWithValue("@skill", userInfo.skill);
                        command.Parameters.AddWithValue("@hobby", userInfo.hobby);
                        command.Parameters.AddWithValue("@id", userInfo.id);

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Users/Index");

        }
    }
}
