using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repo;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class BTController : Controller
    {
        // GET: BT
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return PartialView("_Create", new BTViewModel());
        }

        [HttpPost]
        public ActionResult Create(BTViewModel model)
        {
            ResponseResult result = BTRepo.CreateEdit(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            return PartialView("_List", BTRepo.GetAll());

        }

        public ActionResult Search(string search)
        {
            return PartialView("_List", BTRepo.GetAllBySearch(search));
        }

        public ActionResult Edit(int id)
        {
            return PartialView("_Edit", BTRepo.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(BTViewModel model)
        {
            ResponseResult result = BTRepo.CreateEdit(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", BTRepo.GetById(id));
        }

        [HttpPost]
        public ActionResult Delete(BTViewModel model)
        {
            ResponseResult result = BTRepo.Delete(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}