using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.DataAccess.Model
{
    public class ConfigurationOptions
    {
        public MongoSettings MongoSettings { get; set; }
        public SiteSettings SiteSettings { get; set; }
    }

    public class MongoSettings
    {
        public string MongoConnection { get; set; }
        public string MongoDatabaseName { get; set; }
    }

    public class SiteSettings
    {
        public SiteSettings()
        {
            Authority = "http://localhost:5000/";
        }
        public string Authority { get; set; }
    }
}
