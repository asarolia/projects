using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DML.Generator.Web.Models;
using DML.Generator.Domain;
using DML.Generator.Web.Attributes;

namespace DML.Generator.Web.Controllers
{
    public class ConfigurationController : Controller
    {
        /// <summary>
        /// Adds the table.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddTable()
        {
            return View(new TableViewModel());
        }

        /// <summary>
        /// Adds the table.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddTable(TableViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = ConfigurationFactory.AddExceedTable(viewModel.TableName.ToUpper(),
                    viewModel.HostDMLName.ToUpper(), viewModel.FEDMLName.ToUpper());
            }
            return View(viewModel);
        }

        /// <summary>
        /// Adds the table.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditTable()
        {
            return View(new TableViewModel());
        }

        /// <summary>
        /// Adds the table.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        [Button(ButtonName = "Get")]
        [ActionName("EditTable")]
        [ValidateOnly(new string[1] {"TableName"})]
        public ActionResult GetTable(TableViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string HostDMLName, FEDMLName;
                bool isGroupedTable;
                viewModel.IsTableExist = ConfigurationFactory.GetExceedTable(viewModel.TableName.ToUpper(), out HostDMLName, out FEDMLName, out isGroupedTable);

                if (!viewModel.IsTableExist)
                {
                    TempData["message"] = "Table does not exist.";
                    return View(viewModel);
                }

                viewModel.IsGroupedTable = isGroupedTable;
                viewModel.HostDMLName = HostDMLName ?? string.Empty;
                viewModel.FEDMLName = FEDMLName ?? string.Empty;
            }
            return View(viewModel);
        }

        /// <summary>
        /// Adds the table.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        [Button(ButtonName = "EditTable")]
        public ActionResult EditTable(TableViewModel viewModel)
        {
            viewModel.IsTableExist = !string.IsNullOrEmpty(viewModel.TableName);
            
            if (viewModel.IsGroupedTable)
            {
                ModelState.Remove("HostDMLName");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            
            ConfigurationFactory.EditExceedTable(viewModel.TableName.ToUpper(), viewModel.HostDMLName, viewModel.FEDMLName, viewModel.IsGroupedTable);
            TempData["message"] = string.Format("Table {0} edited successfully", viewModel.TableName);
            return RedirectToAction("EditTable");
        }

        /// <summary>
        /// Adds the scheme.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddScheme()
        {
            return View(new AddSchemeViewModel());
        }

        /// <summary>
        /// Adds the scheme.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddScheme(AddSchemeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = ConfigurationFactory.AddExceedScheme(viewModel.SchemeId);
            }
            return View(viewModel);
        }

    }
}
