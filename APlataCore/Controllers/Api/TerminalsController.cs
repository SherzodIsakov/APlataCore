using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/terminals")]
    public class TerminalsController : Controller
    {
        private ITerminalsService _terminalsService = new TerminalsService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_terminalsService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _terminalsService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Terminals terminals)
        {
            if (string.IsNullOrEmpty(terminals.TerminalAddress))
            {
                return Ok("Адрес не может быть пустым...");
            }
            _terminalsService.Add(terminals);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Terminals terminals)
        {
            _terminalsService.Update(terminals);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _terminalsService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var terminals = _terminalsService.GetAll();

            terminals = terminals.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(terminals);
        }
    }
}
