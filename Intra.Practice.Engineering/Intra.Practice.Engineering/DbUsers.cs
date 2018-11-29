using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace Intra.Practice.Engineering
{
    public class DbUsers
    {
        public static bool SignUpFunc(String email, String password, String group)
        {
            String folder = @"c:\dbIntra/";
            FileStream file = null;

            Directory.CreateDirectory(folder);
            if (File.Exists(folder + email + ".json"))
                return (false);
            if (!File.Exists(folder + email + ".json"))
                file = File.Create(folder + email + ".json");

            if (File.Exists(folder + email + ".json"))
            {
                if (file != null)
                    file.Close();
                JObject json = new JObject();
                json.Add("email", email);
                json.Add("password", password);
                json.Add("group", group);
                try
                {
                    StreamWriter sr = new StreamWriter(folder + email + ".json");

                    sr.WriteLine(json.ToString());
                    sr.Close();
                    System.Diagnostics.Debug.WriteLine("SUCCEES : write data in file");
                    return (true);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("--------------------------------\n" + "Error : can't init StreamWriter : " + ex.ToString() + "\n----------------------------------------");
                    return (false);
                }
            }
            System.Diagnostics.Debug.WriteLine("Error : can't init StreamWriter");
            return (false);
        }
        public static String SignInFunc(String email, String password)
        {
            String folder = @"c:\dbIntra/";

            if (!File.Exists(folder + email + ".json"))
                return ("undefined");
            try
            {
                StreamReader sr = new StreamReader(folder + email + ".json");
                String data = sr.ReadToEnd();
                JObject obj = JObject.Parse(data);

                sr.Close();
                if (obj["password"].ToString() == password)
                    return (obj["group"].ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error : can't init StreamReader exeception : \n" + ex.ToString());
                return ("undefined");
            }
            return ("undefined");
        }
    }
}