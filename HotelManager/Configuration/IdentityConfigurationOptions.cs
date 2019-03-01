using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManager.Web.Configuration
{
    public class IdentityConfigurationOptions
    {
        public static string Authority { get; set; }
        public static string ClientId { get; set; }
        public static string Secret { get; set; }
        public static string Scope { get; set; }
        public static string SignInAsAuthenticationType { get; set; }
        public static string RedirectUrl { get; set; }
        public static bool RequireHttpsMetadata { get; set; }

    }
}
