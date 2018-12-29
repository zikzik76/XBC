using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;
using System.Web.Script.Serialization;  //  

namespace XBC.Repo
{
    public class AuditRepo
    {
        public static void Insert(Object data)
        {
            var dataJson = new JavaScriptSerializer().Serialize(data);
            using (var db = new XBCContext())
            {
                t_audit_log audi = new t_audit_log();
                audi.type = "INSERT";
                audi.json_insert = dataJson;

                //  createdby from user login
                audi.created_by = 111;
                audi.created_on = DateTime.Now;

                db.t_audit_log.Add(audi);
                db.SaveChanges();

            }
        }

        public static void Update(Object dataBefore, Object dataAfter)
        {
            var dataBeforeJson = new JavaScriptSerializer().Serialize(dataBefore);
            var dataAfterJson = new JavaScriptSerializer().Serialize(dataAfter);

            using (var db = new XBCContext())
            {
                t_audit_log audi = new t_audit_log();
                audi.type = "MODIFY";
                audi.json_before = dataBeforeJson;
                audi.json_after = dataAfterJson;

                //  createdby from user login
                audi.created_by = 111;
                audi.created_on = DateTime.Now;

                db.t_audit_log.Add(audi);
                db.SaveChanges();
            }
        }

        public static void Delete(Object data)
        {
            var dataJson = new JavaScriptSerializer().Serialize(data);

            using (var db = new XBCContext())
            {
                t_audit_log audi = new t_audit_log();
                audi.type = "DELETE";
                audi.json_delete = dataJson;

                //  createdby from user login
                audi.created_by = 111;
                audi.created_on = DateTime.Now;

                db.t_audit_log.Add(audi);
                db.SaveChanges();
            }
        }
    }
}