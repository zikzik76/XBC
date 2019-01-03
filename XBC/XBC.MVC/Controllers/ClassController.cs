using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repo;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List",ClassRepo.All());
        }

        public ActionResult Search(string searching)
        {
            return PartialView("_List", ClassRepo.Search(searching));
        }

        public ActionResult CreateParticipant(int id)
        {
            //var userid = (long)Session["userid"];
            //id => Batch Id
            ViewBag.BatchId = id;
            ViewBag.BiodataList = new SelectList(BiodataRepo.All(/*userid*/), "id", "name");
            return PartialView("_AddParticipant", new ClassViewModel());
        }

        [HttpPost]
        public ActionResult CreateParticipant(ClassViewModel model)
        {
            //var userid = (long)Session["userid"];
            ResponseResult result = ClassRepo.CreateParticipant(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", ClassRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(ClassViewModel model)
        {
            ResponseResult result = ClassRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}