using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;


namespace APlataCore.Controllers.Api
{
    [Route("api/bpchosts")]
    public class BpcHostsController : Controller
    {
        private IBpcHostsService _bpchostsService = new BpcHostsService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_bpchostsService.GetAll());
        }


        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _bpchostsService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] BpcHosts bpchosts)
        {
            //if (string.IsNullOrEmpty(bpchosts.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _bpchostsService.Add(bpchosts);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] BpcHosts bpchosts)
        {
            _bpchostsService.Update(bpchosts);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _bpchostsService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var bpchosts = _bpchostsService.GetAll();

           // bpchosts = bpchosts.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(bpchosts);
        }
    }
}
