using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/eventypes")]
    public class EventTypesController : Controller
    {
        private IEventTypesService _eventypesService = new EventTypesService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_eventypesService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _eventypesService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] EventTypes eventypes)
        {
            //if (string.IsNullOrEmpty(eventypes.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _eventypesService.Add(eventypes);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] EventTypes eventypes)
        {
            _eventypesService.Update(eventypes);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _eventypesService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var eventypes = _eventypesService.GetAll();

           // eventypes = eventypes.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(eventypes);
        }
    }
}
