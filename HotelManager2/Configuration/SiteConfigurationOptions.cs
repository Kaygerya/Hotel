using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManager.Web.Configuration
{
    public class SiteConfigurationOptions
    {
        public SiteConfigurationOptions()
        {
            Url = "http://localhost:51675/";
        }

        public static string Url { get; set; }
    }
}
