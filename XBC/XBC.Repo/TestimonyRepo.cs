using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    public class dataTestimonyAfterUpdate
    {
        public long id { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public bool is_delete { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }
    }

    public class TestimonyRepo
    {
        public static Object dataBeforeUpdate;
        public static Object dataBeforeDelete;
        public static object db { get; private set; }

        //GetAll
        public static List<TestimonyViewModel> All(string search)
        {
            List<TestimonyViewModel> result = new List<TestimonyViewModel>();
            using (var db = new XBCContext())
            {
                if ((String.IsNullOrEmpty(search)))
                {
                    result = (from t in db.t_testimony
                              where t.is_delete == false
                              select new TestimonyViewModel
                              {
                                  id = t.id,
                                  title = t.title,
                                  content = t.content,
                              }).ToList();
                }
                else
                {
                    var src = from t in db.t_testimony
                              where t.is_delete == false
                              select new TestimonyViewModel
                              {
                                  id = t.id,
                                  title = t.title,
                                  content = t.content,
                              };
                    src = src.Where(s => s.title.Contains(search));
                    result = src.ToList();
                }
                
            }
            return result;
        }

        //Get by Id
        public static TestimonyViewModel ByID(int id)
        {
            TestimonyViewModel result = new TestimonyViewModel();
            using (var db = new XBCContext())
            {
                result = (from t in db.t_testimony
                          where t.id == id
                          select new TestimonyViewModel
                          {
                              id = t.id,
                              title = t.title,
                              content = t.content,
                          }).FirstOrDefault();
            }
            return result != null ? result : new TestimonyViewModel();
        }

        //Create New
        public static ResponseResult CreateEdit(TestimonyViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            result.Success = true;
            try
            {
                using (var db = new XBCContext())
                {

                    //Insert
                    if (entity.id == 0)
                    {
                        t_testimony Testimony = new t_testimony();
                        Testimony.title = entity.title;
                        Testimony.content = entity.content;

                        Testimony.created_by = 1;
                        Testimony.created_on = DateTime.Now;
                        Testimony.modified_by = 2;
                        Testimony.modified_on = DateTime.Now;

                        db.t_testimony.Add(Testimony);
                        db.SaveChanges();

                        result.Entity = entity;

                        AuditRepo.Insert(Testimony);
                    }

                    //Update
                    else
                    {
                        t_testimony Testimony = db.t_testimony.Where(o => o.id == entity.id).FirstOrDefault();

                        if (Testimony != null)
                        {
                            Testimony.title = entity.title;
                            Testimony.content = entity.content;

                            Testimony.created_by = 1;
                            Testimony.created_on = DateTime.Now;
                            Testimony.modified_by = 2;
                            Testimony.modified_on = DateTime.Now;

                            db.SaveChanges();

                            result.Entity = entity;

                            dataTestimonyAfterUpdate test = new dataTestimonyAfterUpdate();

                            test.title = Testimony.title;
                            test.content = Testimony.content;

                            test.created_by = 1;
                            test.created_on = DateTime.Now;
                            test.modified_by = 2;
                            test.modified_on = DateTime.Now;

                            AuditRepo.Update(dataBeforeUpdate, test);
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Testimony Not Found ";
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

        //Delete
        public static ResponseResult Delete(TestimonyViewModel entity)
        {
            //id -> CategoryId
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBCContext())
                {
                    t_testimony Testimony = db.t_testimony.Where(o => o.id == entity.id).FirstOrDefault();
                    if (Testimony != null)
                    {

                        Testimony.is_delete = true;
                        db.SaveChanges();

                        result.Entity = entity;

                        dataTestimonyAfterUpdate test = new dataTestimonyAfterUpdate();
                        test.title = Testimony.title;
                        test.content = Testimony.content;

                        test.created_by = 1;
                        test.created_on = DateTime.Now;
                        test.modified_by = 2;
                        test.modified_on = DateTime.Now;
                        test.is_delete = Testimony.is_delete;
                        test.deleted_by = Testimony.deleted_by;
                        test.deleted_on = Testimony.deleted_on;

                        AuditRepo.Update(dataBeforeUpdate, test);

                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Category Not Found";
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
