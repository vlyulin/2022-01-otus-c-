using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace hw03_solid
{
    /// <summary>
    /// SRP – принцип единой ответственности: ответственность - только загрузка настроек игры определенным способом.
    /// </summary>
    public class AppInitFromFile : IAppInit
    {
        public Config LoadConfig()
        {
            Config config = new Config();
            config.AttemptsNumber = GetInitValue("AttemptsNumber");
            config.RangeFrom = GetInitValue("RangeFrom");
            config.RangeTo = GetInitValue("RangeTo");
            return config;
        }

        private static int GetInitValue(string paramName)
        {
            string? str = ConfigurationManager.AppSettings.Get(paramName);
            try
            {
                return Convert.ToInt32(str);
            }
            catch (Exception ex)
            {
                throw new Exception(paramName + " setting is not defined or int value: [" + str + "]. " 
                    + ex.Message);
            }
        }
    }
}
