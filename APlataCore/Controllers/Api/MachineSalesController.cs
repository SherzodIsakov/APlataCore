using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/machinesales")]
    public class MachineSalesController : Controller
    {
        private IMachineSalesService _machinesalesService = new MachineSalesService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_machinesalesService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _machinesalesService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] MachineSales machinesales)
        {
            //if (string.IsNullOrEmpty(machinesales.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _machinesalesService.Add(machinesales);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] MachineSales machinesales)
        {
            _machinesalesService.Update(machinesales);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _machinesalesService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var machinesales = _machinesalesService.GetAll();

            //machinesales = machinesales.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(machinesales);
        }
    }
}
