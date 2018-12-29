using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repo;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class BTTController : Controller
    {
        // GET: BTT
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new BTTViewModel());
        }

        [HttpPost]
        public ActionResult Create(BTTViewModel model)
        {
            ResponseResult result = BTTRepo.CreateEdit(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            return PartialView("_List", BTTRepo.GetAll());
        }

        public ActionResult Search(string search)
        {
            return PartialView("_List", BTTRepo.GetAllBySearch(search));

        }

        public ActionResult Edit(int id)
        {
            return PartialView("_Edit", BTTRepo.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(BTTViewModel model)
        {
            ResponseResult result = BTTRepo.CreateEdit(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", BTTRepo.GetById(id));
        }

        [HttpPost]
        public ActionResult Delete(BTTViewModel model)
        {
            ResponseResult result = BTTRepo.Delete(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}