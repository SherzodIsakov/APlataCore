using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/events")]
    public class EventsController : Controller
    {
        private IEventsService _eventsService = new EventsService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_eventsService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _eventsService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Events events)
        {
            //if (string.IsNullOrEmpty(events.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _eventsService.Add(events);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Events events)
        {
            _eventsService.Update(events);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _eventsService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var events = _eventsService.GetAll();

            //events = events.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(events);
        }
    }
}
