using ERPManager.Entities.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManager.Web.Models.Room
{
    public class RoomListModel: BaseModel
    {
        public RoomListModel()
        {
            Items = new List<RoomModel>();
        }
        public List<RoomModel> Items { get; set; }
    }
}
