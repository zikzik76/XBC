using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    public class BTAfterUpdate
    {
        public long id { get; set; }

        public long? batchId { get; set; }

        public long? testId { get; set; }

        public long? created_by { get; set; }

        public DateTime? created_on { get; set; }
    }
    public class BatchTestRepo
    {
        public static Object dataBeforeUpdate;  // TEMPORARY BEFORE UPDATING DATA
        //public static Object dataAfterUpdate;   // TEMPORARY AFTER UPDATING DATA
        public static Object dataBeforeDelete;  // TEMPORARY BEFORE DELETING DATA

        //Get By Id
        public static BatchTestViewModel ById(int id)
        {
            BatchTestViewModel result = new BatchTestViewModel();
            using (var db = new XBCContext())
            {
                result = (from bt in db.t_batch_test
                          where bt.id == id
                          select new BatchTestViewModel
                          {
                              batchId = bt.batch_id,
                              testId = bt.test_id,

                              created_by = bt.created_by,
                              created_on = bt.created_on
                              

                          }).FirstOrDefault();

            }
            //  dataBeforeUpdate temporary for before update audit_log
            if (result != null)
            {
                dataBeforeUpdate = result;
            }
            return result != null ? result : new BatchTestViewModel();
        }

        //By BatchTest
        public static List<BatchTestViewModel> ByBatchTest()
        {
            List<BatchTestViewModel> result = new List<BatchTestViewModel>();

            using (var db = new XBCContext())
            {
                result = (from bt in db.t_batch_test
                          join b in db.t_batch
                          on bt.batch_id equals b.id
                          where bt.id != 0
                          select new BatchTestViewModel
                          {
                              id = bt.id,
                              batchId = bt.batch_id,
                              testId = bt.test_id,
                              created_by = bt.created_by,
                              created_on = bt.created_on

                          }).ToList();

            }
            return result;
        }

        //Delete
        public static ResponseResult Delete(BatchTestViewModel entity)
        {
            //id -> Variant Id
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBCContext())
                {
                    t_batch_test bt = db.t_batch_test.Where(o => o.id == entity.id).FirstOrDefault();
                    if (bt != null)
                    {
                        db.t_batch_test.Remove(bt);

                        db.SaveChanges();

                        result.Entity = entity;

                        //  AUDIT LOG => "MODIFY" DELETE
                        BTAfterUpdate dau = new BTAfterUpdate();
                        dau.id = bt.id;
                        dau.batchId = bt.batch_id;
                        dau.testId = bt.test_id;

                        dau.created_by = bt.created_by;
                        dau.created_on = bt.created_on;


                        AuditRepo.Update(dataBeforeUpdate, dau);
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "BatchTest not found!";
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
    }
}
