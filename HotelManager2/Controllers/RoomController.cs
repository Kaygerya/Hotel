using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HotelManager.Web.Models.Room;
using ERPManager.Entities.Model.Rooms;
using Microsoft.AspNetCore.Http;
using HotelManager.Web.Extensions;
using ERPManager.Entities.Model.Users;
using ERPManager.Service.Abstract;

namespace HotelManager.Web.Controllers
{
    public class RoomController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILanguageService _languageService;
        private readonly IRoomService _roomService;
        private readonly string  userId;
        private readonly string companyId;
        public RoomController(IHttpContextAccessor httpContextAccessor, ILanguageService languageService, IRoomService roomService)
        {
            _httpContextAccessor = httpContextAccessor;
            _languageService = languageService;
            _roomService = roomService;
            userId = _httpContextAccessor.CurrentUser();
        }

        public ActionResult Index()
        {
            RoomListModel model = new RoomListModel();

            return View(model);
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult LoadTable()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
            string search = Request.Form["search[value]"][0];
            //Get Sort columns value
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            long totalRecords = 0;

            var coolApps = _roomService.SearchRoom(companyId,out totalRecords, start, length, search, sortColumn, sortColumnDir == "asc"); //GetUserAddresses(user.Id, out totalRecords, start, length, search, sortColumn, sortColumnDir == "asc");
            RoomListModel model = new RoomListModel();
            model = PrepareCoolAppListModel(model, coolApps);
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = model.Items });
        }


        public ActionResult Create()
        {
            RoomModel model = new RoomModel();

            model = PrepareRoomModel(model, new Room());
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(RoomModel model)
        {

            Room room = _roomService.GetById(companyId,model.Id);

            if (room != null)
            {
                model.Errors.Add(_languageService.GetLocaleString("Room Already Exists"));
                return Json(model);
            }

            if (_roomService.GetAll(companyId).Where(k => k.Id == model.Id).Count() > 0)
            {
                model.Errors.Add(_languageService.GetLocaleString("Room Already Exists"));
                return Json(model);
            }

            if (ModelState.IsValid)
            {
                room = new Room();
                room.CompanyId = room.CompanyId;
                room.BedCount = model.BedCount;
                room.Number = model.Number;
                _roomService.Insert(companyId,room);
                model.SuccessMessage = _languageService.GetLocaleString("Room Created");
            }
            else
            {
                model.Errors.Add("Check fields for editing");
            }
            return Json(model);

        }

        public ActionResult Edit(string Id)
        {
            RoomModel model = new RoomModel();
            var room = _roomService.GetById(companyId,Id);


            if (room == null)
            {
                model.Errors.Add(_languageService.GetLocaleString("Room not found"));
                return View(model);
            }

            model = PrepareRoomModel(model, room);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RoomModel model)
        {
            var room = _roomService.GetById(companyId, model.Id);
            if (room == null)
            {
                model.Errors.Add(_languageService.GetLocaleString("Room not found"));
                return Json(model);
            }
            if (ModelState.IsValid)
            {
                room.CompanyId = model.CompanyId;
                room.Number = model.Number;
                room.BedCount = model.BedCount;
                _roomService.Update(companyId, room);

                model.SuccessMessage = _languageService.GetLocaleString("Room Updated");
            }
            else
            {
                model.Errors.Add("Check fields for editing");
            }
            return Json(model);
        }


        public ActionResult Delete(string Id)
        {
            RoomModel model = new RoomModel();
            var room = _roomService.GetById(companyId,Id);
            if (room != null)
            {
                _roomService.Delete(companyId, room);
            }
            else
            {
                model.Errors.Add(_languageService.GetLocaleString("Room Couldn't found"));
            }

            model.SuccessMessage = _languageService.GetLocaleString("Room Deleted successfully");
            return Json(model);
        }

        private RoomListModel PrepareCoolAppListModel(RoomListModel model, List<Room> rooms)
        {
            foreach (var c in rooms)
            {
                RoomModel ccm = new RoomModel();
                ccm = PrepareRoomModel(ccm, c);
                model.Items.Add(ccm);
            }

            return model;

        }

        private RoomModel PrepareRoomModel(RoomModel model, Room room)
        {
            model.Id = room.Id;
            model.CompanyId = room.CompanyId;
            model.Number  = room.Number;
            return model;
        }
    }
}
