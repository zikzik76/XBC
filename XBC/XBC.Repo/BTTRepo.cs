using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    public class BTTRepo
    {
        public static ResponseResult CreateEdit(BTTViewModel entity)
        {
            ResponseResult result = new ResponseResult();

            try
            {
                using (var db = new XBCContext())
                {
                    // insert
                    if (entity.id == 0)
                    {
                        t_bootcamp_test_type btt = new t_bootcamp_test_type();

                        btt.name = entity.name;
                        btt.notes = entity.notes;

                        //  createdby from user login
                        btt.created_by = 111;
                        btt.created_on = DateTime.Now;

                        db.t_bootcamp_test_type.Add(btt);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else // edit
                    {
                        t_bootcamp_test_type btt = db.t_bootcamp_test_type.Where(o => o.id == entity.id).FirstOrDefault();

                        if (btt != null)
                        {
                            btt.name = entity.name;
                            btt.notes = entity.notes;

                            //  createdby from user login
                            btt.modified_by = 111;
                            btt.modified_on = DateTime.Now;

                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = ("btt not found!");
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

        public static List<BTTViewModel> GetAll()
        {
            List<BTTViewModel> result = new List<BTTViewModel>();
            using (var db = new XBCContext())
            {
                result = (from btt in db.t_bootcamp_test_type
                          where btt.is_delete == false
                          select new BTTViewModel
                          {
                              id = btt.id,
                              name = btt.name,
                              created_by = btt.created_by,
                              is_delete = btt.is_delete
                          }).ToList();
            }
            return result;
        }

        public static List<BTTViewModel> GetAllBySearch(string search)
        {
            List<BTTViewModel> result = new List<BTTViewModel>();
            using (var db = new XBCContext())
            {
                result = (from btt in db.t_bootcamp_test_type
                          where btt.is_delete == false && btt.name.Contains(search) || search == null
                          select new BTTViewModel
                          {
                              id = btt.id,
                              name = btt.name,
                              created_by = btt.created_by,
                              is_delete = btt.is_delete
                          }).ToList();
            }
            return result;
        }

        public static BTTViewModel GetById(int id)
        {
            BTTViewModel result = new BTTViewModel();
            using (var db = new XBCContext())
            {
                result = (from btt in db.t_bootcamp_test_type
                          where btt.id == id
                          select new BTTViewModel
                          {
                              id = btt.id,
                              name = btt.name,
                              notes = btt.notes,

                              modified_by = 111,
                              modified_on = DateTime.Now
                          }).FirstOrDefault();
            }
            return result == null ? new BTTViewModel() : result;
        }

        public static ResponseResult Delete(BTTViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBCContext())
                {
                    t_bootcamp_test_type btt = db.t_bootcamp_test_type.Where(o => o.id == entity.id).FirstOrDefault();

                    if (btt != null)
                    {
                        btt.deleted_by = 111;
                        btt.deleted_on = DateTime.Now;
                        btt.is_delete = true;

                        db.SaveChanges();
                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "btt not found!";
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