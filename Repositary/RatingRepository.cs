using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Repositary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamar_Sheva_Project;

namespace Repository
{
    public class RatingRepository: IRatingRepository
    {
            AdoNetOurStore326035854Context furniture = new AdoNetOurStore326035854Context();
        IConfiguration configuration;
            public RatingRepository(AdoNetOurStore326035854Context furniture,IConfiguration conf)
            {
            this.configuration = conf;
             this.furniture = furniture;

            }
        public int AddRating(Rating rating)
        {
            string con = configuration.GetConnectionString("School");
            string query = "INSERT INTO RATING(HOST,METHOD,PATH,REFERER,USER_AGENT,Record_Date)" +
                "VALUES(@HOST,@METHOD,@PATH,@REFERER,@USER_AGENT,@Record_Date)";

           
            using (SqlConnection cn = new SqlConnection(con))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@HOST", rating.Host);
                    cmd.Parameters.AddWithValue("@METHOD", rating.Method);
                    cmd.Parameters.AddWithValue("@PATH", rating.Path);
                    cmd.Parameters.AddWithValue("@REFERER", rating.Referer);
                    cmd.Parameters.AddWithValue("@USER_AGENT", rating.UserAgent);
                    cmd.Parameters.AddWithValue("@Record_Date", DateTime.Now);


                    cn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            return 1;
        }
                  

    }
}
