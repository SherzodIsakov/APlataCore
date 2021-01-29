using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APlataCore.Controllers
{
    public class CommandsViewController : Controller
    {
        // GET: CommandsViewController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CommandsViewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommandsViewController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommandsViewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommandsViewController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommandsViewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommandsViewController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommandsViewController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
