using System;
using System.Configuration;
using System.Data;
using Dapper;
using Npgsql;

namespace DBTest
{
    public class PostgresDataAccess
    {
        public static List<UserModel> LoadUsers()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {

                var output = cnn.Query<UserModel>("select * from \"Users\"", new DynamicParameters());
                Console.WriteLine(output);
                return output.ToList();
            }
            // Kopplar upp mot DB:n
            // läser ut alla Users
            // Returnerar en lista av Users
        }

        public static void SavePerson(UserModel user)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into \"Users\" (\"FirstName\", \"LastName\", \"PinCode\") values (@FirstName, @LastName, @PinCode)", user);

            }
        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

    }
}



