using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/log")]
    public class LogController : Controller
    {
        private ILogService _logService = new LogService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_logService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _logService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Log log)
        {
            //if (string.IsNullOrEmpty(log.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _logService.Add(log);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Log log)
        {
            _logService.Update(log);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _logService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var log = _logService.GetAll();

            //log = log.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(log);
        }
    }
}
