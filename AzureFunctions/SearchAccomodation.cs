using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace AzureFunctions
{
    public static class SearchAccomodation
    {
        // TEST URL : http://localhost:7000/api/SearchAccomodation
        [FunctionName("SearchAccomodation")]
        public static string Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string message = "Error";
            //Parameters
            string cost = req.Query["Cost"];
            string connectionString = "Server=citizen.manukautech.info,6305;Database=CC22_Team2_sem2;UID=CC21_Team2;PWD=fBit$45313;encrypt=true;trustservercertificate=true";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    //Search query and displays it alphabetically
                    var query = "SELECT * FROM Accomodation WHERE AccomodationCost >= '" + decimal.Parse(cost) + "' ORDER BY AccomodationName ASC";

                    var sqlCommmand = new SqlCommand(query, sqlConnection);

                    if (sqlCommmand.Connection.State == System.Data.ConnectionState.Open)
                    {
                        sqlCommmand.Connection.Close();
                    }

                    sqlCommmand.Connection.Open();
                    SqlDataReader sqlDataReader = sqlCommmand.ExecuteReader();
                    var accomodationResult = new DataTable();
                    accomodationResult.Load(sqlDataReader);
                    //Sending data in JSON form
                    message = JsonConvert.SerializeObject(accomodationResult);
                }
            }
            catch (Exception ex)
            {
                message += ex.Message;
            }
            return message;
        }
    }
}
