using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repo;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class TestimonyController : Controller
    {
        // GET: Testimony
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string search)
        {
            return PartialView("_List", TestimonyRepo.All(search));
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new TestimonyViewModel());
        }

        [HttpPost]
        public ActionResult Create(TestimonyViewModel model)
        {
            ResponseResult result = TestimonyRepo.CreateEdit(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            //id => TestimonyId
            return PartialView("_Edit", TestimonyRepo.ByID(id));
        }

        [HttpPost]
        public ActionResult Edit(TestimonyViewModel model)
        {
            ResponseResult result = TestimonyRepo.CreateEdit(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Delete(int id)
        {
            //id => CategoryId
            return PartialView("_Delete", TestimonyRepo.ByID(id));
        }

        [HttpPost]
        public ActionResult Delete(TestimonyViewModel model)
        {
            ResponseResult result = TestimonyRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);

        }
    }
}