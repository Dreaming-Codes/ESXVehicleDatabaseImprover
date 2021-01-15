using System;
using System.Data;
using CitizenFX.Core;
using MySql.Data.MySqlClient;
using static CitizenFX.Core.Native.API;

namespace UpdateVehicleTable
{
    public class Main : BaseScript
    {
        public Main()
        {
            string mysqlConnectionString = AdaptString(GetConvar("mysql_connection_string",
                "server=localhost;database=gtaserver;userid=root;password="));
            MySqlConnection con = new MySqlConnection(mysqlConnectionString);
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand
            {
                Connection = con,
                CommandText =
                    "SELECT model FROM vehicles"
            };
            try
            {
                Console.WriteLine("TRYING TO CONNECT TO YOUR MYSQL DATABASE");
                con.Open();
                Console.WriteLine("CONNECTION SUCCEEDED");
                MySqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                Console.WriteLine("EXTRACTION OF VEHICLE MODELS FROM THE DATABASE COMPLETED");
                string cmd2 = "";
                for (int y = 0; y < dt.Rows.Count; y++)
                {
                    string model = dt.Rows[y][0].ToString();
                    Console.Write($"GENERATING HASH FOR THE VEHICLE WITH THE FOLLOWING 'model': {model}");
                    int modelHashKey = GetHashKey(model);
                    Console.WriteLine($" -> {modelHashKey}");
                    cmd2 += $"UPDATE vehicles SET hash = {modelHashKey} WHERE model = '{model}';";
                }

                cmd.CommandText = cmd2;
                Console.WriteLine("QUERY GENERATION COMPLETED");
                Console.WriteLine("EXECUTING...");
                cmd.ExecuteNonQuery();
                Console.WriteLine("DONE");
                Console.WriteLine("   _____                           _              _____          _           \n |  __ \\                         (_)            / ____|        | |          \n | |  | |_ __ ___  __ _ _ __ ___  _ _ __   __ _| |     ___   __| | ___  ___ \n | |  | | '__/ _ \\/ _` | '_ ` _ \\| | '_ \\ / _` | |    / _ \\ / _` |/ _ \\/ __|\n | |__| | | |  __/ (_| | | | | | | | | | | (_| | |___| (_) | (_| |  __/\\__ \\\n |_____/|_|  \\___|\\__,_|_| |_| |_|_|_| |_|\\__, |\\_____\\___/ \\__,_|\\___||___/\n                                           __/ |                            \n                                          |___/                             ");
                Console.WriteLine("Follow me on github <3");


            } catch (Exception e)
            {
                Console.WriteLine("   _____                           _              _____          _           \n |  __ \\                         (_)            / ____|        | |          \n | |  | |_ __ ___  __ _ _ __ ___  _ _ __   __ _| |     ___   __| | ___  ___ \n | |  | | '__/ _ \\/ _` | '_ ` _ \\| | '_ \\ / _` | |    / _ \\ / _` |/ _ \\/ __|\n | |__| | | |  __/ (_| | | | | | | | | | | (_| | |___| (_) | (_| |  __/\\__ \\\n |_____/|_|  \\___|\\__,_|_| |_| |_|_|_| |_|\\__, |\\_____\\___/ \\__,_|\\___||___/\n                                           __/ |                            \n                                          |___/                             ");
                Console.WriteLine("AN ERROR OCCURRED PASTE THE TEXT BELOW ON A NEW ISSUE ON GITHUB");
                Console.WriteLine(e);
            }
        }
        private string AdaptString(string x)
        {
            x = x.Replace("server=", "Server=");
            x = x.Replace("database=", "Database=");
            x = x.Replace("userid=", "Uid=");
            x = x.Replace("password=", "Pwd=");
            return x.Replace(";", "; ");
        }
    }
}
