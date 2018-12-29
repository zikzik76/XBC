using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    public class BTRepo
    {

        public static Object dataBeforeUpdate;  // TEMPORARY BEFORE UPDATING DATA
        public static Object dataAfterUpdate;   // TEMPORARY AFTER UPDATING DATA
        public static Object dataBeforeDelete;  // TEMPORARY BEFORE DELETING DATA
        public static ResponseResult CreateEdit(BTViewModel entity)
        {
            ResponseResult result = new ResponseResult();

            try
            {
                using (var db = new XBCContext())
                {
                    // insert
                    if (entity.id == 0)
                    {
                        t_bootcamp_type bt = new t_bootcamp_type();

                        bt.name = entity.name;
                        bt.notes = entity.notes;

                        //  createdby from user login
                        bt.created_by = 111;
                        bt.created_on = DateTime.Now;

                        db.t_bootcamp_type.Add(bt);
                        db.SaveChanges();

                        result.Entity = entity;

                        //  Audit Log "INSERT"
                        AuditRepo.Insert(bt);
                    }
                    else
                    {
                        t_bootcamp_type bt = db.t_bootcamp_type.Where(o => o.id == entity.id).FirstOrDefault();


                        if (bt != null)
                        {
                            bt.name = entity.name;
                            bt.notes = entity.notes;

                            //  createdby from user login
                            bt.modified_by = 111;
                            bt.modified_on = DateTime.Now;

                            db.SaveChanges();

                            result.Entity = entity;

                            //  Audit Log "UPDATE"
                            dataAfterUpdate = bt;
                            AuditRepo.Update(dataBeforeUpdate, dataAfterUpdate);
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = ("bt not found!");
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

        public static List<BTViewModel> GetAll()
        {
            List<BTViewModel> result = new List<BTViewModel>();
            using (var db = new XBCContext())
            {
                result = (from bt in db.t_bootcamp_type
                          where bt.is_delete == false
                          select new BTViewModel
                          {
                              id = bt.id,
                              name = bt.name,
                              created_by = bt.created_by,
                              is_delete = bt.is_delete
                          }).ToList();
            }
            return result;
        }


        public static List<BTViewModel> GetAllBySearch(string search)
        {
            List<BTViewModel> result = new List<BTViewModel>();
            using (var db = new XBCContext())
            {
                result = (from bt in db.t_bootcamp_type
                          where bt.is_delete == false && bt.name.Contains(search) || search == null
                          select new BTViewModel
                          {
                              id = bt.id,
                              name = bt.name,
                              created_by = bt.created_by,
                              is_delete = bt.is_delete
                          }).ToList();
            }
            return result;
        }


        public static BTViewModel GetById(int id)
        {
            BTViewModel result = new BTViewModel();
            using (var db = new XBCContext())
            {
                result = (from bt in db.t_bootcamp_type
                          where bt.id == id
                          select new BTViewModel
                          {
                              id = bt.id,
                              name = bt.name,
                              notes = bt.notes,

                              modified_by = 111,
                              modified_on = DateTime.Now
                          }).FirstOrDefault();
            }

            if (result != null)
            {
                dataBeforeUpdate = result;
            }

            return result == null ? new BTViewModel() : result;
        }

        public static ResponseResult Delete(BTViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBCContext())
                {
                    t_bootcamp_type bt = db.t_bootcamp_type.Where(o => o.id == entity.id).FirstOrDefault();

                    if (bt != null)
                    {
                        bt.deleted_by = 111;
                        bt.deleted_on = DateTime.Now;
                        bt.is_delete = true;

                        db.SaveChanges();
                        result.Entity = entity;

                        //  Audit Log "UPDATE"
                        dataAfterUpdate = bt;
                        AuditRepo.Update(dataBeforeUpdate, dataAfterUpdate);
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "bt not found!";
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