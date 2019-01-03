using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repo;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class batchTestController : Controller
    {
        // GET: batchTest
        public ActionResult Index()
        {
            return View();
        }

        //Setup Test (Tabel Batch)
        public ActionResult Test(string search = " ")
        {
            return PartialView("_Test", TestRepo.All(search));
        }

        //[HttpPost]
        //public ActionResult Test(TestViewModel model)
        //{
        //    ResponseResult result = TestRepo.CreateParticipant(model);
        //    return Json(new
        //    {
        //        success = result.Success,
        //        message = result.Message,
        //        entity = result.Entity
        //    }, JsonRequestBehavior.AllowGet);
        //}

    }
}