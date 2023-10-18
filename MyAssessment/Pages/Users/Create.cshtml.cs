using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyAssessment.Pages.Users
{
    public class CreateModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            userInfo.name = Request.Form["name"];
            userInfo.email = Request.Form["email"];
            userInfo.phone = Request.Form["phone"];
            userInfo.skill = Request.Form["skill"];
            userInfo.hobby = Request.Form["hobby"];

            if (userInfo.name.Length == 0 || userInfo.email.Length == 0 || userInfo.phone.Length == 0)
            {
                errorMessage = "The fields are required";
                return;
            }

            //save data to db
            try
            {
                String connectionString = "Data Source=LAPTOP-SPTV8UL9;Initial Catalog=userdata;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO users " +
                        "(name, email, phone, skill, hobby) VALUES " +
                        "(@name, @email, @phone, @skill, @hobby);";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userInfo.name);
                        command.Parameters.AddWithValue("@email", userInfo.email);
                        command.Parameters.AddWithValue("@phone", userInfo.phone);
                        command.Parameters.AddWithValue("@skill", userInfo.skill);
                        command.Parameters.AddWithValue("@hobby", userInfo.hobby);

                        command.ExecuteNonQuery();

                    }
                }
            }

            
            catch (Exception ex)
            {
                errorMessage = ex.Message; 
                return;
            }
            userInfo.name = ""; userInfo.email = ""; userInfo.phone = "";
            userInfo.skill = ""; userInfo.hobby = "";
            successMessage = "New User Successfully Added";

            Response.Redirect("/Users/Index");
        }
    }
}
