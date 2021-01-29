using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/machinestates")]
    public class MachineStatesController : Controller
    {
        private IMachineStatesService _machinestatesService = new MachineStatesService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_machinestatesService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _machinestatesService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] MachineStates machinestates)
        {
            //if (string.IsNullOrEmpty(machinestates.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _machinestatesService.Add(machinestates);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] MachineStates machinestates)
        {
            _machinestatesService.Update(machinestates);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _machinestatesService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var machinestates = _machinestatesService.GetAll();

            //machinestates = machinestates.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(machinestates);
        }
    }
}
