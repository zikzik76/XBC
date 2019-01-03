using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    //  HANDLE Circular references in JSONSerializer and StackOverflow exceptions with Create Manual Class
    public class BatchAfterUpdate
    {
        public long id { get; set; }
        public long technologyId { get; set; }
        public string technologyName { get; set; }
        public long trainerId { get; set; }
        public string trainerName { get; set; }
        public long biodataId { get; set; }
        public string biodataName { get; set; }
        public string name { get; set; }
        public DateTime? periodFrom { get; set; }
        public DateTime? periodTo { get; set; }
        public long? roomId { get; set; }
        public long? bootcampTypeId { get; set; }
        public string notes { get; set; }
        public long createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public long? modifiedBy { get; set; }
        public DateTime? modifiedOn { get; set; }
        public long? deletedBy { get; set; }
        public DateTime? deletedOn { get; set; }
        public bool isDelete { get; set; }
    }
    public class BatchRepo
    {
        public static Object dataBeforeUpdate;  // TEMPORARY BEFORE UPDATING DATA
        //public static Object dataAfterUpdate;   // TEMPORARY AFTER UPDATING DATA
        public static Object dataBeforeDelete;  // TEMPORARY BEFORE DELETING DATA

        //Get All
        public static List<BatchViewModel> All()
        {
            List<BatchViewModel> result = new List<BatchViewModel>();
            using (var db = new XBCContext())
            {
                result = (from b in db.t_batch
                          join tk in db.t_technology
                          on b.technology_id equals tk.id
                          join t in db.t_trainer
                          on b.trainer_id equals t.id
                          where b.is_delete == false
                          select new BatchViewModel
                          {
                              id = b.id,
                              technologyId = b.technology_id,
                              technologyName = tk.name,
                              name = b.name,
                              trainerId = b.trainer_id,
                              trainerName = t.name,
                              isDelete = b.is_delete

                          }).ToList();
            }
            return result != null ? result : new List<BatchViewModel>();
        }

        //get by param
        public static List<BatchViewModel> search(string searching)
        {
            List<BatchViewModel> result = new List<BatchViewModel>();
            using (var db = new XBCContext())
            {
                result = (from b in db.t_batch
                          join tk in db.t_technology
                          on b.technology_id equals tk.id
                          join tr in db.t_trainer
                          on b.trainer_id equals tr.id
                          where b.is_delete == false && tk.name.StartsWith(searching) || b.is_delete == false && b.name.StartsWith(searching) || b.is_delete == false && searching == null
                          select new BatchViewModel
                          {
                              id = b.id,
                              technologyId = b.technology_id, //id view Model
                              technologyName = tk.name,
                              name = b.name,
                              trainerId = b.trainer_id,
                              trainerName = tr.name,
                              isDelete = b.is_delete

                          }).ToList();
            }

            return result != null ? result : new List<BatchViewModel>();
        }

        //Get By Id
        public static BatchViewModel ById(int id, long userid)
        {
            BatchViewModel result = new BatchViewModel();
            using (var db = new XBCContext())
            {
                result = (from b in db.t_batch
                          join tk in db.t_technology
                          on b.technology_id equals tk.id
                          join t in db.t_trainer
                          on b.trainer_id equals t.id
                          join r in db.t_room
                          on b.room_id equals r.id
                          join bt in db.t_bootcamp_type
                          on b.bootcamp_type_id equals bt.id
                          where b.id == id 
                          select new BatchViewModel
                          {
                              //id = b.id,
                              technologyId = b.technology_id,
                              trainerId = b.trainer_id,
                              name = b.name,
                              periodFrom = b.period_from,
                              periodTo = b.period_to,
                              roomId = b.room_id,
                              bootcampTypeId = b.bootcamp_type_id,
                              notes = b.notes,
                              modifiedBy = userid,
                              modifiedOn = DateTime.Now

                          }).FirstOrDefault();

            }
            return result != null ? result : new BatchViewModel();
        }

        //By Test
        public static List<TestViewModel> ByTest(int id)
        {
            List<TestViewModel> result = new List<TestViewModel>();
            using (var db = new XBCContext())
            {
                result = (from t in db.t_test
                          join bt in db.t_batch_test
                          on t.id equals bt.test_id
                          join b in db.t_batch
                          on bt.batch_id equals b.id
                          where b.id == id 
                          select new TestViewModel
                          {
                              id = t.id,
                              name = t.name
                          }).ToList();
            }
            return result != null ? result : new List<TestViewModel>();
        }

        //By BT
        //public static List<BatchViewModel> ByBT(long bId,long btId)
        //{
        //    List<BatchViewModel> result = new List<BatchViewModel>();
        //    using (var db = new XBCContext())
        //    {
        //        result = (from bt in db.t_batch_test
        //                  join t in db.t_test
        //                  on bt.test_id equals t.id
        //                  join b in db.t_batch
        //                  on bt.batch_id equals b.id
        //                  where b.id == bId && bt.id == btId 
        //                  select new BatchViewModel
        //                  {
        //                      id = t.id,
        //                      batchTestId = bt.id,
        //                      name = t.name
        //                  }).ToList();
        //    }
        //    return result != null ? result : new List<BatchViewModel>();
        //}

        //Create New
        public static ResponseResult CreateEdit(BatchViewModel entity/*,long userid*/)
        {
            ResponseResult result = new ResponseResult();

            try
            {
                using (var db = new XBCContext())
                {
                    if (entity.id == 0)
                    {
                        t_batch batch = new t_batch();

                        batch.technology_id = entity.technologyId;
                        batch.trainer_id = entity.trainerId;
                        batch.name = entity.name;
                        batch.period_from = entity.periodFrom;
                        batch.period_to = entity.periodTo;
                        batch.room_id = entity.roomId;
                        batch.bootcamp_type_id = entity.bootcampTypeId;
                        batch.notes = entity.notes;

                        batch.created_by = 1;
                        batch.created_on = DateTime.Now;

                        db.t_batch.Add(batch);
                        db.SaveChanges();

                        result.Entity = entity;

                        AuditRepo.Insert(batch);
                    }
                    else
                    //update
                    {
                        t_batch batch = db.t_batch.Where(o => o.id == entity.id).FirstOrDefault();

                        if (batch != null)
                        {
                            batch.technology_id = entity.technologyId;
                            batch.trainer_id = entity.trainerId;
                            batch.name = entity.name;
                            batch.period_from = entity.periodFrom;
                            batch.period_to = entity.periodTo;
                            batch.room_id = entity.roomId;
                            batch.bootcamp_type_id = entity.bootcampTypeId;
                            batch.notes = entity.notes;

                            batch.modified_by = 1;
                            batch.modified_on = DateTime.Now; ;


                            db.SaveChanges();
                            result.Entity = entity;

                            //  AUDIT LOG => "MODIFY" UPDATE
                            BatchAfterUpdate dau = new BatchAfterUpdate();
                            dau.id = batch.id;
                            dau.technologyId = batch.technology_id;
                            dau.trainerId = batch.trainer_id;

                            dau.name = batch.name;
                            dau.periodFrom = batch.period_from;
                            dau.periodTo = batch.period_to;

                            dau.roomId = batch.room_id;
                            dau.bootcampTypeId = batch.bootcamp_type_id;
                            dau.notes = batch.notes;

                            dau.createdBy = batch.created_by;
                            dau.createdOn = batch.created_on;

                            dau.modifiedBy = batch.modified_by;
                            dau.modifiedOn = batch.modified_on;

                            AuditRepo.Update(dataBeforeUpdate, dau);

                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Batch Not Found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        //Save Batch test
        public static ResponseResult SaveBT(BatchTestViewModel entity/*, long userid*/)
        {
            ResponseResult result = new ResponseResult();

            try
            {
                using (var db = new XBCContext())
                {
                    if (entity.id == 0)
                    {
                        t_batch_test bt = new t_batch_test();

                        bt.id = entity.id;
                        bt.test_id = entity.testId;
                        bt.batch_id = entity.batchId;
                        bt.created_by = 1;
                        bt.created_on = DateTime.Now;

                        db.t_batch_test.Add(bt);

                        db.SaveChanges();
                        result.Entity = entity;

                        AuditRepo.Insert(bt);
                    }
                    else
                    {
                        result.Message = "Id have been saved!!!";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        //public static ResponseResult SaveOrder(OrderHeaderViewModel entity)
        //{
        //    ResponseResult result = new ResponseResult();

        //    try
        //    {
        //        entity.Reference = GetNewReference();

        //        using (var db = new XPosContext())
        //        {
        //            OrderHeaders oh = new OrderHeaders();
        //            oh.Reference = entity.Reference;
        //            oh.Amount = entity.Amount;
        //            oh.Active = entity.Active;

        //            oh.CreateBy = "Martin";
        //            oh.CreateDate = DateTime.Now;

        //            db.OrderHeaders.Add(oh);

        //            foreach (var item in entity.Details) //dari OrderHeaderViewModel
        //            {
        //                OrderDetails od = new OrderDetails();
        //                od.HeaderId = oh.Id;
        //                od.ProductId = item.ProductId;
        //                od.Price = item.Price;
        //                od.Quantity = item.Quantity;
        //                od.Active = item.Active;

        //                od.CreateBy = "Martin";
        //                od.CreateDate = DateTime.Now;

        //                db.OrderDetails.Add(od);
        //            }
        //            db.SaveChanges();

        //            result.Entity = entity;
        //        }
        //        //result.Reference = GetNewReference();
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.Message = ex.Message;

        //    }
        //    return result;
        //}

    }
}
