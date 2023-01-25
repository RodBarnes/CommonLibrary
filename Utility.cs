using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Common
{
    public static class Utility
    {
        public static string ParseException(Exception ex)
        {
            var sb = new StringBuilder();
            sb.Append($"{ex.Source}--{ex.Message}");

            var iex = ex.InnerException;
            while (iex != null)
            {
                sb.Append($"\n{iex.Source}--{iex.Message}");
                iex = iex.InnerException;
            }

            return sb.ToString();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        public static string GetAssemblyDir()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);

            return Path.GetDirectoryName(path);
        }

        public static string FormatWithComma(object value)
        {
            return string.Format("{0:n0}", value);
        }

        public static string StateCodeFromName(string name)
        {
            var checkString = name.TrimAll().ToLower();

            string code;
            switch (checkString)
            {
                case "alabama":
                    code = "AL";
                    break;
                case "alaska":
                    code = "AK";
                    break;
                case "arizona":
                    code = "AZ";
                    break;
                case "arkansas":
                    code = "AR";
                    break;
                case "california":
                    code = "CA";
                    break;
                case "colorado":
                    code = "CO";
                    break;
                case "connecticut":
                    code = "CT";
                    break;
                case "delaware":
                    code = "DE";
                    break;
                case "florida":
                    code = "FL";
                    break;
                case "georgia":
                    code = "GA";
                    break;
                case "hawaii":
                    code = "HI";
                    break;
                case "idaho":
                    code = "ID";
                    break;
                case "illinois":
                    code = "IL";
                    break;
                case "indiana":
                    code = "IN";
                    break;
                case "iowa":
                    code = "IA";
                    break;
                case "kansas":
                    code = "KS";
                    break;
                case "kentucky":
                    code = "KY";
                    break;
                case "lousiana":
                    code = "LA";
                    break;
                case "maine":
                    code = "ME";
                    break;
                case "maryland":
                    code = "MD";
                    break;
                case "massachusetts":
                    code = "MA";
                    break;
                case "michigan":
                    code = "MI";
                    break;
                case "minnesota":
                    code = "MN";
                    break;
                case "mississippi":
                    code = "MS";
                    break;
                case "missouri":
                    code = "MO";
                    break;
                case "montana":
                    code = "MT";
                    break;
                case "nebraska":
                    code = "NE";
                    break;
                case "nevada":
                    code = "NV";
                    break;
                case "newhampshire":
                    code = "NH";
                    break;
                case "newjersey":
                    code = "NJ";
                    break;
                case "newmexico":
                    code = "NM";
                    break;
                case "newyork":
                    code = "NY";
                    break;
                case "northcarolina":
                    code = "NC";
                    break;
                case "northdakota":
                    code = "ND";
                    break;
                case "ohio":
                    code = "OH";
                    break;
                case "oklahoma":
                    code = "OK";
                    break;
                case "oregon":
                    code = "OR";
                    break;
                case "pennsylvania":
                    code = "PA";
                    break;
                case "rhodeisland":
                    code = "RI";
                    break;
                case "southcarolina":
                    code = "SC";
                    break;
                case "southdakota":
                    code = "SD";
                    break;
                case "tennessee":
                    code = "TN";
                    break;
                case "texas":
                    code = "TX";
                    break;
                case "utah":
                    code = "UT";
                    break;
                case "vermont":
                    code = "VT";
                    break;
                case "washington":
                    code = "WA";
                    break;
                case "virginia":
                    code = "VA";
                    break;
                case "westvirginia":
                    code = "WV";
                    break;
                case "wisconsin":
                    code = "WI";
                    break;
                case "wyoming":
                    code = "WY";
                    break;
                default:
                    code = "XX";
                    break;
            }

            return code;
        }

        public static void LoadSettings(Settings list)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = config.AppSettings.Settings;

            foreach (var item in list)
            {
                // If a setting exists for this item, use that value
                if (settings.Count != 0 && settings[item.Name] != null && !string.IsNullOrEmpty(settings[item.Name].Value))
                {
                    item.Value = settings[item.Name].Value;
                }
            }
        }

        public static void SaveSettings(Settings list)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = config.AppSettings.Settings;

            foreach (var item in list)
            {
                // Add or update the value of the setting
                if (settings[item.Name] == null)
                {
                    settings.Add(item.Name, item.Value);
                }
                else
                {
                    settings[item.Name].Value = item.Value;
                }
            }
            config.Save(ConfigurationSaveMode.Full);
        }

    }
}
