using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/machinestateslog")]
    public class MachineStatesLogController : Controller
    {
        private IMachineStatesLogService _machinestateslogService = new MachineStatesLogService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_machinestateslogService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _machinestateslogService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] MachineStatesLog machinestateslog)
        {
            //if (string.IsNullOrEmpty(machinestateslog.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _machinestateslogService.Add(machinestateslog);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] MachineStatesLog machinestateslog)
        {
            _machinestateslogService.Update(machinestateslog);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _machinestateslogService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var machinestateslog = _machinestateslogService.GetAll();

            //machinestateslog = machinestateslog.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(machinestateslog);
        }
    }
}
