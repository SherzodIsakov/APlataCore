using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/items")]
    public class ItemsController : Controller
    {
        private IItemsService _itemsService = new ItemsService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_itemsService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _itemsService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Items items)
        {
            //if (string.IsNullOrEmpty(items.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _itemsService.Add(items);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Items items)
        {
            _itemsService.Update(items);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _itemsService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var items = _itemsService.GetAll();

            //items = items.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(items);
        }
    }
}
