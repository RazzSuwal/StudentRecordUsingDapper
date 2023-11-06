using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentRecordUsingDapper.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Dapper;

namespace StudentRecordUsingDapper.Services
{
    public class UserServices : IUserServices
    {
        private IConfiguration _configuration;

        public UserServices(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            providerName = "System.Data.SqlClient";
        }

        public string ConnectionString { get; }
        public string providerName { get; }
        public IDbConnection connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public User AuthenticateUser(string username, string password)
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Username", username);
            parameters.Add("@Password", password);

            var ur = dbConnection.QueryFirstOrDefault<User>("spAuthenticateUser", parameters, commandType: CommandType.StoredProcedure);
            return ur;
        }

        // public async Task<User> AuthenticateUserAsync(string userName, string password)
        // {
        //     using IDbConnection db = new SqlConnection(ConnectionString);
        //     var parameters = new { UserName = userName, Password = password };
        //     return await db.QueryFirstOrDefaultAsync<User>("UserAuthentication", parameters, commandType: CommandType.StoredProcedure);
        // }

        public string RegisterUser(User user)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    var ur = dbConnection.Query<User>("UserRegistration",
                    new { UserName = user.UserName, Email = user.Email, City = user.City, Password = user.Password },
                    commandType: CommandType.StoredProcedure);

                    if (ur != null && ur.FirstOrDefault().Response == "Register Successfully")
                    {
                        result = "Register Successfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return errorMessage;
            }
        }
    }
}