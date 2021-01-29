using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/machinemodels")]
    public class MachineModelsController : Controller
    {
        private IMachineModelsService _machinemodelsService = new MachineModelsService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_machinemodelsService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _machinemodelsService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] MachineModels machinemodels)
        {
            //if (string.IsNullOrEmpty(machinemodels.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _machinemodelsService.Add(machinemodels);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] MachineModels machinemodels)
        {
            _machinemodelsService.Update(machinemodels);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _machinemodelsService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var machinemodels = _machinemodelsService.GetAll();

            //machinemodels = machinemodels.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(machinemodels);
        }
    }
}
