using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/machineitemsmap")]
    public class MachineItemsMapController : Controller
    {
        private IMachineItemsMapService _machineitemsmapService = new MachineItemsMapService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_machineitemsmapService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _machineitemsmapService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] MachineItemsMap machineitemsmap)
        {
            //if (string.IsNullOrEmpty(machineitemsmap.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _machineitemsmapService.Add(machineitemsmap);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] MachineItemsMap machineitemsmap)
        {
            _machineitemsmapService.Update(machineitemsmap);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _machineitemsmapService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var machineitemsmap = _machineitemsmapService.GetAll();

           // machineitemsmap = machineitemsmap.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(machineitemsmap);
        }
    }
}
