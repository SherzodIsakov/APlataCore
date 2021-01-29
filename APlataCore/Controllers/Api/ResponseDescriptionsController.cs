using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/responsedescriptions")]
    public class ResponseDescriptionsController : Controller
    {
        private IResponseDescriptionsService _responsedescriptionsService = new ResponseDescriptionsService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_responsedescriptionsService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _responsedescriptionsService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ResponseDescriptions responsedescriptions)
        {
            //if (string.IsNullOrEmpty(responsedescriptions.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _responsedescriptionsService.Add(responsedescriptions);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] ResponseDescriptions responsedescriptions)
        {
            _responsedescriptionsService.Update(responsedescriptions);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _responsedescriptionsService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var responsedescriptions = _responsedescriptionsService.GetAll();

            //responsedescriptions = responsedescriptions.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(responsedescriptions);
        }
    }
}
