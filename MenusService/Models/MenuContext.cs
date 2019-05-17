using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MenusService.Models
{
    public class MenuContext
    {
        public string connetionString { get; set; }
        
        public MenuContext()
        {
            connetionString = "Data Source=PRACSERVER\\SQLEXPRESS;Initial Catalog=Resturants; Trusted_Connection=True;";
            Resturants = getResturants(connetionString);
            Meals = getMeals(connetionString);
            if (Meals.Count == 0) {
               generateMeals(2,16.44,"DoublQpounder","Double patie burger",DateTime.Now,DateTime.Now.AddDays(3),connetionString);
               generateMeals(1, 35.44, "streetWise", "fisrt choice", DateTime.Now, DateTime.Now.AddDays(6), connetionString);
               generateMeals(3, 78.44, "WinpFamMeal", "family meal", DateTime.Now, DateTime.Now.AddDays(1), connetionString);

            }
        }

        private void generateMeals(int resId,double price,string name,string desc,DateTime startDate, DateTime endDate,string connetionString)
        {
            string sql = null;
            var guid = Guid.NewGuid().ToString().Split('-').First();
            
            using (SqlConnection cnn = new SqlConnection(connetionString))
            {
                sql = "insert into dbo.Meals ([resId],[price],[name],[description],[startDate],[endDate])" +
                    " values(@resId,@price,@name,@description,@startDate,@endDate)";
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cmd.Parameters.AddWithValue("@resId", resId);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@description", desc);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Data Inserted");
                }
            }
        }
        public List<Meal> Meals { get; set; }
        public List<Resturant> Resturants { get; set; }
        private List<Meal> getMeals(string connetionString)
        {
            string queryString = "SELECT id,resId, name, price, description,startdate,endDate FROM dbo.Meals;";
            List<Meal> meals = new List<Meal>();
            using (var connection = new SqlConnection(connetionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Meal team = new Meal()
                        {
                            id = Convert.ToInt32(reader[0].ToString()),
                            resturantId = Convert.ToInt32(reader[1].ToString()),
                            name = reader[2].ToString(),
                            price = Convert.ToDouble(reader[3].ToString()),
                            description = reader[4].ToString(),
                            startDate   = Convert.ToDateTime(reader[5]),
                            endDate = Convert.ToDateTime(reader[6])
                        };
                        meals.Add(team);
                    }
                }
            }
            return meals ;
        }

        private List<Resturant> getResturants(string connectionString)
        {

            string queryString = "SELECT id, guid, resturant FROM dbo.Resturants;";
            List<Resturant> res = new List<Resturant>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Resturant team = new Resturant()
                        {
                            id = Convert.ToInt32(reader[0].ToString()),
                            guid = reader[1].ToString(),
                            name = reader[2].ToString()
                        };
                        res.Add(team);
                    }
                }
            }
            return res;
        }
    }
}