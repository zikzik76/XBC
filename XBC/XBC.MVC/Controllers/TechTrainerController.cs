//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using XBC.Repo;
//using XBC.ViewModel;

//namespace XBC.MVC.Controllers
//{
//    public class TechTrainerController : Controller
//    {
//        // GET: TechTrainer
//        public ActionResult Index()
//        {
//            return View();
//        }
//        public ActionResult List()
//        {
//            return PartialView("_List", TechTrainerRepo.All());
//        }

//        public ActionResult Create()
//        {
//            return PartialView("_Create", new TechTrainerViewModel());
//        }

//        [HttpPost]
//        public ActionResult Create(TechTrainerViewModel model)
//        {
//            ResponseResult result = TechTrainerRepo.CreateEdit(model);
//            return Json(new
//            {
//                success = result.Success,
//                message = result.Message,
//                entity = result.Entity
//            }, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult Edit(int id)
//        {
//            //id => Technology Id
//            return PartialView("_Edit", TechTrainerRepo.ById(id));
//        }

//        [HttpPost]
//        public ActionResult Edit(TechTrainerViewModel model)
//        {
//            ResponseResult result = TechTrainerRepo.CreateEdit(model);
//            return Json(new
//            {
//                success = result.Success,
//                message = result.Message,
//                entity = result.Entity
//            }, JsonRequestBehavior.AllowGet);
//        }

//        public ActionResult Delete(int id)
//        {
//            return PartialView("_Delete", TechTrainerRepo.ById(id));
//        }

//        [HttpPost]
//        public ActionResult Delete(TechTrainerViewModel model)
//        {
//            ResponseResult result = TechTrainerRepo.Delete(model);
//            return Json(new
//            {
//                success = result.Success,
//                message = result.Message,
//                entity = result.Entity
//            }, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult AddTrainer()
//        {
//            ViewBag.TrainerList = new SelectList(TrainerRepo.All(), "Id", "Name");
//            ViewBag.TechnologyList = new SelectList(TechnologyRepo.All(), "Id", "Name");
//            return PartialView("_AddTrainer", new TechTrainerViewModel());
//        }

//        [HttpPost]
//        public ActionResult AddTrainer(TechTrainerViewModel model)
//        {
//            ResponseResult result = TechTrainerRepo.CreateEdit(model);
//            return Json(new
//            {
//                success = result.Success,
//                message = result.Message,
//                entity = result.Entity
//            }, JsonRequestBehavior.AllowGet);
//        }
//    }
//}