using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManager.Web.Models.Reservation
{
    public class CalendarModel
    {
        public CalendarModel()
        {
            CalendarDays = new List<CalendarDaysModel>();
        }
        public List<CalendarDaysModel> CalendarDays { get; set; }

    }
}
