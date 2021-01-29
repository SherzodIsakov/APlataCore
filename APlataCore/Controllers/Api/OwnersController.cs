using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/owners")]
    public class OwnersController : Controller
    {
        private IOwnersService _ownersService = new OwnersService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_ownersService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _ownersService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Owners owners)
        {
            //if (string.IsNullOrEmpty(owners.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _ownersService.Add(owners);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Owners owners)
        {
            _ownersService.Update(owners);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _ownersService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var owners = _ownersService.GetAll();

           // owners = owners.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(owners);
        }
    }
}
