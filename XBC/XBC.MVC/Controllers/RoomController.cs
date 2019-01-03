using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repo;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", new List<RoomViewModel>());
        }

        public ActionResult CreateRoom()
        {
            return PartialView("_Create", new RoomViewModel());
        }

        [HttpPost]
        public ActionResult CreateRoom(RoomViewModel model)
        {
            ResponseResult result = RoomRepo.CreateEdit(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteRoom()
        {
            return PartialView("_DeleteRoom", new RoomViewModel());
        }



    }
}