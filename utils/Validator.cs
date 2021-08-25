using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;

namespace FileArchiver.utils
{
    class Validator
    {
        private static Config GetConfig()
        {
            Config cfg;
            string sJson = File.ReadAllText(@"config\cfg.json");
            cfg = JsonConvert.DeserializeObject<Config>(sJson);
            return cfg;
        }
        public static bool isValidDate(string filename = "ext24023_12_21_2017_19;02;04_58826_QFINITI10OBS4")
        {
            bool valid = false;
            try
            {
                Config cfg = GetConfig();
                DateTime dIni = convertToDateTime(cfg.fIni, "dd/MM/yyyy");
                DateTime dFin = convertToDateTime(cfg.fFin, "dd/MM/yyyy");
                string date = filename.Substring(9, 10).Replace("_", "/");
                DateTime dRecord = convertToDateTime(date, "MM/dd/yyyy");
                if (dIni < dRecord && dRecord < dFin)
                    valid = true;
            }
            catch(Exception e)
            {
                return false;
            }
            return valid;
        }

        public static bool isException(string filename = "ext24023_12_21_2017_19;02;04_58826_QFINITI10OBS4")
        {
            bool exception = false;
            Config cfg = GetConfig();
            for (int i = 0; i < cfg.exceptions.Length; i++)
            {
                if (filename.Contains(cfg.exceptions[i]))
                {
                    return true;
                }
            }
            return exception;
        }

        private static DateTime convertToDateTime(string dateString, string dateFormat)
        {
            return DateTime.ParseExact(dateString, dateFormat, CultureInfo.InvariantCulture);
        }
    }
}
