using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Core.Settings
{
    public class ErpManagerSettings
    {
        public ErpManagerSettings()
        {
            PageSize = 15;
            DashboardDataItemCount = 5;
            CompanyImagePath = "";
            UserProfilePath = "";
            AppPicturePath = "";
        }

        public static int PageSize { get; set; }
        public static int DashboardDataItemCount { get; set; }

        public static string CompanyImagePath { get; set; }
        public static string UserProfilePath { get; set; }
        public static string AppPicturePath { get; set; }



        //entegration settings
        public static string LanguageContexConnectionString { get; set; }
        public static string LanguageContextDbType { get; set; }

        public static string IdentityContexConnectionString { get; set; }
        public static string IdentityContextDbType { get; set; }
    }
}
