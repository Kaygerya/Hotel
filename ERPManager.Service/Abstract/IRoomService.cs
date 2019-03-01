using ERPManager.Entities.Model.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Service.Abstract
{
    public interface IRoomService
    {
        Room GetById(string companyId, string id);
        void Insert(string companyId, Room room);
        void Update(string companyId, Room room);
        List<Room> GetAll(string companyId);
        void Delete(string companyId, Room room);
        List<Room> SearchRoom(string companyId, out long totalRecord, int start, int pageSize, string q, string sortId, bool asc);
    }
}
