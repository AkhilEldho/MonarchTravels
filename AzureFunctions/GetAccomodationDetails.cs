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
    public static class GetAccomodationDetails
    {
        // TEST URL : http://localhost:7000/api/AccomodoationDetail
        [FunctionName("AccomodoationDetail")]
        public static string Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string message = "Error";
            string connectionString = "Server=citizen.manukautech.info,6305;Database=CC22_Team2_sem2;UID=CC21_Team2;PWD=fBit$45313;encrypt=true;trustservercertificate=true";

            try
            {
                //connecting azure to databse
                using (SqlConnection SQLConnection = new SqlConnection(connectionString))
                {
                    //checking if connection can be made to database
                    SQLConnection.Open();

                    //Searching for the product
                    var sqlQuery = "SELECT * FROM ACCOMODATION";

                    //sql command
                    var sqlCommand = new SqlCommand(sqlQuery, SQLConnection);

                    //closing connection if a connection already exists 
                    if (sqlCommand.Connection.State == System.Data.ConnectionState.Open)
                        sqlCommand.Connection.Close();

                    //establishing actual connection to database
                    sqlCommand.Connection.Open();
                    //running the sql query to read data
                    SqlDataReader SQLReader = sqlCommand.ExecuteReader();

                    //saving sql query results in data table
                    var productsData = new DataTable();
                    productsData.Load(SQLReader);

                    //taking object and converting it to a string
                    message = JsonConvert.SerializeObject(productsData);
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }
    }
}
