using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace HotelManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/resources")]
    public class ResourcesController : Controller
    {
        // GET: api/resources
        [HttpGet]
        public IEnumerable<ResourceData> Get()
        {
            return new ResourceData[] {
                new ResourceData { Id = "A", Name = "Resource A" },
                new ResourceData { Id = "B", Name = "Resource B" },
                new ResourceData { Id = "C", Name = "Resource C" },
                new ResourceData { Id = "D", Name = "Resource D" },
            };
        }

    }


    public class ResourceData
    {
        public String Id;
        public String Name;
    }
}
