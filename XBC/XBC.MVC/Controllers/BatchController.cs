using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XBC.Repo;
using XBC.ViewModel;

namespace XBC.MVC.Controllers
{
    public class BatchController : Controller
    {
        // GET: Batch
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            //ViewBag.BatchTestId = BTID;
            return PartialView("_List", BatchRepo.All());
        }

        public ActionResult Search(string searching)
        {
            return PartialView("_List", BatchRepo.search(searching));
        }

        //get BTID on _Test View
        //public ActionResult GetBT(long BID,long BTID)
        //{
        //    ViewBag.BatchTestId = BTID;
        //    return PartialView("_List", BatchRepo.ByBT(BID,BTID));
        //}

        public ActionResult Create()
        {
            ViewBag.TechnologyList = new SelectList(TechnologyRepo.ByTechnology(), "id", "name");
            ViewBag.TrainerList = new SelectList(TrainerRepo.ByTrainer(), "id", "name");
            ViewBag.RoomList = new SelectList(RoomRepo.All(),"id","name");
            ViewBag.BTList = new SelectList(BTRepo.GetAll(),"id","name");
            return PartialView("_Create", new BatchViewModel());
        }

        [HttpPost]
        public ActionResult Create(BatchViewModel model)
        {
            //var userid = (long)Session["userid"];
            ResponseResult result = BatchRepo.CreateEdit(model/*,userid*/);
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
            //BatchViewModel model = BatchRepo.ById(id,userid);

            BatchViewModel model = BatchRepo.ById(id);
            ViewBag.TechnologyList = new SelectList(TechnologyRepo.ByTechnology(), "id", "name");
            ViewBag.TrainerList = new SelectList(TrainerRepo.ByTrainer(), "id", "name");
            ViewBag.RoomList = new SelectList(RoomRepo.All(), "id", "name");
            ViewBag.BTList = new SelectList(BTRepo.GetAll(), "id", "name");
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(BatchViewModel model)
        {
            //var userid = (long)Session["userid"];
            ResponseResult result = BatchRepo.CreateEdit(model/*,userid*/);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        //Setup Test (Tabel Batch)
        public ActionResult Test(long? id)
        {
            //id => Batch Test Id
            //ViewBag.BatchTestId = bt;
            //id => Batch Id
            ViewBag.BatchId = id;
            ViewBag.BatchTestId = new SelectList(BatchTestRepo.ByBatchTest(/*btId*/));
            return PartialView("_Test", TestRepo.GetBySearch(id));
        }

        public ActionResult CreateBT(int testId, int batchId)
        {
            //id => Batch Test Id
            //ViewBag.BatchTestId = id;
            return PartialView("_Test",new BatchTestViewModel());
        }

        [HttpPost]
        public ActionResult CreateBT(BatchTestViewModel model)
        {
            //var userid = (long)Session["userid"];
            ResponseResult result = BatchRepo.SaveBT(model/*,userid*/);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteBT(int id)
        {
            return PartialView("_Test", BatchTestRepo.ById(id));
        }

        [HttpPost]
        public ActionResult DeleteBT(BatchTestViewModel model)
        {
            ResponseResult result = BatchTestRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);

        }


        //public ActionResult Test(string search = " ")
        //{
        //    return PartialView("_Test", TestRepo.GetBySearch(search));
        //}

        //public ActionResult CreateParticipant()
        //{
        //    ViewBag.BiodataList = new SelectList(BiodataRepo.All(), "id", "name");
        //    return PartialView("_AddParticipant", new BatchViewModel());
        //}

        //[HttpPost]
        //public ActionResult CreateParticipant(ClassViewModel model)
        //{
        //    ResponseResult result = ClassRepo.CreateParticipant(model);
        //    return Json(new
        //    {
        //        success = result.Success,
        //        message = result.Message,
        //        entity = result.Entity
        //    }, JsonRequestBehavior.AllowGet);
        //}
    }
}