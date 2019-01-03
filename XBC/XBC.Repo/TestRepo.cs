using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    public class TestRepo
    {
        public static List<TestViewModel> All(string search)
        {
            List<TestViewModel> result = new List<TestViewModel>();
            using (var db = new XBCContext())
            {
                if ((String.IsNullOrEmpty(search)))
                {
                    result = (from t in db.t_test
                              where t.is_delete == false
                              select new TestViewModel
                              {
                                  id = t.id,
                                  name = t.name,
                                  is_bootcamp_test = t.is_bootcamp_test,
                                  notes = t.notes,
                                  is_delete = t.is_delete
                              }).ToList();
                }
                else
                {
                    var src = from t in db.t_test
                              where t.is_delete == false
                              select new TestViewModel
                              {
                                  id = t.id,
                                  name = t.name,
                                  is_bootcamp_test = t.is_bootcamp_test,
                                  notes = t.notes,
                                  is_delete = t.is_delete
                              };
                    src = src.Where(o => o.name.StartsWith(search));
                    result = src.ToList();
                }
            }
            return result != null ? result : new List<TestViewModel>();
        }
        public static TestViewModel ById(int id)
        {
            TestViewModel result = new TestViewModel();
            using (var db = new XBCContext())
            {
                result = (from t in db.t_test
                          where t.id == id
                          select new TestViewModel
                          {
                              id = t.id,
                              name = t.name,
                              is_bootcamp_test = t.is_bootcamp_test,
                              notes = t.notes,
                              is_delete = t.is_delete
                          }).FirstOrDefault();
            }
            return result != null ? result : new TestViewModel();
        }

        //Test List save Db
        public static List<TestViewModel> GetBySearch(long? search/*, long id*/)
        {
            List<TestViewModel> result = new List<TestViewModel>();
            using (var db = new XBCContext())
            {
                /*from bt in db.t_batch_test*/
                //join t in db.t_test
                //on bt.test_id equals t.id
                //join b in db.t_batch
                //on bt.batch_id equals b.id
                //where t.is_bootcamp_test == true &&
                //t.name.Contains(search)
                result = (
                          from t in db.t_test
                          join bt in db.t_batch_test
                          on t.id equals bt.test_id into fg
                          from fgi in (
                              from f in fg
                              where f.batch_id == search /*&& f.id == id*/
                              select f).DefaultIfEmpty()
                          where t.is_delete == false /*&& fgi.id == id*/
                          select new TestViewModel
                          {
                              id = t.id,
                              name = t.name,
                              //batchId = b.id,
                              batchTestId = (fgi.id == null ? 0 : fgi.id)
                              //is_bootcamp_test = t.is_bootcamp_test,
                              //notes = t.notes,
                              //is_delete = t.is_delete
                            
                          }).ToList();
            }
            return result != null ? result : new List<TestViewModel>();
        }


        public static ResponseResult CreateEdit(TestViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBCContext())
                {
                    if (entity.id == 0)
                    {
                        t_test test = new t_test();
                        test.name = entity.name;
                        test.is_bootcamp_test = entity.is_bootcamp_test;
                        test.notes = entity.notes;

                        test.created_by = 1;
                        test.created_on = DateTime.Now;

                        db.t_test.Add(test);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        t_test t = db.t_test.Where(o => o.id == entity.id).FirstOrDefault();

                        if (t != null)
                        {
                            t.name = entity.name;
                            t.is_bootcamp_test = entity.is_bootcamp_test;
                            t.notes = entity.notes;

                            t.modified_by = 1;
                            t.modified_on = DateTime.Now;

                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Test not found!!";
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

        public static ResponseResult Delete(TestViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBCContext())
                {
                    t_test test = db.t_test.Where(o => o.id == entity.id).FirstOrDefault();
                    if (test != null)
                    {
                        test.is_delete = true;
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Test tidak ditemukan";
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
