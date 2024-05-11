using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

public class Authentication
{
    public static readonly string _constring = "Data Source=DESKTOP-33UQUOB\\SQLEXPRESS;Initial Catalog=ChefJoyDB;Integrated Security=True;";
   public static SqlConnection _connection = new SqlConnection(_constring);


    public static bool IsValidUser(string username, string password)
    {
        
        bool isValid = false;

        try
        {
            using (SqlConnection connection = new SqlConnection(_constring))
            {
                connection.Open();

                string query = @"SELECT * FROM CJ_users WHERE username = @username AND upass = @password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter dataAdapt = new SqlDataAdapter (command);
                    dataAdapt.Fill (dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        isValid = true;
                       
                        string insertQuery = @"INSERT INTO CJ_Session (UserID, sessionStart) VALUES (@UserID, @sessiondate)";
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        { 
                            insertCommand.Parameters.AddWithValue("@UserID", dataTable.Rows[0]["UserID"]);
                            insertCommand.Parameters.AddWithValue("@sessiondate", DateTime.Now);

                            insertCommand.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            // Log the error or provide a more informative error message
            Console.WriteLine($"Error: {ex.Message}");
        }

        return isValid;
    }
    
    
}