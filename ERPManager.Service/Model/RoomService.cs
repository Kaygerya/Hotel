using ERPManager.DataAccess.Abstract;
using ERPManager.Entities.Model.Rooms;
using ERPManager.Service.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPManager.Service.Model
{
    public class RoomService: IRoomService
    {
        private IHttpContextAccessor _httpContext { get; set; }
        private ICacheService _cacheService { get; set; }
        private IRepository _roomRepository;

        public RoomService(ICacheService cacheService, IHttpContextAccessor httpContext, IRepository roomRepository)
        {
            _cacheService = cacheService;
            _httpContext = httpContext;
            _roomRepository = roomRepository;
        }

        public Room GetById(string companyId, string id)
        {
            return _roomRepository.Single<Room>(k => k.Id == id && k.CompanyId == companyId);
        }

        public void Insert(string companyId, Room room)
        {
            room.CompanyId = companyId;
            _roomRepository.Add<Room>(room);
        }

        public void Update(string companyId, Room room)
        {
            _roomRepository.Single<Room>(k => k.Id == room.Id && k.CompanyId == companyId);

        }
        public List<Room> GetAll(string companyId)
        {

                var rooms = _roomRepository.Where<Room>(k=> k.CompanyId == companyId);
            return rooms.OrderBy(k => k.Number).ToList();
        }

        public void Delete(string companyId, Room room)
        {

            _roomRepository.Delete<Room>(k=> k.Id == room.Id && k.CompanyId == companyId);
        }

        public List<Room> SearchRoom(string companyId, out long totalRecord, int start, int pageSize, string q, string sortId, bool asc)
        {
            var query = _roomRepository.Where<Room>( k=> k.CompanyId == companyId).AsQueryable();


            if (!string.IsNullOrEmpty(sortId))
            {
                if (sortId == "CompanyId")
                {
                    if (asc)
                    {
                        query = query.OrderBy(k => k.CompanyId);
                    }
                    else
                    {
                        query = query.OrderByDescending(k => k.CompanyId);
                    }
                }
                else if (sortId == "BedCount")
                {
                    if (asc)
                    {
                        query = query.OrderBy(k => k.BedCount);
                    }
                    else
                    {
                        query = query.OrderByDescending(k => k.BedCount);
                    }
                }
                else if (sortId == "Number")
                {
                    if (asc)
                    {
                        query = query.OrderBy(k => k.Number);
                    }
                    else
                    {
                        query = query.OrderByDescending(k => k.Number);
                    }
                }
                else if (sortId == "Id")
                {
                    if (asc)
                    {
                        query = query.OrderBy(k => k.Id);
                    }
                    else
                    {
                        query = query.OrderByDescending(k => k.Id);
                    }
                }
            }
            else
            {
                query = query.OrderByDescending(k => k.CreatedDate);
            }

            totalRecord = query.LongCount();
            query = query.Skip(start).Take(pageSize);



            return query.ToList();
        }
    }

}
