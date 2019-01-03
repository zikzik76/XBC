using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.ViewModel;
using XBC.Repo;

namespace XBC.MVC.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string search)
        {
            return PartialView("_List", RoleRepo.All(search));
        }
        public ActionResult Create()
        {
            return PartialView("_Create", new RoleViewModel());
        }
        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            ResponseResult result = RoleRepo.CreateEdit(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int id)
        {
            return PartialView("_Edit", RoleRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Edit(RoleViewModel model)
        {
            ResponseResult result = RoleRepo.CreateEdit(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", RoleRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Delete(RoleViewModel model)
        {
            ResponseResult result = RoleRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}