using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/machines")]
    public class MachinesController : Controller
    {
        private IMachinesService _machinesService = new MachinesService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_machinesService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _machinesService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Machines machines)
        {
            //if (string.IsNullOrEmpty(machines.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _machinesService.Add(machines);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Machines machines)
        {
            _machinesService.Update(machines);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _machinesService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var machines = _machinesService.GetAll();

            //machines = machines.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(machines);
        }
    }
}
