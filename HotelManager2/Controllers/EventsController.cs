using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelManager.Web.Controllers
{
    [Produces("application/json")]
    public class EventsController : Controller
    {
        // GET: api/events
        [HttpGet]
        [HttpPost]
        [Route("api/events")]
        public ActionResult Get(string callback)
        {
            var eventDatas = new List<EventData>();
            eventDatas.Add(new EventData
            {
                Id = "1",
                Title = "Event 1",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(5),
                Description = "A",
                RoomId = "1",
                IsAllDay = false
            }
                );

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            return Content(callback + "("+ JsonConvert.SerializeObject(eventDatas, settings) + ")");
        }

        [HttpGet]
        [HttpPost]
        [Route("api/events/create")]
        public ActionResult Create(string models, string callback)
        {
            List<EventData> evetDatas = JsonConvert.DeserializeObject<List<EventData>>(models);


            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            return Content(callback + "(" + JsonConvert.SerializeObject(evetDatas, settings) + ")");
        }
    }



    public class EventData
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TimeZoneInfo StartTimezone { get; set; }
        public TimeZoneInfo  EndTimezone { get; set; }
        public string Description{ get; set; }
        public string RoomId { get; set; }
        public bool IsAllDay { get; set; }


    }
}
