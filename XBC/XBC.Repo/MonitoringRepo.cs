using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    public class dataAfterUpdate
    {
        public long id { get; set; }
        public long biodata_id { get; set; }
        public DateTime idle_date { get; set; }
        public string last_project { get; set; }
        public string idle_reason { get; set; }
        public DateTime? placement_date { get; set; }
        public string placement_at { get; set; }
        public string notes { get; set; }
        public long created_by { get; set; }
        public DateTime created_on { get; set; }
        public long? modified_by { get; set; }
        public DateTime? modified_on { get; set; }
        public long? deleted_by { get; set; }
        public DateTime? deleted_on { get; set; }
        public bool is_delete { get; set; }
    }
    public class MonitoringRepo
    {
        public static Object dataBeforeUpdate;  // TEMPORARY BEFORE UPDATING DATA
        //public static Object dataAfterUpdate;   // TEMPORARY AFTER UPDATING DATA
        public static Object dataBeforeDelete;  // TEMPORARY BEFORE DELETING DATA

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

                        //  Audit Log "INSERT"
                        AuditRepo.Insert(mt);
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
                            mt.placement_date = entity.placement_date;

                            //  createdby from user login
                            mt.modified_by = 111;
                            mt.modified_on = DateTime.Now;

                            db.SaveChanges();

                            //  Audit Log "UPDATE"
                            dataAfterUpdate dau = new dataAfterUpdate();
                            dau.id = mt.id;
                            dau.biodata_id = mt.biodata_id;
                            dau.idle_date = mt.idle_date;
                            dau.last_project = mt.last_project;
                            dau.idle_reason = mt.idle_reason;
                            dau.placement_date = mt.placement_date;
                            dau.modified_by = mt.modified_by;
                            dau.modified_on = mt.modified_on;

                            AuditRepo.Update(dataBeforeUpdate, dau);
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
                              idle_reason = mt.idle_reason,

                              placement_date = mt.placement_date,
                              placement_at = mt.placement_at,
                              notes = mt.notes,

                              created_by = mt.created_by,
                              created_on = mt.created_on,
                              modified_by = mt.modified_by,
                              modified_on = mt.modified_on,
                              deleted_by = mt.deleted_by,
                              deleted_on = mt.deleted_on,
                              is_delete = mt.is_delete

                          }).FirstOrDefault();
            }

            if (result != null)
            {
                dataBeforeUpdate = result;
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

                        dataAfterUpdate dau = new dataAfterUpdate();
                        dau.id = mt.id;
                        dau.biodata_id = mt.biodata_id;
                        dau.idle_date = mt.idle_date;
                        dau.last_project = mt.last_project;
                        dau.idle_reason = mt.idle_reason;

                        dau.placement_date = mt.placement_date;
                        dau.placement_at = mt.placement_at;
                        dau.notes = dau.notes;

                        dau.deleted_by = mt.modified_by;
                        dau.deleted_on = mt.modified_on;
                        dau.is_delete = mt.is_delete;

                        AuditRepo.Update(dataBeforeUpdate, dau);
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

                        //  Audit Log "MODIFY"
                        dataAfterUpdate dau = new dataAfterUpdate();
                        dau.id = mt.id;
                        dau.biodata_id = mt.biodata_id;

                        dau.placement_date = mt.placement_date;
                        dau.placement_at = mt.placement_at;
                        dau.notes = mt.notes;

                        dau.modified_by = mt.modified_by;
                        dau.modified_on = mt.modified_on;

                        AuditRepo.Update(dataBeforeUpdate, dau);
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

            if (result != null)
            {
                dataBeforeUpdate = result;
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
