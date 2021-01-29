using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/sessions")]
    public class SessionsController : Controller
    {
        private ISessionsService _sessionsService = new SessionsService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_sessionsService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _sessionsService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Sessions sessions)
        {
            //if (string.IsNullOrEmpty(sessions.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _sessionsService.Add(sessions);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Sessions sessions)
        {
            _sessionsService.Update(sessions);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _sessionsService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var sessions = _sessionsService.GetAll();

            //sessions = sessions.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(sessions);
        }
    }
}
