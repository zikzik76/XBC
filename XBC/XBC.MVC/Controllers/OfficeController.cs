using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repo;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class OfficeController : Controller
    {
        // GET: Office
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", OfficeRepo.get());
        }

        public ActionResult search(string search)
        {
            return PartialView("_List", OfficeRepo.All(search));
        }

        //public ActionResult OfficeByRoom()
        //{
        //    return PartialView("_OfficeByRoom", RoomRepo.ByOffice());
        //}

        public ActionResult RoomById(RoomViewModel model)
        {
            //RoomViewModel model = new RoomViewModel();
            return PartialView("_RoomById", model);
        }


        public ActionResult Create()
        {

            return PartialView("_Create", new OfficeViewModel());
        }

        [HttpPost]
        public ActionResult Create(OfficeViewModel model)
        {
            //var userid = (long)Session["userid"];
            ViewBag.RoomId = model;
            if (OfficeRepo.Create(model) == true)
            {
                var result = new
                {
                    success = true,
                    alertType = "success",
                    alertStrong = "Data Saved !",
                    alertMessage = "New Office has been add with Code" + model.name
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return PartialView();
        }

        public ActionResult Edit(int id)
        {
            //id => TestimonyId
            return PartialView("_Edit", OfficeRepo.ByID(id));
        }

        [HttpPost]
        public ActionResult Edit(OfficeViewModel model)
        {
            //var userid = (long)Session["userid"];
            if (OfficeRepo.CreateEdit(model) == true)
            {
                var result = new
                {
                    success = true,
                    alertType = "success",
                    alertStrong = "Data Saved !",
                    alertMessage = "Office has been edited" + model.name
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return View();

        }

        public ActionResult ViewModal2()
        {
            return PartialView();
        }

        public ActionResult Delete(int id)
        {
            //id => CategoryId
            return PartialView("_Delete", OfficeRepo.ByID(id));
        }

        [HttpPost]
        public ActionResult Delete(OfficeViewModel model)
        {
            ResponseResult result = OfficeRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Editview()
        {            
            return PartialView();
        }
    }
}

