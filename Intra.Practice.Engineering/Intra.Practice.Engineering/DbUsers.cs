﻿using System;
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

        public static Boolean PostDayOff(String dayStart, String dayEnd, String Reason, String UserEmail)
        {
            String folder = @"c:\dbIntra/";
            String dbName = "ListDayOff";

            if (!File.Exists(folder + UserEmail + dbName + ".json"))
            {
                FileStream file = File.Create(folder + UserEmail + dbName + ".json");

                file.Close();
                try
                {
                    StreamWriter srW = new StreamWriter(folder + UserEmail + dbName + ".json");

                    JObject obj = new JObject();
                    JArray array = new JArray();

                    obj.Add("list", array);
                    srW.WriteLine(obj.ToString());
                    srW.Close();
                    System.Diagnostics.Debug.WriteLine("SUCCEES : write data in file");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("--------------------------------\n" + "Error : can't init StreamWriter : " + ex.ToString() + "\n----------------------------------------");
                    return (false);
                }
            }
            if (File.Exists(folder + UserEmail + dbName + ".json"))
            {
                JObject objFinal = new JObject();
                try
                {
                    StreamReader sr = new StreamReader(folder + UserEmail + dbName + ".json");
                    objFinal = JObject.Parse(sr.ReadToEnd());
                    JArray array = JArray.Parse(objFinal["list"].ToString());
                    JObject newObjList = new JObject();
                  
                    newObjList.Add("Start", dayStart);
                    newObjList.Add("End", dayEnd);
                    newObjList.Add("Reason", Reason);
                    newObjList.Add("State", "In progress");
                    newObjList.Add("Id", (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString().Split(",")[0]);
                    newObjList.Add("User", UserEmail);
                    array.Add(newObjList);
                    objFinal["list"] = array;
                    sr.Close();
                    try
                    {
                        StreamWriter srWS = new StreamWriter(folder + UserEmail + dbName + ".json");

                        srWS.Write(objFinal.ToString());
                        srWS.Close();
                        System.Diagnostics.Debug.WriteLine("SUCCEES : write data in file");
                        System.Diagnostics.Debug.WriteLine(objFinal.ToString());
                        return (true);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("--------------------------------\n" + "Error : can't init StreamWriter : " + ex.ToString() + "\n----------------------------------------");
                        return (false);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error : can't init StreamReader exeception : \n" + ex.ToString());
                    return (false);
                }
            }
            return (false);
        }

        public static JArray getRequirementsListFromUser(String userEmail)
        {
            String folder = @"c:\dbIntra/";
            String dbName = "ListDayOff";

            if (File.Exists(folder + userEmail + dbName + ".json"))
            {
                try
                {
                    StreamReader sr = new StreamReader(folder + userEmail + dbName + ".json");
                    JArray array = JArray.Parse(JObject.Parse(sr.ReadToEnd())["list"].ToString());

                    sr.Close();
                    return (array);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error : can't init StreamReader exeception : \n" + ex.ToString());
                    return (new JArray());
                }
            }
            return (new JArray());
        }

        public static Boolean removeOneItemFromList(String userEmail, String Id)
        {
            String folder = @"c:\dbIntra/";
            String dbName = "ListDayOff";
            JObject objFinal = new JObject();
            
            try
            {
                StreamReader sr = new StreamReader(folder + userEmail + dbName + ".json");
                JArray array = JArray.Parse(JObject.Parse(sr.ReadToEnd())["list"].ToString());
                int idx = 0;

                sr.Close();
                while (idx != array.Count)
                {
                    if (array[idx]["Id"].ToString() == Id)
                        break;
                    idx++;
                }
                if (idx != array.Count)
                    array.RemoveAt(idx);
                else
                    return (false);
                objFinal.Add("list", array);
                try
                {
                    StreamWriter srW = new StreamWriter(folder + userEmail + dbName + ".json");

                    srW.Write(objFinal.ToString());
                    srW.Close();
                    System.Diagnostics.Debug.WriteLine("SUCCEES : write data in file");
                    System.Diagnostics.Debug.WriteLine(objFinal.ToString());
                    return (true);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("--------------------------------\n" + "Error : can't init StreamWriter : " + ex.ToString() + "\n----------------------------------------");
                    return (false);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error : can't init StreamReader exeception : \n" + ex.ToString());
                return (false);
            }
        }

        public static JArray getAllRequirementsList(String separator)
        {
            String folder = @"c:\dbIntra/";
            JArray globalArray = new JArray();

            var allFilesDb = Directory.EnumerateFiles(folder);

            System.Diagnostics.Debug.WriteLine("START FILE FINDER");
            foreach (String nameFile in allFilesDb)
            {
                System.Diagnostics.Debug.WriteLine("NAME FILE = " + nameFile);
            }
            return (globalArray);
        }
    }
}