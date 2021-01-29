using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private IUsersService _usersService = new UsersService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_usersService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _usersService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Users users)
        {
            //if (string.IsNullOrEmpty(users.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _usersService.Add(users);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Users users)
        {
            _usersService.Update(users);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _usersService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var users = _usersService.GetAll();

           // users = users.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(users);
        }
    }
}
