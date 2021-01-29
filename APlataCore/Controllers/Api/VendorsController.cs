using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/vendors")]
    public class VendorsController : Controller
    {
        private IVendorsService _vendorsService = new VendorsService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_vendorsService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _vendorsService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Vendors vendors)
        {
            //if (string.IsNullOrEmpty(vendors.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _vendorsService.Add(vendors);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Vendors vendors)
        {
            _vendorsService.Update(vendors);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _vendorsService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var vendors = _vendorsService.GetAll();

            //vendors = vendors.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(vendors);
        }
    }
}
