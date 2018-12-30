using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    public class MonitoringRepo
    {
        public static ResponseResult CreateEdit(MonitoringViewModel entity)
        {
            ResponseResult result = new ResponseResult();

            try
            {
                using (var db = new XBCContext())
                {
                    // insert
                    if (entity.id == 0)
                    {
                        t_monitoring mt = new t_monitoring();

                        //  binding id w/ name on View
                        mt.id = entity.id;
                        mt.biodata_id = entity.biodata_id;

                        mt.idle_date = entity.idle_date;
                        mt.last_project = entity.last_project;
                        mt.idle_reason = entity.idle_reason;

                        //  createdby from user login
                        mt.created_by = 111;
                        mt.created_on = DateTime.Now;

                        db.t_monitoring.Add(mt);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else // edit
                    {
                        t_monitoring mt = db.t_monitoring.Where(o => o.id == entity.id).FirstOrDefault();

                        if (mt != null)
                        {
                            mt.id = entity.id;
                            mt.biodata_id = entity.biodata_id;
                            mt.idle_date = entity.idle_date;
                            mt.last_project = entity.last_project;
                            mt.idle_reason = entity.idle_reason;

                            //  createdby from user login
                            mt.modified_by = 111;
                            mt.modified_on = DateTime.Now;

                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = ("mt not found!");
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

        public static List<MonitoringViewModel> GetAll()
        {
            List<MonitoringViewModel> result = new List<MonitoringViewModel>();
            using (var db = new XBCContext())
            {
                result = (from mt in db.t_monitoring
                          join bio in db.t_biodata
                          on mt.biodata_id equals bio.id
                          where mt.is_delete == false
                          select new MonitoringViewModel
                          {
                              id = mt.id,
                              biodata_id = mt.biodata_id,
                              biodataName = bio.name,
                              idle_date = mt.idle_date,
                              last_project = mt.last_project,
                              idle_reason = mt.idle_reason,
                              placement_date = mt.placement_date,
                              placement_at = mt.placement_at,
                              notes = mt.notes,
                              is_delete = mt.is_delete
                          }).ToList();
            }
            return result != null ? result : new List<MonitoringViewModel>();
        }

        public static List<MonitoringViewModel> GetAllBySearch(string search)
        {
            List<MonitoringViewModel> result = new List<MonitoringViewModel>();
            using (var db = new XBCContext())
            {
                result = (from mt in db.t_monitoring
                          join bio in db.t_biodata
                          on mt.biodata_id equals bio.id
                          where mt.is_delete == false && bio.name.Contains(search) || search == null
                          select new MonitoringViewModel
                          {
                              id = mt.id,
                              biodata_id = mt.biodata_id,
                              biodataName = bio.name,
                              idle_date = mt.idle_date,
                              last_project = mt.last_project,
                              idle_reason = mt.idle_reason,
                              placement_date = mt.placement_date,
                              placement_at = mt.placement_at,
                              notes = mt.notes,
                              is_delete = mt.is_delete
                          }).ToList();
            }
            return result;
        }

        public static MonitoringViewModel GetById(int id)
        {
            MonitoringViewModel result = new MonitoringViewModel();
            using (var db = new XBCContext())
            {
                result = (from mt in db.t_monitoring
                          join bio in db.t_biodata
                          on mt.biodata_id equals bio.id
                          where mt.id == id
                          select new MonitoringViewModel
                          {
                              id = mt.id,
                              biodata_id = mt.biodata_id,
                              biodataName = bio.name,
                              idle_date = mt.idle_date,
                              last_project = mt.last_project,
                              idle_reason = mt.idle_reason
                          }).FirstOrDefault();
            }
            return result == null ? new MonitoringViewModel() : result;
        }

        public static ResponseResult Delete(MonitoringViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBCContext())
                {
                    t_monitoring mt = db.t_monitoring.Where(o => o.id == entity.id).FirstOrDefault();

                    if (mt != null)
                    {
                        mt.deleted_by = 111;
                        mt.deleted_on = DateTime.Now;
                        mt.is_delete = true;

                        db.SaveChanges();
                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Monitoring not found!";
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

        public static ResponseResult AddPlacement(MonitoringViewModel entity)
        {
            ResponseResult result = new ResponseResult();

            try
            {
                using (var db = new XBCContext())
                {
                    // insert
                    t_monitoring mt = db.t_monitoring.Where(o => o.id == entity.id).FirstOrDefault();

                    if (mt != null)
                    {
                        mt.placement_date = entity.placement_date;
                        mt.placement_at = entity.placement_at;
                        mt.notes= entity.notes;

                        //  createdby from user login
                        mt.modified_by = 111;
                        mt.modified_on = DateTime.Now;

                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = ("idle not found!");
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

        public static MonitoringViewModel GetByIdDuringIdle(int id)
        {
            MonitoringViewModel result = new MonitoringViewModel();
            using (var db = new XBCContext())
            {
                result = (from mt in db.t_monitoring
                          join bio in db.t_biodata
                          on mt.biodata_id equals bio.id
                          where mt.id == id
                          select new MonitoringViewModel
                          {
                              id = mt.id,
                              biodata_id = mt.biodata_id,
                              biodataName = bio.name,
                              idle_date = mt.idle_date, // added this for jquery compare
                              placement_date = mt.placement_date,
                              placement_at = mt.placement_at,
                              notes = mt.notes
                          }).FirstOrDefault();
            }
            return result == null ? new MonitoringViewModel() : result;
        }

        public static List<MonitoringViewModel> GetIdleDuringPlacement()
        {
            List<MonitoringViewModel> result = new List<MonitoringViewModel>();
            using (var db = new XBCContext())
            {
                result = (from bio in db.t_biodata
                          join mt in db.t_monitoring
                          on bio.id equals mt.biodata_id
                          where mt.placement_date == null && mt.is_delete == false
                          select new MonitoringViewModel
                          {
                              biodata_id = mt.biodata_id,
                              biodataName = bio.name
                          }).ToList();
            }
            return result != null ? result : new List<MonitoringViewModel>();
        }
    }
}
