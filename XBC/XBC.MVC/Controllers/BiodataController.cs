using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.DataModel;
using XBC.Repo;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class BiodataController : Controller
    {
        //XBCContext db = new XBCContext();
        // GET: Biodata
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            //var userid = (long)Session["userid"];
            return PartialView("_List", BiodataRepo.All(/*userid*/));
        }

        //public ActionResult Search(string s)
        //{
        //    List<BiodataViewModel> model = BiodataRepo.search(s);
        //    return View("Search", model);
        //}

        public ActionResult Search(string searching)
        {
            //var bio = from b in db.t_biodata
            //          select b;

            //if (!string.IsNullOrEmpty(searching))
            //{
            //    bio = bio.Where(b => b.name.Contains(searching) || searching == null);
            //}

            //return View("Search",bio.ToList());

            //return View(db.t_biodata.Where(b => b.name.StartsWith(searching) || searching == null).ToList());

            return PartialView("_List",BiodataRepo.search(searching));
            
        }

        public ActionResult Create()
        {
            return PartialView("_Create",new BiodataViewModel());
        }

        [HttpPost]
        public ActionResult Create(BiodataViewModel model)
        {
            //var userid = (long)Session["userid"];
            ResponseResult result = BiodataRepo.CreateEdit(model/*,userid*/);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            //var userid = (long)Session["userid"];
            BiodataViewModel model = BiodataRepo.ById(id/*,userid*/);
            ViewBag.BiodataList = new SelectList(BTTRepo.GetAll(), "id", "name");
            return PartialView("_Edit",model);
        }

        [HttpPost]
        public ActionResult Edit(BiodataViewModel model)
        {
            //var userid = (long)Session["userid"];
            ResponseResult result = BiodataRepo.CreateEdit(model/*,userid*/);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            //var userid = (long)Session["userid"];
            return PartialView("_Delete", BiodataRepo.ById(id/*,userid*/));
        }

        [HttpPost]
        public ActionResult Delete(BiodataViewModel model)
        {
            //var userid = (long)Session["userid"];
            ResponseResult result = BiodataRepo.Delete(model/*,userid*/);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);

        }
    }
}