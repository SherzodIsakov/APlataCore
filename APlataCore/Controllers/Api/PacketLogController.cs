using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/packetlog")]
    public class PacketLogController : Controller
    {
        private IPacketLogService _packetlogService = new PacketLogService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_packetlogService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _packetlogService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] PacketLog packetlog)
        {
            //if (string.IsNullOrEmpty(packetlog.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _packetlogService.Add(packetlog);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] PacketLog packetlog)
        {
            _packetlogService.Update(packetlog);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _packetlogService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var packetlog = _packetlogService.GetAll();

            //packetlog = packetlog.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(packetlog);
        }
    }
}
