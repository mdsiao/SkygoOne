using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Reflection;

namespace HRISOnline.Utilities
{
    public static class Functions
    {
        public static double ComputeNoOfHours(DateTime time1, DateTime time2)
        {
            TimeSpan t;
            double result = 0.0;            
            double min = 0.0;

            try
            {
                t = time2 - time1;
                if (t.Minutes > 0) { min = Convert.ToDouble((double)t.Minutes / 100); }

                result = t.Hours + min;                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return result;
        }

        public static double ComputeNoOfHoursOT(DateTime time1, DateTime time2)
        {
            TimeSpan t;
            double result = 0.0;            
                        
            try
            {
                t = time2 - time1;
                
                result = t.TotalHours;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return result;
        }

        public static double ComputeNoOfDays(DateTime dtFrom, DateTime dtTo, bool hasWorkOnSat)
        {
            //compute for how many days
            double days = ((TimeSpan)(dtTo - dtFrom)).Days + 1;
            DateTime currDate = dtFrom;

            //exclude sunday & saturday if no work on saturday
            while (currDate <= dtTo) {
                
                if (((hasWorkOnSat == false) && (currDate.DayOfWeek.ToString() == "Saturday")) ||
                    (currDate.DayOfWeek.ToString() == "Sunday")){
                    
                    days -= 1;
                }

                currDate = currDate.AddDays(1);
            }

            return days;
        }

        public static DataTable ConvertToDatatable<T>(List<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable dt = new DataTable();

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    dt.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    dt.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }

                dt.Rows.Add(values);
            }

            return dt;
        }

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static List<string> GetPayrollPeriods(DateTime payrollStartDate)
        {
            List<string> dates = new List<string>();
            DateTime dToday = DateTime.Now.Date;
            DateTime limitDate;
            DateTime startDate;

            if (dToday.Day <= 15)
                limitDate = Convert.ToDateTime(dToday.Month + "/15/" + dToday.Year);
            else
                limitDate = GetEndOfMonth(Convert.ToDateTime(dToday));

            startDate = limitDate.AddMonths(-3);
            if (payrollStartDate > startDate)
                startDate = payrollStartDate;

            startDate = startDate.AddDays(1);
            DateTime runningDate = startDate;
            while(runningDate <= dToday)
            {
                string strDateStart = startDate.ToShortDateString();
                string strDateEnd = string.Empty;

                if (Convert.ToDateTime(strDateStart).Day <= 15)
                {
                    strDateEnd = new DateTime(startDate.Year, startDate.Month, 15).ToShortDateString();
                }
                else
                {
                    strDateEnd = GetEndOfMonth(Convert.ToDateTime(strDateStart)).ToShortDateString();
                }

                dates.Add(strDateStart + " - " + strDateEnd);
                startDate = Convert.ToDateTime(strDateEnd);
                startDate = startDate.AddDays(1);
                runningDate = startDate;
            }

            //dates.Sort();
            dates.Reverse();
            return dates;
        }

      



        //public static List<string> GetPayrollPeriods(DateTime payrollStartDate)
        //{
        //    List<string> dates = new List<string>();
        //    DateTime dToday = DateTime.Now.Date;
        //    DateTime limitDate = Convert.ToDateTime(dToday.Month + "/15/" + dToday.Year);
        //    DateTime startDate = payrollStartDate;
        //    int limiter = 0;
        //    int removeItemCount = 0;
        //    int diff = limitDate.Month - startDate.Month;
            

        //    if ((diff == 0) && (dToday < limitDate)){
        //        limiter = 1;
        //    }
        //    else if ((diff == 0) && (dToday.Day > 15)){
        //        limiter = 2;
        //    }
        //    else if ((dToday.Day <= 15) && (diff == 1)){
        //        limiter = (diff * 2) + 1;                
        //    }
        //    else if ((dToday.Day > 15) && (diff == 1)) {
        //        limiter = (diff * 2) + 2;
        //    }
        //    else if ((dToday.Day <= 15) && (diff > 1)){
        //        limiter = (diff * 2) + 1;                    
        //    }
        //    else if ((dToday.Day > 15) && (diff > 1)){
        //        limiter = (diff * 2) + 1;                
        //    }


        //    if (limiter > 6) {
        //        removeItemCount = limiter - 6;
        //    }

        //    if (dToday > limitDate) {
        //        limitDate = GetEndOfMonth(dToday);                
        //    }

        //    int counter;
        //    for (counter = 0; counter < limiter; counter++){
        //        string strDateStart = startDate.ToShortDateString();
        //        string strDateEnd = string.Empty;

        //        if (Convert.ToDateTime(strDateStart).Day <= 15){
        //            strDateEnd = new DateTime(startDate.Year, startDate.Month, 15).ToShortDateString();
        //        }
        //        else{
        //            strDateEnd = GetEndOfMonth(Convert.ToDateTime(strDateStart)).ToShortDateString();
        //        }

        //        dates.Add(strDateStart + " - " + strDateEnd);                                        
        //        startDate = Convert.ToDateTime(strDateEnd);
        //        startDate = startDate.AddDays(1);
        //    }

        //    for (int i = 0; i < removeItemCount; i++){
        //        dates.RemoveAt(0);
        //    }

        //    dates.Sort();
        //    dates.Reverse();
        //    return dates;

        //}

        public static DateTime GetEndOfMonth(DateTime dt)
        {
            DateTime today = dt;
            DateTime endOfMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));

            return endOfMonth;
        }

        public static string ConvertHoursToWords(double dHours)
        {
            string outputString = "0 h 0 m";
            string[] splitString = new string[] { };            
            double durationMin = 0;

            splitString = dHours.ToString().Split('.');
            outputString = splitString[0].ToString() + " h ";

            if (splitString.Length > 1) { 
                durationMin = Convert.ToDouble(splitString[1]);

                switch (splitString[1].ToString())
                { 
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                        durationMin = Convert.ToDouble(splitString[1]) * 10;
                        break;
                }
            }

            outputString = outputString + ((durationMin != 0) ? durationMin.ToString() + " m" : "");

            return outputString;
        }

        public static string ConvertDecimalHoursToWords(double dHours)
        {
            string outputString = "0 h 0 m";
            string[] splitString = new string[] { };
            double durationMin = 0;

            splitString = dHours.ToString().Split('.');
            outputString = splitString[0].ToString() + " h ";

            if (splitString.Length > 1)
            {
                durationMin = Convert.ToDouble(splitString[1]);

                switch (splitString[1].ToString())
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                        durationMin = Convert.ToDouble(splitString[1]) * 10;
                        break;
                }

                durationMin = durationMin / 100;
            }

            int durationMinOutput = Convert.ToInt32((durationMin * 60));
            outputString = outputString + ((durationMinOutput != 0) ? durationMinOutput.ToString() + " m" : "");

            return outputString;
        }
    }

    //date time span
    public struct DateTimeSpan
    {
        private readonly int years;
        private readonly int months;
        private readonly int days;
        private readonly int hours;
        private readonly int minutes;
        private readonly int seconds;
        private readonly int milliseconds;

        public DateTimeSpan(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds)
        {
            this.years = years;
            this.months = months;
            this.days = days;
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
            this.milliseconds = milliseconds;
        }

        public int Years { get { return years; } }
        public int Months { get { return months; } }
        public int Days { get { return days; } }
        public int Hours { get { return hours; } }
        public int Minutes { get { return minutes; } }
        public int Seconds { get { return seconds; } }
        public int Milliseconds { get { return milliseconds; } }

        enum Phase { Years, Months, Days, Done }

        public static DateTimeSpan CompareDates(DateTime date1, DateTime date2)
        {
            if (date2 < date1)
            {
                var sub = date1;
                date1 = date2;
                date2 = sub;
            }

            DateTime current = date1;
            int years = 0;
            int months = 0;
            int days = 0;

            Phase phase = Phase.Years;
            DateTimeSpan span = new DateTimeSpan();

            while (phase != Phase.Done)
            {
                switch (phase)
                {
                    case Phase.Years:
                        if (current.AddYears(years + 1) > date2)
                        {
                            phase = Phase.Months;
                            current = current.AddYears(years);
                        }
                        else
                        {
                            years++;
                        }
                        break;
                    case Phase.Months:
                        if (current.AddMonths(months + 1) > date2)
                        {
                            phase = Phase.Days;
                            current = current.AddMonths(months);
                        }
                        else
                        {
                            months++;
                        }
                        break;
                    case Phase.Days:
                        if (current.AddDays(days + 1) > date2)
                        {
                            current = current.AddDays(days);
                            var timespan = date2 - current;
                            span = new DateTimeSpan(years, months, days, timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
                            phase = Phase.Done;
                        }
                        else
                        {
                            days++;
                        }
                        break;
                }
            }

            return span;
        }
    }
}
