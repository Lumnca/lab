using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.IServices;

namespace LabExam.Services
{
    public class FileHandleService: IFileHandleService
    {
        public string GetDateTimeFileName()
        {
            DateTime dt = DateTime.Now;
            int year = dt.Year;
            String month = dt.Month > 9 ? dt.Month.ToString() : $"0{dt.Month}";
            String day = dt.Day > 9 ? dt.Day.ToString() : $"0{dt.Day}";
            String hour = dt.Hour > 9 ? dt.Hour.ToString() : $"0{dt.Hour}";
            String m = dt.Minute > 9 ? dt.Minute.ToString() : $"0{dt.Minute}";
            String s = dt.Second > 9 ? dt.Second.ToString() : $"0{dt.Second}";
            int ms = dt.Millisecond;
            return $"{year}{month}{day}{hour}{m}{s}{ms}";
        }
    }
}
