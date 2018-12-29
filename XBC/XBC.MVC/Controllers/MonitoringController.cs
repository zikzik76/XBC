using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repo;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class MonitoringController : Controller
    {
        // GET: Monitoring
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.BiodataList = new SelectList(BiodataRepo.GetBiodataIdle(), "id", "name");
            return PartialView("_Create", new MonitoringViewModel());
        }

        [HttpPost]
        public ActionResult Create(MonitoringViewModel model)
        {
            ResponseResult result = MonitoringRepo.CreateEdit(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            return PartialView("_List", MonitoringRepo.GetAll());
        }

        public ActionResult Search(string search)
        {
            return PartialView("_List", MonitoringRepo.GetAllBySearch(search));
        }

        public ActionResult Edit(int id)
        {
            ViewBag.BiodataList = new SelectList(BiodataRepo.All(), "id", "name");
            return PartialView("_Edit", MonitoringRepo.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(MonitoringViewModel model)
        {
            ResponseResult result = MonitoringRepo.CreateEdit(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", MonitoringRepo.GetById(id));
        }

        [HttpPost]
        public ActionResult Delete(MonitoringViewModel model)
        {
            ResponseResult result = MonitoringRepo.Delete(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        //  Add Placement (edit)
        public ActionResult Placement(int id)
        {
            ViewBag.BiodataList = new SelectList(BiodataRepo.All(), "id", "name");
            return PartialView("_Placement", MonitoringRepo.GetById(id));
        }

        [HttpPost]
        public ActionResult Placement(MonitoringViewModel model)
        {
            ResponseResult result = MonitoringRepo.CreatePlacement(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditPlacement(int id)
        {
            ViewBag.BiodataList = new SelectList(BiodataRepo.All(), "id", "name");
            return PartialView("_EditPlacement", MonitoringRepo.GetById(id));
        }

        [HttpPost]
        public ActionResult EditPlacement(MonitoringViewModel model)
        {
            ResponseResult result = MonitoringRepo.CreateEdit(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}