using APlataCore.BusinessLogic.Services;
using APlataCore.BusinessLogic.Services.FileService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APlataCore.Controllers.Api
{
    [Route("api/transactions")]
    public class TransactionsController : Controller
    {
        private ITransactionsService _transactionsService = new TransactionsService();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_transactionsService.GetAll());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var terminal = _transactionsService.Get(id);

            if (terminal == null)
            {
                return NotFound();
            }
            return Ok(terminal);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Transactions transactions)
        {
            //if (string.IsNullOrEmpty(transactions.TerminalAddress))
            //{
            //    return Ok("Адрес не может быть пустым...");
            //}
            _transactionsService.Add(transactions);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Transactions transactions)
        {
            _transactionsService.Update(transactions);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _transactionsService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string name)
        {
            var transactions = _transactionsService.GetAll();

            //transactions = transactions.Where(x => x.TerminalAddress.ToLower().Contains(name.ToLower())).ToList();

            return Ok(transactions);
        }
    }
}
