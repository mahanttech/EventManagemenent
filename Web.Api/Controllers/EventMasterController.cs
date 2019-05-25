using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Infrastructure.Interface;
using Web.Api.Infrastructure.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Api.Controllers
{
    [Route("api/EventMaster")]
    [ApiController]
    public class EventMasterController : Controller
    {


        private readonly IEventRepository _iEvent;
        public EventMasterController(IEventRepository iEvent)
        {
            _iEvent= iEvent;
        }


        [Route("AddUpdateEvent")]
        [HttpPost]
        public IActionResult InsertUpdateEvent(EventMasterModel eventMasterModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_iEvent.InsertUpdateEvent(eventMasterModel));
        }

        [HttpGet]
        [Route("EventList")]
        public IActionResult GetEventList(DateTime date)
        {
            return Ok(_iEvent.GetEventList(date));
        }

        [HttpGet]
        public IActionResult GetUserWeekReport(DateTime date)
        {
            return Ok(_iEvent.GetUserWeekReport(date));
        }

        [HttpGet]
        [Route("GetAllEvents")]
        public IActionResult GetAllEvents()
        {
            return Ok(_iEvent.GetAllEvents());
        }

    }
}
