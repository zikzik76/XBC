using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repo;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult Index()
        {
            return View();
        } 
        public ActionResult List()
        {
            return PartialView("_List", TrainerRepo.All());
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new TrainerViewModel());
        }

        [HttpPost]
        public ActionResult Create(TrainerViewModel model)
        {
            ResponseResult result = TrainerRepo.CreateEdit(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int id)
        {
            //id => Trainer Id
            return PartialView("_Edit", TrainerRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(TrainerViewModel model)
        {
            ResponseResult result = TrainerRepo.CreateEdit(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", TrainerRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(TrainerViewModel model)
        {
            ResponseResult result = TrainerRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

    }
}