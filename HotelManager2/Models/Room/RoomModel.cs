using ERPManager.Entities.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManager.Web.Models.Room
{
    public class RoomModel : BaseModel
    {
        public string Id { get; set; }

        public string CompanyId { get; set; }

        public int Number { get; set; }

        public int BedCount { get; set; }
    }
}
