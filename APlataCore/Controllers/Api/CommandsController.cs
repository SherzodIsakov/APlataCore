using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/commands")]
    public class CommandsController : Controller
    {
        private ICommandsService _commandsService = new CommandsService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_commandsService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _commandsService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Commands commands)
        {
            //if (string.IsNullOrEmpty(commands.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _commandsService.Add(commands);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Commands commands)
        {
            _commandsService.Update(commands);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _commandsService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var commands = _commandsService.GetAll();

            //commands = commands.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(commands);
        }
    }
}
