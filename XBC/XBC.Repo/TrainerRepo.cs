using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    public class TrainerRepo
    {

        //Get All
        public static List<TrainerViewModel> All()
        {
            List<TrainerViewModel> result = new List<TrainerViewModel>();

            using (var db = new XBCContext())
            {
                result = (from tr in db.t_trainer
                          where tr.is_delete == false
                          orderby tr.name ascending
                          select new TrainerViewModel
                          {
                              id = tr.id,
                              name = tr.name,
                              notes = tr.notes

                          }).ToList();

            }
            return result;
        }
        public static TrainerViewModel ById(int id)
        {
            TrainerViewModel result = new TrainerViewModel();
            using (var db = new XBCContext())
            {
                //id trainer.id
                result = (from tr in db.t_trainer
                          //join u in db.
                          where tr.id == id
                          select new TrainerViewModel
                          {
                              id = tr.id,
                              name = tr.name,
                              notes = tr.notes,
                              created_by_name = "Awi"
                          }).FirstOrDefault();
            }
            return result != null ? result : new TrainerViewModel(); //NB :jika hasil sama dengan null, maka dia akan menampilkan hasil kosong, bukan error
        }
        //create new & update
        public static ResponseResult CreateEdit(TrainerViewModel entity)
        {
            ResponseResult result = new ViewModel.ResponseResult();

            try
            {
                using (var db = new XBCContext())
                {
                    //insert
                    if (entity.id == 0)
                    {
                        t_trainer trainer = new t_trainer();
                        trainer.name = entity.name;
                        trainer.notes = entity.notes;

                        trainer.created_by = 1;
                        trainer.created_on = DateTime.Now;

                        db.t_trainer.Add(trainer);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        t_trainer trainer = db.t_trainer.Where(o => o.id == entity.id).FirstOrDefault();
                        if (trainer != null)
                        {
                            trainer.name = entity.name;
                            trainer.notes = entity.notes;
                            trainer.is_delete = entity.is_delete;

                            trainer.modified_by = 1;
                            trainer.modified_on = DateTime.Now;

                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Trainer not found!";
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

        //DELETE
        public static ResponseResult Delete(TrainerViewModel entitiy)
        {
            //id -> trainer Id
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBCContext())
                {
                    t_trainer trainer = db.t_trainer.Where(o => o.id == entitiy.id).FirstOrDefault();
                    if (trainer != null)
                    {
                        trainer.is_delete = true;

                        trainer.modified_by = 1;
                        trainer.modified_on = DateTime.Now;

                        db.SaveChanges();

                        result.Entity = entitiy;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "trainer not found!";
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

        //  ini ido yang add
        //public static List<TrainerViewModel> GetTrainerAvailable()
        //{
        //    List<TrainerViewModel> result = new List<TrainerViewModel>();

        //    using (var db = new XBCContext())
        //    {
        //        result = (from tr in db.t_trainer
        //                  join tc in db.t_technology
        //                  on 
        //                  where tr.is_delete == false
        //                  orderby tr.name ascending
        //                  select new TrainerViewModel
        //                  {
        //                      id = tr.id,
        //                      name = tr.name,
        //                      notes = tr.notes

        //                  }).ToList();

        //    }
        //    return result;
        //}
    }
}
