using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DpParkingsParser.Utilities
{
    public class FileNameGenerator
    {
        public static string YearMonth
        {
            get
            {
                DateTime now = DateTime.Now;
                return $"{now.Year}/{now.Month}/";
            }
        }

        public static string Generate(string filename)
        {
            return YearMonth + filename;
        }
    }
}