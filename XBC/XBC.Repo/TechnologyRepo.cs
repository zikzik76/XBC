using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    public class TechnologyRepo
    {
        public static Object dataBeforeUpdate;  // TEMPORARY BEFORE UPDATING DATA
        public static Object dataAfterUpdate;   // TEMPORARY AFTER UPDATING DATA
        public static Object dataBeforeDelete;  // TEMPORARY BEFORE DELETING DATA
        //search
        public static List<TechnologyViewModel> GetAllBySearch(string search)
        {
            List<TechnologyViewModel> result = new List<TechnologyViewModel>();
            using (var db = new XBCContext())
            {
                result = (from tc in db.t_technology
                          where tc.is_delete == false && tc.name.Contains(search) || search == null
                          select new TechnologyViewModel
                          {
                              id = tc.id,
                              name = tc.name,
                              notes = tc.notes,
                              created_by = tc.created_by

                          }).ToList();

            }
            return result;
        }

        //Get All
        public static List<TechnologyViewModel> All()
        {
            List<TechnologyViewModel> result = new List<TechnologyViewModel>();

            using (var db = new XBCContext())
            {
                result = (from tc in db.t_technology
                          join b in db.t_batch
                          on tc.id equals b.technology_id
                          where tc.is_delete == false
                          orderby tc.name ascending
                          select new TechnologyViewModel
                          {
                              id = tc.id,
                              name = tc.name,
                              notes = tc.notes

                          }).ToList();

            }
            return result;
        }

        //By Technology
        public static List<TechnologyViewModel> ByTechnology()
        {
            List<TechnologyViewModel> result = new List<TechnologyViewModel>();

            using (var db = new XBCContext())
            {
                result = (from tc in db.t_technology
                          where tc.is_delete == false
                          select new TechnologyViewModel
                          {
                              id = tc.id,
                              name = tc.name,
                              notes = tc.notes

                          }).ToList();

            }
            return result;
        }
        public static TechnologyViewModel ById(int id)
        {
            TechnologyViewModel result = new TechnologyViewModel();
            using (var db = new XBCContext())
            {
                //id technology.id
                result = (from tc in db.t_technology
                          where tc.id == id
                          select new TechnologyViewModel
                          {
                              id = tc.id,
                              name = tc.name,
                              notes = tc.notes,
                              created_by = tc.created_by
                          }).FirstOrDefault();
            }
            if (result != null)
            {
                dataBeforeUpdate = result;
            }
            return result != null ? result : new TechnologyViewModel();
        }

        //private static bool ByName(string name)
        //{
        //    TechnologyViewModel result = new TechnologyViewModel();

        //    using (var db = new XBCContext())
        //    {
        //        //id technology.id
        //        result = (from tc in db.t_technology
        //                  where tc.name.ToLower() == name.ToLower() && tc.is_delete == false
        //                  select new TechnologyViewModel
        //                  {
        //                      id = tc.id,
        //                      name = tc.name,
        //                      notes = tc.notes,
        //                      created_by = tc.created_by
        //                  }).FirstOrDefault();

        //    }
        //    return result != null ? false : true;
        //}

        //private static bool ByEditName(string name, long id)
        //{
        //    TechnologyViewModel result = new TechnologyViewModel();

        //    using (var db = new XBCContext())
        //    {
        //        //id technology.id
        //        result = (from tc in db.t_technology
        //                  where ((tc.id == id && tc.name.ToLower() == name.ToLower()) && tc.is_delete == false)
        //                  select new TechnologyViewModel
        //                  {
        //                      id = tc.id,
        //                      name = tc.name,
        //                      notes = tc.notes,
        //                      created_by = tc.created_by
        //                  }).FirstOrDefault();

        //    }
        //    return result != null ? true : false;
        //}

        //create new & update
        public static ResponseResult CreateEdit(TechnologyViewModel entity)
        {
            

            ResponseResult result = new ResponseResult();
            //jika nama sudah di create


            try
            {
                using (var db = new XBCContext())
                {
                    //insert
                    //bool isNameCreated = ByName(entity.name);
                    if (entity.id == 0)
                    {
                        //if (isNameCreated == true)
                        //{
                        t_technology technology = new t_technology();
                        technology.name = entity.name;
                        technology.notes = entity.notes;

                        technology.created_by = 1;
                        technology.created_on = DateTime.Now;

                        foreach (var item in entity.trainers)
                        {
                            t_technology_trainer tctr = new t_technology_trainer();
                            tctr.technology_id = technology.id;
                            tctr.trainer_id = item.id;

                            tctr.created_by = 1;
                            tctr.created_on = DateTime.Now;

                            db.t_technology_trainer.Add(tctr);

                            result.Entity = entity;
                        }
                        db.t_technology.Add(technology);
                        db.SaveChanges();

                        
                      
                    }
                    //else
                    //{
                    //    result.Success = false;
                    //    result.Message = "Nama technology sama, coba nama yg beda ya, manatau cocok :)";
                    //}
                    //}
                    else
                    {
                        //bool isEditNameCreated = ByEditName(entity.name, entity.id);

                        t_technology technology = db.t_technology.Where(o => o.id == entity.id).FirstOrDefault();

                        // (isEditNameCreated)
                        //{
                        if (technology != null && technology.is_delete == false)
                        {
                            technology.name = entity.name;
                            technology.notes = entity.notes;

                            technology.modified_by = 1;
                            technology.modified_on = DateTime.Now;

                            var tect_train = (from tt in db.t_technology_trainer
                                              where tt.technology_id == entity.id
                                              select new TechTrainerViewModel
                                              {
                                                  id = tt.id,
                                                  trainer_id = tt.trainer_id
                                              }).ToList();

                            foreach (var item in tect_train)
                            {
                                var newTechTrainer = entity.trainers.Find(o => o.id == item.trainer_id);
                                if (newTechTrainer == null)
                                {
                                    var remTrainer = db.t_technology_trainer.Where(o => o.id == item.id).FirstOrDefault();
                                    db.t_technology_trainer.Remove(remTrainer);
                                }
                            }

                            foreach (var item in entity.trainers)
                            {
                                var oldTechTrainer = db.t_technology_trainer.Where(o => o.trainer_id == item.id && o.technology_id == entity.id).FirstOrDefault();
                                if (oldTechTrainer == null)
                                {
                                    t_technology_trainer tctr = new t_technology_trainer();
                                    tctr.technology_id = entity.id;
                                    tctr.trainer_id = item.id;

                                    tctr.created_by = 1;
                                    tctr.created_on = DateTime.Now;

                                    db.t_technology_trainer.Add(tctr);

                                }
                            }

                            db.SaveChanges();
                            result.Entity = entity;
                        }

                        else
                        {
                            result.Success = false;
                            result.Message = "Trainer not found!";
                        }

                        //}
                        //else
                        //{
                        //    result.Success = false;
                        //    result.Message = "Nama technology sama, coba nama yg beda ya, manatau cocok :)";
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                //result.Message = "Data kemungkinan ganda atau tidak lengkap";
                result.Message = ex.Message;
            }

            return result;
        }


        //DELETE
        public static ResponseResult Delete(TechnologyViewModel entitiy)
        {
            //id -> technology Id
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBCContext())
                {
                    t_technology technology = db.t_technology.Where(o => o.id == entitiy.id).FirstOrDefault();
                    if (technology != null)
                    {
                        technology.is_delete = true;

                        technology.deleted_by = 1;
                        technology.deleted_on = DateTime.Now;

                        db.SaveChanges();

                        result.Entity = entitiy;


                        TechTrainerRepo.DeleteTechTrainer(entitiy.id);
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "technology not found!";
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

        //public static List<TrainerViewModel> EditTrainerById()
        //{
        //    List<TrainerViewModel> result = new List<TrainerViewModel>();

        //    return result;
        //}

        //baru dibuat
        public static List<TrainerViewModel> Detail()
        {
            List<TrainerViewModel> result = new List<TrainerViewModel>();

            return result;
        }

        public static TrainerViewModel Detailtrainer(int id)
        {
            TrainerViewModel result = new TrainerViewModel();
            using (var db = new XBCContext())
            {
                result = (from tr in db.t_trainer
                          where tr.id == id
                          select new TrainerViewModel
                          {
                              id = tr.id,
                              name = tr.name,
                              created_by = tr.created_by,

                          }).FirstOrDefault();
            }
            return result == null ? new TrainerViewModel() : result;
        }

        public static List<TrainerViewModel> Detail2(int id)
        {
            List<TrainerViewModel> result = new List<TrainerViewModel>();

            using (var db = new XBCContext())
            {

                result = (from tctr in db.t_technology_trainer
                          join tr in db.t_trainer
                          on tctr.trainer_id equals tr.id
                          where tctr.technology_id == id
                          select new TrainerViewModel
                          {
                              id = tr.id,
                              name = tr.name,
                              created_by = tr.created_by
                          }).ToList();

            }
            return result;
        }
    }
}