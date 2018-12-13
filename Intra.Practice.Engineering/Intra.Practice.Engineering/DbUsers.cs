using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Intra.Practice.Engineering.Models;
using Intra.Practice.Engineering.Data;

namespace Intra.Practice.Engineering
{
    public class DbUsers
    {
        public static bool SignUpFunc(String email, String password, String group, IntraContext context)
        {
            try
            {
                User user = context.Users.Where(u => u.email == email).First();

                System.Diagnostics.Debug.WriteLine("Error : " + user.email + " Already exist");
                return (false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                context.Users.Add(new User { email = email, password = password, group = group });
                context.SaveChanges();
                return (true);
            }
        }

        public static String SignInFunc(String email, String password, IntraContext context)
        {
            try
            {
                User user = context.Users.Where(u => u.email == email).First();

                System.Diagnostics.Debug.WriteLine("LOGIN : " + user.email + " Connected");
                return (user.group);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return ("undefined");
            }
        }

        public static Boolean PostDayOff(String dayStart, String dayEnd, String Reason, String UserEmail, IntraContext context)
        {
            context.DayOffs.Add(new DayOff { id=(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString().Split(",")[0],
            start=DateTime.Parse(dayStart),
            end=DateTime.Parse(dayEnd),
            reason=Reason,
            state="0",
            emailOwner=UserEmail});
            context.SaveChanges();
            return (true);
        }

        public static JArray getRequirementsListFromUser(String userInfo, IntraContext context)
        {
            List<DayOff> dayOffs = context.DayOffs.ToList();
            JArray array = new JArray();
            JObject obj;

            foreach (DayOff data in dayOffs)
            {
                if (data.emailOwner == userInfo)
                {
                    obj = new JObject();
                    obj.Add("start", data.start);
                    obj.Add("end", data.end);
                    obj.Add("state", data.state);
                    obj.Add("id", data.id);
                    obj.Add("reason", data.reason);
                    array.Add(obj);
                }
            }
            return (array);
        }

        public static Boolean removeOneItemFromList(String userEmail, String Id, IntraContext context)
        {
            context.DayOffs.Remove(new DayOff { id=Id, emailOwner=userEmail});
            context.SaveChanges();
            return (true);
        }

        public static JArray getAllRequirementsList(IntraContext context)
        {
            List<DayOff> dayOffs = context.DayOffs.ToList();
            JArray array = new JArray();
            JObject obj;

            foreach (DayOff data in dayOffs)
            {
                obj = new JObject();
                obj.Add("start", data.start);
                obj.Add("end", data.end);
                obj.Add("state", data.state);
                obj.Add("reason", data.reason);
                obj.Add("id", data.id);
                obj.Add("userEmailIntra", data.emailOwner);
                array.Add(obj);
            }
            return (array);
        }

        public static Boolean changeStateOneRequest(String userEmail, String idRequest, String newState, IntraContext context)
        {
            try
            {
                DayOff dayOff = context.DayOffs.Where(d => d.id == idRequest).First();

                dayOff.state = newState;
                context.DayOffs.Update(dayOff);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return (false);
            }
            return (true);
        }
    }
}