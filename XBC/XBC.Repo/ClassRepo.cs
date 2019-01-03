using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    public class ClassAfterUpdate
    {
        public long id { get; set; }

        public long batchId { get; set; }
        public string batchName { get; set; }
        public long biodataId { get; set; }
        public string biodataName { get; set; }
        public long created_by { get; set; }
        public DateTime created_on { get; set; }
    }

    public class ClassRepo
    {
        public static Object dataBeforeUpdate;  // TEMPORARY BEFORE UPDATING DATA
        //public static Object dataAfterUpdate;   // TEMPORARY AFTER UPDATING DATA
        public static Object dataBeforeDelete;  // TEMPORARY BEFORE DELETING DATA

        //Get All
        public static List<ClassViewModel> All()
        {
            List<ClassViewModel> result = new List<ClassViewModel>();
            using (var db = new XBCContext())
            {
                result = (from c in db.t_clazz
                          join ba in db.t_batch
                          on c.batch_id equals ba.id
                          join bi in db.t_biodata
                          on c.biodata_id equals bi.id
                          join tk in db.t_technology
                          on ba.technology_id equals tk.id
                          select new ClassViewModel
                          {
                              id = c.id,
                              technologyId = tk.id,
                              technologyName = tk.name,
                              batchId = ba.id,
                              batchName = ba.name,
                              biodataId = bi.id,
                              biodataName = bi.name
                          }).ToList();
            }
            return result != null ? result : new List<ClassViewModel>();
        }

        //Get By Id
        public static ClassViewModel ById(int id)
        {
            ClassViewModel result = new ClassViewModel();
            using (var db = new XBCContext())
            {
                result = (from c in db.t_clazz
                          join ba in db.t_batch
                          on c.batch_id equals ba.id
                          join bi in db.t_biodata
                          on c.biodata_id equals bi.id
                          where c.id == id
                          select new ClassViewModel
                          {
                              id = c.id,
                              batchId = c.batch_id,
                              biodataId = c.biodata_id,
                              biodataName = bi.name

                          }).FirstOrDefault();
            }

            return result != null ? result : new ClassViewModel();
        }

        //Get by Param
        public static List<ClassViewModel> Search(string searching)
        {
            List<ClassViewModel> result = new List<ClassViewModel>();

            using (var db = new XBCContext())
            {
                result = (from c in db.t_clazz
                          join ba in db.t_batch
                          on c.batch_id equals ba.id
                          join bi in db.t_biodata
                          on c.biodata_id equals bi.id
                          join tk in db.t_technology
                          on ba.technology_id equals tk.id
                          where ba.name.StartsWith(searching) || tk.name.StartsWith(searching) ||searching == null
                          select new ClassViewModel
                          {
                              id = c.id,
                              technologyId = ba.technology_id,
                              technologyName = tk.name,
                              batchId = c.batch_id,
                              batchName = ba.name,
                              biodataId = c.biodata_id,
                              biodataName = bi.name

                          }).ToList();
            }

            return result != null ? result : new List<ClassViewModel>();
        }

        //Create Participant
        public static ResponseResult CreateParticipant(ClassViewModel entity)
        {
            ResponseResult result = new ResponseResult();

            try
            {
                using (var db = new XBCContext())
                {    
                    if (entity.id == 0)
                    {
                        t_clazz cl = new t_clazz();

                        cl.batch_id = entity.batchId;
                        cl.biodata_id = entity.biodataId;

                        cl.created_by = 1;
                        cl.created_on = DateTime.Now;

                        db.t_clazz.Add(cl);
                        db.SaveChanges();

                        result.Entity = entity;

                        //  AUDIT LOG => "INSERT"
                        AuditRepo.Insert(cl);
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

        //Delete
        public static ResponseResult Delete(ClassViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBCContext())
                {
                    t_clazz cl = db.t_clazz.Where(o => o.id == entity.id).FirstOrDefault();
                    if (cl != null)
                    {
                        db.t_clazz.Remove(cl);
                        db.SaveChanges();

                        result.Entity = entity;

                        //  AUDIT LOG => "MODIFY" DELETE
                        ClassAfterUpdate dau = new ClassAfterUpdate();
                        dau.id = cl.id;
                        dau.biodataId = cl.biodata_id;
                        dau.batchId = cl.batch_id;

                        dau.created_by = cl.created_by;
                        dau.created_on = cl.created_on;

                        AuditRepo.Update(dataBeforeUpdate, dau);
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Class not found!";
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
