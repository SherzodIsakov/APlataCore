using APlataCore.BusinessLogic.Services.WebApiService;
using APlataCore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APlataCore.Controllers
{
    public class MachinesController : Controller
    {
        private IMachinesService _machinesService = new MachinesService();
        public async Task<IActionResult> Search(string query)
        {
            var machines = await _machinesService.SearchLinqAsync(query);

            return View(machines);
        }

        //[Route("Machines/Index")]
        public async Task<IActionResult> Index()
        {
            var machines = await _machinesService.GetAllAsync();

            return View(machines);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { RedirectToAction("Index"); }

            var machines = await _machinesService.GetAsync(id);

            return View(machines);
        }

        #region CRUD

        public async Task<IActionResult> Create()
        {
            var machineModelsService = new MachineModelsService();
            var MachineModel = await machineModelsService.GetAllAsync();

            var terminalsService = new TerminalsService();
            var StateID = await terminalsService.GetAllTerminalsFreeAsync();

            ViewBag.MachineModel = new SelectList(MachineModel, "Name", "Name");
            ViewBag.StateID = new SelectList(StateID);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Machines machines)
        {
            try
            {
                var result = await _machinesService.CreateAsync(machines);

                if (result)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return View(machines);
            }
            catch
            {
                return View(machines);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { RedirectToAction("Index"); }

            var machines = await _machinesService.GetAsync(id);

            var machineModelsService = new MachineModelsService();
            var MachineModel = await machineModelsService.GetAllAsync();

            var terminalsService = new TerminalsService();
            var StateID = await terminalsService.GetAllTerminalsFreeAsync();

            List<int> StateID2 = new List<int>();
            foreach (var item in StateID) { StateID2.Add(item); }
            var selectedId = Convert.ToInt32(machines.StateID);
            StateID2.Add(selectedId);

            ViewBag.MachineModel = new SelectList(MachineModel, "Name", "Name", machines.MachineModel);
            ViewBag.StateID = new SelectList(StateID2, null, null, machines.StateID);

            return View(machines);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Machines machines)
        {
            try
            {
                var result = await _machinesService.EditAsync(id, machines);

                if (result)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return View(machines);
            }
            catch
            {
                return View(machines);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { RedirectToAction("Index"); }

            var machines = await _machinesService.GetAsync(id);
            return View(machines);
        }

        [HttpPost, Route("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                var result = await _machinesService.DeleteAsync(id);

                if (result)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        #endregion

        #region MachineItemsMap

        public async Task<IActionResult> EditMachineItemsMap(int? id)
        {
            if (id == null)
            {
                RedirectToAction("Index");
            }

            var machine = await _machinesService.GetAsync(id);

            List<Machines> machines = new List<Machines>() { machine };
            ViewBag.MachineID = new SelectList(machines, "Id", "Name", machine.Id);

            ItemsService itemsService = new ItemsService();
            var Items = await itemsService.GetAllAsync();
            ViewBag.ItemID = new SelectList(Items, "Id", "Name");

            List<int> MachineItemIDList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            ViewBag.MachineItemID = new SelectList(MachineItemIDList);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditMachineItemsMap(MachineItemsMap machineItemsMap)
        {
            var machines = await _machinesService.GetAllAsync();

            return View();
        }

        public async Task<IActionResult> EditMachineItemsMaps()
        {
            var machines = await _machinesService.GetAllAsync();
            ViewBag.MachineID = new SelectList(machines, "Id", "Name");

            ItemsService itemsService = new ItemsService();
            var Items = await itemsService.GetAllAsync();
            ViewBag.ItemID = new SelectList(Items, "Id", "Name");

            List<int> MachineItemIDList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            ViewBag.MachineItemID = new SelectList(MachineItemIDList);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditMachineItemsMaps(int? id, List<MachineItemsMap> machineItemsMaps)
        {
            var machines = await _machinesService.GetAllAsync();

            return View(machineItemsMaps);
        }
        #endregion

        #region Commands
        public async Task<IActionResult> SendCommands(int? id)
        {
            if (id == null)
            {
                RedirectToAction("Index");
            }

            var machine = await _machinesService.GetAsync(id);

            TerminalsService terminalsService = new TerminalsService();
            var machineOldCommands = await terminalsService.GetTerminalCommandsAsync(machine.StateID);

            CommandsService commandsService = new CommandsService();
            var commands = await commandsService.GetAllAsync();
            ViewBag.CommandID = new SelectList(commands, "CommandID", "CommandID");

            List<int> parametrs = new List<int>() { 1, 2, 3 };
            ViewBag.Parameter1 = new SelectList(parametrs);
            ViewBag.Parameter2 = new SelectList(parametrs);
            ViewBag.Parameter3 = new SelectList(parametrs);

            MachinesViewModel machinesViewModel = new MachinesViewModel
            {
                Machine = machine,
                Commands = machineOldCommands
            };

            return View(machinesViewModel);
        }

        //[ChildActionOnly]
        //public async Task<IActionResult> DetailsCommands(int? id)
        //{
        //    if (id == null)
        //    {
        //        RedirectToAction("Index");
        //    }
        //    var commands = await GetTerminalCommandsAsync(id);

        //    return PartialView("Partial/CommandPartialViews/ListCommands", commands);
        //}
        #endregion
    }
}
