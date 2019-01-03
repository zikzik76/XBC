using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repo;
using XBC.ViewModel;

namespace XBC.MVC.WebApp.Controllers
{
    
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index(string search)
        {
            return View(RoleRepo.All(search));
        }
        // List
        public ActionResult List(string search)
        {
            return PartialView("_List", RoleRepo.All(search));
        }
       
        //Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            var userid = (long)Session["userid"];
            ResponResultViewModel result = RoleRepo.Update(model,userid);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        //Edit
        public ActionResult Edit(int id)
        {
            return PartialView("_Edit", RoleRepo.GetRole(id));
        }
        [HttpPost]
        public ActionResult Edit(RoleViewModel model)
        {
            var userid = (long)Session["userid"];
            ResponResultViewModel result = RoleRepo.Update(model,userid);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", RoleRepo.GetRole(id));
        }
        [HttpPost]
        public ActionResult Delete(RoleViewModel model)
        {
            var userid = (long)Session["userid"];
            ResponResultViewModel result = RoleRepo.Delete(model, userid);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}