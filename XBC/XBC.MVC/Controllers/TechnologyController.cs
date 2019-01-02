using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repo;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class TechnologyController : Controller
    {

        // GET: Technology
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
           return PartialView("_List", TechnologyRepo.All());
        }

        //public ActionResult Index(string search)
        //{
        //    return View(TechnologyRepo.All(search));
        //}
        //public ActionResult List(string search)
        //{
        //    return PartialView("_List", TechnologyRepo.All(search));
        //}

        public ActionResult Search(string search)
        {
            return PartialView("_List", TechnologyRepo.GetAllBySearch(search));
        }

        public ActionResult Create()
        {
            ViewBag.TrainerList = new SelectList(TrainerRepo.All(), "id", "name");
            return PartialView("_Create", new TechnologyViewModel());
        }

        [HttpPost]
        public ActionResult Create(TechnologyViewModel model)
        {
            ResponseResult result = TechnologyRepo.CreateEdit(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int id)
        {
            //id => Technology Id
            ViewBag.TrainerList = (TrainerRepo.ById(id));
            return PartialView("_Edit", TechnologyRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(TechnologyViewModel model)
        {
            ResponseResult result = TechnologyRepo.CreateEdit(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", TechnologyRepo.ById(id));
        }
       
        [HttpPost]
        public ActionResult Delete(TechnologyViewModel model)
        {
            ResponseResult result = TechnologyRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        //baru dibuat 29/12/18
        public ActionResult Detail()
        {
            return PartialView("_Detail", TechnologyRepo.Detail());
        }

        public ActionResult Detailtrainer(int id)
        {
            return PartialView("_Detailtrainer", TechnologyRepo.Detailtrainer(id));
        }

        public ActionResult Detail2(int id)
        {
            return PartialView("_Detail2", TechnologyRepo.Detail2(id));
        }
        public ActionResult Trainer()
        {
            ViewBag.Trainerlist = new SelectList(TrainerRepo.All(), "id", "name");
            return PartialView("_Trainer");
        }

        public ActionResult Trainer2()
        {
            ViewBag.Trainerlist = new SelectList(TrainerRepo.All(), "id", "name");
            return PartialView("_Trainer2");
        }
    }
}